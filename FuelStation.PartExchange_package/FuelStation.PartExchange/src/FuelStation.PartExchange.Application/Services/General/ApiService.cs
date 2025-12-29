using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Application.Common.Extension;
using FuelStation.PartExchange.Application.DTOs;
using FuelStation.PartExchange.Application.DTOs.FileUpload;
using FuelStation.PartExchange.Application.Interfaces.General;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FuelStation.PartExchange.Application.Services.General;

public class ApiService(
    IHttpClientFactory httpClientFactory,
    ILogger<ApiService> logService,
    IHttpContextAccessor httpContextAccessor)
    : IAPIService
{
    public async Task<ResponseDto> Get(string sourceUrl, string method, object? content, string username)
    {
        try
        {
            var client = httpClientFactory.CreateClient();
            var language = "";
            var clientType = "";
            if (httpContextAccessor.HttpContext != null)
            {
                language = httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();
                clientType = httpContextAccessor.HttpContext.Request.Headers["Client-Type"].ToString();
            }
                

            client.DefaultRequestHeaders.Add("username",
                String.IsNullOrEmpty(username) ? "DeviceManager" : username.ToUpper());
            client.DefaultRequestHeaders.Add("Accept-Language",
                String.IsNullOrEmpty(language) ? "EN" : language.ToUpper());
            client.DefaultRequestHeaders.Add("Client-Type",
                String.IsNullOrEmpty(clientType) ? "APP" : clientType.ToUpper());
            var response = await client.GetAsync(sourceUrl + method + GetQueryString(content));
            var responseBody = await response.Content.ReadAsStringAsync();
            var values = responseBody.Deserialize<ResponseDto>();
            return values;
        }
        catch (Exception e)
        {
            logService.LogError(e.ToString());
            var response = new ResponseDto {
                StatusCode = 500, Message = ["Internal Server Error"]
            };
            return response;
        }
    }

    private string GetQueryString(object? obj)
    {
        if (obj == null)
            return "";
        var properties = from p in obj.GetType().GetProperties()
            where p.GetValue(obj, null) != null
            select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

        return "?" + String.Join("&", properties.ToArray());
    }

    public async Task<ResponseDto> Post(string sourceUrl, string method, object content, string username)
    {
        try
        {
            var client = httpClientFactory.CreateClient();
            var language = "";
            if (httpContextAccessor.HttpContext != null)
                language = httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();

            client.DefaultRequestHeaders.Add("username",
                String.IsNullOrEmpty(username) ? "DeviceManager" : username.ToUpper());
            client.DefaultRequestHeaders.Add("Accept-Language",
                String.IsNullOrEmpty(language) ? "EN" : language.ToUpper());
            var contentJson = content.Serialize();
            HttpContent request = new StringContent(contentJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(sourceUrl + method, request);
            var values = (await response.Content.ReadAsStringAsync()).Deserialize<ResponseDto>();

            return values;
        }
        catch (Exception e)
        {
            logService.LogError(e.ToString());
            var response = new ResponseDto {
                StatusCode = 500, Message = ["Internal Server Error"]
            };
            return response;
        }
    }

    public async Task<ResponseDto> Put(string sourceUrl, string method, object content, string username)
    {
        try
        {
            var client = httpClientFactory.CreateClient();
            var language = "";
            if (httpContextAccessor.HttpContext != null)
                language = httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();

            client.DefaultRequestHeaders.Add("Accept-Language",
                String.IsNullOrEmpty(language) ? "EN" : language.ToUpper());
            client.DefaultRequestHeaders.Add("username",
                String.IsNullOrEmpty(username) ? "DeviceManager" : username.ToUpper());
            HttpContent request = new StringContent(content.Serialize(), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(sourceUrl + method, request);
            var values = (await response.Content.ReadAsStringAsync()).Deserialize<ResponseDto>();

            return values;
        }
        catch (Exception e)
        {
            logService.LogError(e.ToString());
            var response = new ResponseDto {
                StatusCode = 500, Message = ["Internal Server Error"]
            };
            return response;
        }
    }

    public async Task<ResponseDto> Delete(string sourceUrl, string method, object content, string username)
    {
        try
        {
            var client = httpClientFactory.CreateClient();
            var language = "";
            if (httpContextAccessor.HttpContext != null)
                language = httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();

            client.DefaultRequestHeaders.Add("Accept-Language",
                String.IsNullOrEmpty(language) ? "EN" : language.ToUpper());
            client.DefaultRequestHeaders.Add("username",
                String.IsNullOrEmpty(username) ? "DeviceManager" : username.ToUpper());
            var url = sourceUrl + method + GetQueryString(content);
            var response = await client.DeleteAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            var values = responseBody.Deserialize<ResponseDto>();
            return values;
        }
        catch (Exception e)
        {
            logService.LogError(e.ToString());
            var response = new ResponseDto {
                StatusCode = 500, Message = ["Internal Server Error"]
            };
            return response;
        }
    }

    public async Task<ResponseDto> PostMultiPartFormDataForImage(string sourceUrl, string method,
        UploadImageRequestDTO request)
    {
        try
        {
            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(request.ClientType), "ClientType");
                content.Add(new StringContent(request.DeviceModel), "DeviceModel");
                content.Add(new StringContent(request.ObjectId.ToString()), "ObjectId");

                content.Add(new StreamContent(request.Image.OpenReadStream()) {
                    Headers = {
                        ContentLength = request.Image.Length,
                        ContentType = new MediaTypeHeaderValue(request.Image.ContentType)
                    }
                }, "Image", request.Image.FileName);

                var result = await client.PostAsync(sourceUrl + method, content);
                var temp = await result.Content.ReadAsStringAsync();
                var values = (await result.Content.ReadAsStringAsync()).Deserialize<ResponseDto>();


                return values;
            }
        }
        catch (Exception e)
        {
            logService.LogError(e.ToString());
            var response = new ResponseDto {
                StatusCode = 500,
                Message = ["Internal Server Error"]
            };
            return response;
        }
    }
}