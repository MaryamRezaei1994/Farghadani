using FuelStation.PartExchange.Application.DTOs;
using FuelStation.PartExchange.Application.DTOs.FileUpload;

namespace FuelStation.PartExchange.Application.Interfaces.General;

public interface IAPIService
{
    Task<ResponseDto> Get(string SourceUrl, string method, object? content, string username = "DeviceManager");
    Task<ResponseDto> Post(string SourceUrl, string method, object Content, string username = "DeviceManager");
    Task<ResponseDto> Put(string SourceUrl, string method, object Content, string username = "DeviceManager");
    Task<ResponseDto> Delete(string SourceUrl, string method, object Content, string username = "DeviceManager");
    Task<ResponseDto> PostMultiPartFormDataForImage(string sourceUrl, string method, UploadImageRequestDTO request);
}