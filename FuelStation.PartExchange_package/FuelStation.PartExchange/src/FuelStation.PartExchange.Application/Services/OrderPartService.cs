using FuelStation.PartExchange.Application.DTOs;
using FuelStation.PartExchange.Application.DTOs.OrderPart;
using FuelStation.PartExchange.Application.Interfaces;
using FuelStation.PartExchange.Application.Interfaces.General;
using FuelStation.PartExchange.Domain.Enums;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using IDatabase = StackExchange.Redis.IDatabase;

namespace FuelStation.PartExchange.Application.Services;

public class OrderPartService : IOrderPartService
{
    public ILogger<OrderPartService> LogService { get; }
    private readonly ILogger<OrderPartService> _logger;
    private readonly IDatabase _cache;
    private readonly IRepository<Order> _orderRepo;
    private readonly IRepository<Part> _partRepo;
    private readonly IRepository<StationInventory> _stationRepo;
    private readonly IRepository<Domain.Models.FuelStation> _fuelStationRepo;
    private IPartInventoryRepository _inventoryRepo;
    private readonly IPartMatchingService _matchingService;
    private readonly IStringLocalizer<OrderPartService> _orderLocalizer;
    private readonly IAPIService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFileService _fileService;
    public static string CacheKeyOrder = "DM-OrderDetail-"; //+id
    public static string CacheKeyOrderList = "DM-OrderListUser-";
    private readonly TimeSpan _cacheExpirationTime = TimeSpan.FromMinutes(5);

