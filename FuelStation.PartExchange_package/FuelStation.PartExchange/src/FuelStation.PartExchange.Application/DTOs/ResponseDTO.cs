using System.Net;

namespace FuelStation.PartExchange.Application.DTOs;

public class ResponseDto
{
    public ResponseDto()
    {
    }

    public ResponseDto(int statusCode, string message, object? result = null, int total = 0, int page = 1,
        int perPage = 10)
    {
        this.StatusCode = statusCode;
        this.Message = message.Split(",").ToList();
        this.Result = result;
        this.Total = total;
        this.Page = page;
        this.PerPage = perPage;
    }
    
    public static ResponseDto Success(object data) => new(200, "Ok", data);
    
    public static ResponseDto InternalServerError(string message) => new()
    {
        StatusCode = (int)HttpStatusCode.InternalServerError,
        Message = [message]
    };


    public int StatusCode { get; set; }
    public List<string> Message { get; set; } = null!;
    public int Total { get; set; } = 0;
    public int Page { get; set; } = 1;
    public int PerPage { get; set; } = 10;
    public object? Result { get; set; }
}