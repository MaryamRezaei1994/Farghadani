using Application.Dto;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.General;

public interface IFileService
{
    Task<FileMediaResponseDto> GetAppMultimedia(string query, string userAgent, string vendorName);
    Task<string> GetSignedUrl(string key, string userAgent, string bucketName = "device-manager");

}