    public OrderPartService(
        ICacheProvider connection,
        IRepository<Order> orderRepository,
        IRepository<Part> partRepository,
        IRepository<StationInventory> stationRepository,
        IRepository<Domain.Models.FuelStation> fuelStationRepository,
        IPartInventoryRepository inventoryRepository,
        IPartMatchingService matchingService,
        ILogger<OrderPartService> logger,
        IStringLocalizer<OrderPartService> orderLocalizer,
        IAPIService apiService,
        IFileService fileService,
        IHttpContextAccessor httpContextAccessor)
    {
        _cache = connection.Database;
        _orderRepo = orderRepository;
        _partRepo = partRepository;
        _stationRepo = stationRepository;
        _fuelStationRepo = fuelStationRepository;
        _inventoryRepo = inventoryRepository;
        _matchingService = matchingService;
        _logger = logger;
        _orderLocalizer = orderLocalizer;
        _apiService = apiService;
        _fileService = fileService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResponseDto> AddOrder(AddOrderRequestDto input, string username)
    {
        try
        {
            if (input.Quantity <= 0)
            {
                return new ResponseDto
                {
                    StatusCode = 400, Message = [_orderLocalizer["Quantity must be greater than 0"]]
                };
            }

            //CHECK STATION IS EXIST
            var station = await _stationRepo.GetEntitiesById(input.RequestingStationId);
            if (station is null)
            {
                return new ResponseDto
                {
                    StatusCode = 400, Message = [_orderLocalizer["Requesting station not found"]]
                };
            }

            //CHECK PART IS EXIST
            var part = await _partRepo.GetEntitiesById(input.PartId);
            if (part is null)
            {
                return new ResponseDto
                {
                    StatusCode = 400, Message = [_orderLocalizer["Part not found"]]
                };
            }

            //CREATE PART REQUEST FOR CREATE ORDER
            var request = new PartRequest
            {
                Id = input.PartId,
                StationId = input.RequestingStationId,
                PartNumber = part.PartNumber,
                Quantity = input.Quantity,
                Status = Domain.Enums.PartRequestStatus.Created,
                CreatedAt = DateTime.UtcNow
            };

            //FIND SUPPLIER BASE ON REQUESTING PART IN SAME CITY
            var suppliers = await _matchingService.FindPartInCityAsync(station.StationId, request.Quantity,
                request.PartNumber);

            if (suppliers == null || !suppliers.Any())
            {
                var fuelStation = await _fuelStationRepo.GetEntitiesById(input.RequestingStationId);
                return new ResponseDto
                {
                    StatusCode = 400, Message =
                    [
                        _orderLocalizer["No suppliers found in city {City} for part {PartNumber}", fuelStation?.City,
                            request.PartNumber]
                    ]
                };
            }

            var (supplierStation, inventory) = suppliers.First();

            var order = new Order
            {
                Id = Guid.NewGuid(),
                RequestingStationId = input.RequestingStationId,
                SupplierStationId = supplierStation.Id,
                PartId = inventory.PartId,
                Quantity = input.Quantity,
                Status = Domain.Enums.OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _orderRepo.AddEntity(order);
            request.Status = Domain.Enums.PartRequestStatus.Matched;
            await _orderRepo.SaveChanges();
            return new ResponseDto
            {
                StatusCode = 200, Message =
                [
                    _orderLocalizer["Add order successful."]
                ]
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ResponseDto> GetOrderById(Guid orderId, string username)
    {
        try
        {
            var order = await _orderRepo.GetEntitiesById(orderId);
            if (order is null)
            {
                return new ResponseDto
                {
                    StatusCode = 400, Message =
                    [
                        _orderLocalizer["Order not found"]
                    ]
                };
            }

            return new ResponseDto
            {
                StatusCode = 200, Message =
                [
                    _orderLocalizer["Get order by id successful." + order]
                ]
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ResponseDto> GetAllOrders(GetAllOrdersRequestDto requestDto, string username)
    {
        try
        {
            List<Order> orders = new List<Order>();
            orders = await _orderRepo.GetEntitiesByQueryIgnoreFilter(x => !x.IsDeleted).ToListAsync();
            if (orders is null)
            {
                return new ResponseDto
                {
                    StatusCode = 200, Message =
                    [
                        _orderLocalizer["No orders found"]
                    ]
                };
            }

            return new ResponseDto
            {
                StatusCode = 200, Message =
                [
                    _orderLocalizer["Get all orders is successful." + orders.Count]
                ]
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ResponseDto> DeleteOrderById(Guid orderId, string username)
    {
        try
        {
            var order = await _orderRepo.GetEntitiesById(orderId);
            if (order is null)
            {
                return new ResponseDto
                {
                    StatusCode = 400, Message =
                    [
                        _orderLocalizer["Order not found"]
                    ]
                };
            }

            _orderRepo.RemoveEntity(order);
            await _orderRepo.SaveChanges();
            return new ResponseDto
            {
                StatusCode = 200, Message =
                [
                    _orderLocalizer["Order is deleted successful."]
                ]
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ResponseDto> UpdateOrder(UpdateOrderRequestDto requestDto, string username)
    {
        var getOrder = await _orderRepo.GetEntitiesById(requestDto.OrderId);
        if (getOrder == null) return new ResponseDto
        {
            StatusCode = 400, Message =
            [
                _orderLocalizer["Order not found"]
            ]
        };
        getOrder.RequestingStationId = requestDto.RequestingStationId;
        getOrder.SupplierStationId = requestDto.SupplierStationId;
        getOrder.PartId = requestDto.PartId;
        getOrder.Quantity = requestDto.Quantity;
        getOrder.Status = requestDto.Status;
        getOrder.UpdatedAt = DateTime.UtcNow;
        await _orderRepo.UpdateEntity(getOrder);
        await _orderRepo.SaveChanges();
        return new ResponseDto
        {
            StatusCode = 200, Message =
            [
                _orderLocalizer["Update Order id successful." + getOrder]
            ]
        };
    }

    public async  Task<ResponseDto> ConfirmOrder(Guid dtoId, string userName)
    {
        var order = await _orderRepo.GetEntitiesById(dtoId);
        if (order == null) return new ResponseDto
        {
            StatusCode = 400, Message =
            [
                _orderLocalizer["Order not found"]
            ]
        };
        order.Status = OrderStatus.Confirmed;
        await _orderRepo.UpdateEntity(order);
        await _orderRepo.SaveChanges();
        return new ResponseDto
        {
            StatusCode = 200, Message =
            [
                _orderLocalizer["Confirm Order id successful." + order]
            ]
        };
    }
}