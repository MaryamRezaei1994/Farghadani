using FuelStation.PartExchange.Application.DTOs;
using FuelStation.PartExchange.Application.DTOs.OrderPart;

namespace FuelStation.PartExchange.Application.Interfaces;

public interface IOrderPartService
{
    Task<ResponseDto> AddOrder(AddOrderRequestDto request, string username);
    Task<ResponseDto> GetOrderYId(Guid orderId, string username);
    Task<ResponseDto> DeleteOrder(DeleteOrderByIdRequest request, string username);
    Task<ResponseDto> GetAllOrders(GetAllOrdersRequest request, string username);
    Task<ResponseDto> DeleteOrderById(Guid orderId,string username);
    Task<ResponseDto> UpdateOrder(UpdateOrderRequest request, string username);
}