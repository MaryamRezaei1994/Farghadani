using FuelStation.PartExchange.Application.DTOs;
using FuelStation.PartExchange.Application.DTOs.OrderPart;

namespace FuelStation.PartExchange.Application.Interfaces;

public interface IOrderPartService
{
    Task<ResponseDto> AddOrder(AddOrderRequestDto request, string username);
    Task<ResponseDto> GetOrderById(Guid orderId, string username);
    Task<ResponseDto> GetAllOrders(GetAllOrdersRequestDto requestDto, string username);
    Task<ResponseDto> DeleteOrderById(Guid orderId,string username);
    Task<ResponseDto> UpdateOrder(UpdateOrderRequestDto requestDto, string username);
    Task<ResponseDto> ConfirmOrder(Guid orderId, string username);
}