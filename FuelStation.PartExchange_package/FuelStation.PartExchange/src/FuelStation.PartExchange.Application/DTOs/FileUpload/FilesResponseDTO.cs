namespace FuelStation.PartExchange.Application.DTOs.FileUpload;

public class FilesResponseDTO
{
    public Guid objectId { get; set; }
    public List<FileUrl> urls { get; set; }
}