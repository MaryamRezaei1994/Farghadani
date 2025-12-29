using FuelStation.PartExchange.Application.DTOs;
using FuelStation.PartExchange.Application.DTOs.FileUpload;
using FuelStation.PartExchange.Application.Interfaces.General;

namespace FuelStation.PartExchange.Infrastructure.Services;

public class APIService : IAPIService
{
    public Task<ResponseDto> Get(string SourceUrl, string method, object? content, string username = "DeviceManager")
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> Post(string SourceUrl, string method, object Content, string username = "DeviceManager")
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> Put(string SourceUrl, string method, object Content, string username = "DeviceManager")
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> Delete(string SourceUrl, string method, object Content, string username = "DeviceManager")
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> PostMultiPartFormDataForImage(string sourceUrl, string method, UploadImageRequestDTO request)
    {
        throw new NotImplementedException();
    }
}