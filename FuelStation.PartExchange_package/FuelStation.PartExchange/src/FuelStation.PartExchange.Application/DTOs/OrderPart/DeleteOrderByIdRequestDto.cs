using System.ComponentModel.DataAnnotations;
using Application.Common.Statics;
using FuelStation.PartExchange.Application.Common.Statics;

namespace FuelStation.PartExchange.Application.DTOs.OrderPart;

public abstract class DeleteOrderByIdRequestDto
{
    [Required(ErrorMessage = "The Id field is required")]
    [RegularExpression(RegularExpressions.InvalidGuid, ErrorMessage = "The Id as guid not valid")]
    public Guid Id { get; set; }
}