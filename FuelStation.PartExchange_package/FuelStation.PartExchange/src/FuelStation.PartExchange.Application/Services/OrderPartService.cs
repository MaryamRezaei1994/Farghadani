using Application.Interfaces.General;
using FuelStation.PartExchange.Application.DTOs;
using FuelStation.PartExchange.Application.DTOs.OrderPart;
using FuelStation.PartExchange.Application.Interfaces;
using FuelStation.PartExchange.Application.Interfaces.General;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

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
    private IUnitOfWork _uow;
    private readonly IPartMatchingService _matchingService;
    private readonly IStringLocalizer<OrderPartService> _orderLocalizer;
    private readonly IAPIService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFileService _fileService;
    public static string CacheKeyOrder = "DM-OrderDetail-"; //+id
    public static string CacheKeyOrderList = "DM-OrderListUser-";
    private readonly TimeSpan _cacheExpirationTime = TimeSpan.FromMinutes(5);

    public OrderPartService(ICacheProvider connection,
        IRepository<Order> orderRepository,
        IRepository<Part> partRepository,
        IRepository<StationInventory> stationRepository,
        IRepository<Domain.Models.FuelStation> fuelStationRepository,
        IPartInventoryRepository inventoryRepository,
        IUnitOfWork uow,
        IPartMatchingService matchingService,
        ILogger<OrderPartService> logService,
        IStringLocalizer<OrderPartService> orderLocalizer, IAPIService apiService,
        IFileService fileService, IHttpContextAccessor httpContextAccessor
        , ILogger<OrderPartService> logger, IRepository<Part> partRepo,
        IRepository<Domain.Models.FuelStation> fuelStationRepo)
    {
        LogService = logService;
        _cache = connection.Database;
        _orderRepo = orderRepository;
        _orderRepo = orderRepository;
        _stationRepo = stationRepository;
        _inventoryRepo = inventoryRepository;
        _uow = uow;
        _matchingService = matchingService;
        _orderLocalizer = orderLocalizer;
        _apiService = apiService;
        _fileService = fileService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _partRepo = partRepo;
        _fuelStationRepo = fuelStationRepo;
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
                Status = FuelStation.PartExchange.Domain.Enums.OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _orderRepo.AddEntity(order);
            request.Status = FuelStation.PartExchange.Domain.Enums.PartRequestStatus.Matched;
            await _uow.SaveChangesAsync();
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

    public Task<ResponseDto> GetOrderYId(Guid orderId, string username)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> DeleteOrder(DeleteOrderByIdRequest request, string username)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> GetAllOrders(GetAllOrdersRequest request, string username)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> DeleteOrderById(Guid orderId, string username)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> UpdateOrder(UpdateOrderRequest request, string username)
    {
        throw new NotImplementedException();
    }
}