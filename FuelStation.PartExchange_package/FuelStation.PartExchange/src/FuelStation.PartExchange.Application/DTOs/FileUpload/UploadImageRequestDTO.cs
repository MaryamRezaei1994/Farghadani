using Microsoft.AspNetCore.Http;

namespace FuelStation.PartExchange.Application.DTOs.FileUpload;

public class UploadImageRequestDTO
{
    public IFormFile Image { get; set; } = null!;
    public string ClientType { get; set; } = null!;
    public string DeviceModel { get; set; } = null!;
    public Guid ObjectId { get; set; }
}