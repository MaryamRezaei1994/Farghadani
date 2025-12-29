using System.Reactive.Linq;
using Minio;
using Application.Dto;
using FuelStation.PartExchange.Application.Common.Extension;
using FuelStation.PartExchange.Application.DTOs;
using FuelStation.PartExchange.Application.Interfaces;
using FuelStation.PartExchange.Application.Interfaces.General;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Minio.ApiEndpoints;
using Minio.DataModel;
using Minio.DataModel.Args;
using StackExchange.Redis;

namespace FuelStation.PartExchange.Application.Services.General;

public class FileService(
    IHttpContextAccessor httpContextAccessor,
    ICacheProvider cache,
    ILogger<FileService> logger,
    MinIoClientProvider minioProvider) // ← نام درست: MinIOClientProvider
    : IFileService
{
    private readonly IDatabase _cache = cache.Database;
    private readonly MinioClient _minioClient = minioProvider.GetClient();

    public async Task<FileMediaResponseDto> GetAppMultimedia(string query, string userAgent, string vendorName)
    {
        try
        {
            query = query.ToLowerInvariant();
            var clientType = GetClientType();
            var cacheKey = query.Equals("home", StringComparison.OrdinalIgnoreCase)
                ? $"{vendorName}-AppMultimedia-{query}-{userAgent}-{clientType}"
                : $"AppMultimedia-{query}-{clientType}";

            var cached = await _cache.GetAsync<List<FileMediaDto>>(cacheKey);
            if (cached is not null && cached.Count > 0)
            {
                return new FileMediaResponseDto
                {
                    StatusCode = 200,
                    Message = [],
                    Page = 1,
                    PerPage = cached.Count,
                    Total = cached.Count,
                    Result = cached
                };
            }

            var bucketName = "app-multimedia";

            // ✅ لیست کردن اشیاء با MinIO 6.0.3 + Rx.NET
            var listObjectsArgs = new ListObjectsArgs()
                .WithBucket(bucketName)
                .WithRecursive(true);

            IObservable<Item> observable = _minioClient.ListObjectsAsync(listObjectsArgs);
            var itemList = await observable.ToList(); // ← کار می‌کند چون System.Reactive.Linq نصب است

            var list = new List<FileMediaDto>();
            foreach (var item in itemList)
            {
                if (item.Key == null) continue;

                bool matchesQuery = query.Equals("home", StringComparison.OrdinalIgnoreCase)
                    ? item.Key.Contains($"{vendorName}_{query}", StringComparison.OrdinalIgnoreCase)
                    : item.Key.Contains(query, StringComparison.OrdinalIgnoreCase);

                if (!matchesQuery) continue;

                if (query.Equals("avatar", StringComparison.OrdinalIgnoreCase) &&
                    item.Key.Contains("automatestateavatar", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (item.Key.Split('-', 2) is [_, var idPart] && Guid.TryParse(idPart, out var objectId))
                {
                    var signedUrl = await GetSignedUrl(item.Key, userAgent, bucketName);
                    list.Add(new FileMediaDto
                    {
                        Id = Guid.NewGuid(),
                        DeviceModel = query,
                        ClientType = clientType,
                        ObjectId = objectId,
                        Url = signedUrl
                    });
                }
            }

            await _cache.StringSetAsync(cacheKey, list.MemoryPackSerialize(), TimeSpan.FromHours(23));

            return new FileMediaResponseDto
            {
                StatusCode = 200,
                Message = [],
                Page = 1,
                PerPage = list.Count,
                Total = list.Count,
                Result = list
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetAppMultimedia: {Query}, {Vendor}", query, vendorName);
            return new FileMediaResponseDto
            {
                StatusCode = 500,
                Message = ["Internal Server Error"],
                Page = 0,
                PerPage = 0,
                Total = 0,
                Result = []
            };
        }
    }

    public async Task<string> GetSignedUrl(string key, string userAgent, string bucketName = "device-manager")
    {
        try
        {
            // ✅ استفاده از PresignedGetObjectArgs (سبک جدید MinIO 6.x)
            var args = new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(key)
                .WithExpiry(3600); // 1 ساعت به ثانیه

            var presignedUrl = await _minioClient.PresignedGetObjectAsync(args);

            if (userAgent.Equals("ios", StringComparison.OrdinalIgnoreCase))
            {
                var uri = new Uri(presignedUrl);
                return $"{uri.Scheme}://storage.iotappest.com{uri.PathAndQuery}";
            }

            return presignedUrl;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error generating signed URL for key: {Key}", key);
            return string.Empty;
        }
    }

    private string GetClientType()
    {
        if (httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("Client-Type", out var clientType) == true)
        {
            return clientType.FirstOrDefault() ?? "APP";
        }
        return "APP";
    }
}