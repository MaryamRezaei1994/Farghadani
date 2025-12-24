using FuelStation.PartExchange.Domain.Entities;
using FuelStation.PartExchange.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FuelStation.PartExchange.Application.Services;

public class PartRequestService
{
    private readonly IFuelStationRepository _stationRepo;
    private readonly IPartInventoryRepository _inventoryRepo;
    private readonly IOrderRepository _orderRepo;
    private readonly IUnitOfWork _uow;
    private readonly IPartMatchingService _matchingService;
    private readonly ILogger<PartRequestService> _logger;

    public PartRequestService(
        IFuelStationRepository stationRepo,
        IPartInventoryRepository inventoryRepo,
        IOrderRepository orderRepo,
        IUnitOfWork uow,
        IPartMatchingService matchingService,
        ILogger<PartRequestService> logger)
    {
        _stationRepo = stationRepo;
        _inventoryRepo = inventoryRepo;
        _orderRepo = orderRepo;
        _uow = uow;
        _matchingService = matchingService;
        _logger = logger;
    }

    public async Task<PartRequest?> CreatePartRequestAsync(Guid requestingStationId, string partNumber, int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be > 0", nameof(quantity));

        var station = await _stationRepo.GetByIdAsync(requestingStationId);
        if (station is null) throw new InvalidOperationException("Requesting station not found");

        var request = new PartRequest
        {
            Id = Guid.NewGuid(),
            RequestingStationId = requestingStationId,
            PartNumber = partNumber,
            Quantity = quantity,
            Status = FuelStation.PartExchange.Domain.Enums.PartRequestStatus.Created,
            CreatedAt = DateTime.UtcNow
        };

        var suppliers = await _matchingService.FindSuppliersAsync(requestingStationId, partNumber, quantity);

        if (!suppliers.Any())
        {
            _logger.LogInformation("No suppliers found in city {City} for part {PartNumber}", station.City, partNumber);
            await _uow.SaveChangesAsync();
            return request;
        }

        var (supplierStation, inventory) = suppliers.First();

        var order = new Order
        {
            Id = Guid.NewGuid(),
            RequestingStationId = requestingStationId,
            SupplierStationId = supplierStation.Id,
            PartId = inventory.PartId,
            Quantity = quantity,
            Status = FuelStation.PartExchange.Domain.Enums.OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _orderRepo.AddAsync(order);
        request.Status = FuelStation.PartExchange.Domain.Enums.PartRequestStatus.Matched;

        await _uow.SaveChangesAsync();

        return request;
    }
}
