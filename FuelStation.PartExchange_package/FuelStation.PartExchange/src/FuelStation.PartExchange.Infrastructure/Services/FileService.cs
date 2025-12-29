using Application.Dto;
using FuelStation.PartExchange.Application.Interfaces.General;

namespace FuelStation.PartExchange.Infrastructure.Services;

public class FileService : IFileService
{
    public Task<FileMediaResponseDto> GetAppMultimedia(string query, string userAgent, string vendorName)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetSignedUrl(string key, string userAgent, string bucketName = "device-manager")
    {
        throw new NotImplementedException();
    }
}