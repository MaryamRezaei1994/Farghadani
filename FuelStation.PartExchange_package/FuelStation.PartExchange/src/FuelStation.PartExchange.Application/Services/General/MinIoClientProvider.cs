using Minio;
using Microsoft.Extensions.Options;

namespace FuelStation.PartExchange.Application.Services.General;

public class MinIoSetOptions
{
    public const string MinIO = "MinIO";
    public string Endpoint { get; set; } = string.Empty; // e.g. "localhost:9000"
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public bool UseSSL { get; set; } = false;
}
public class MinIoClientProvider
{
    private readonly MinioClient _client;

    public MinIoClientProvider(IOptions<MinIoSetOptions> options)
    {
        var opt = options.Value;

        _client = (MinioClient)new MinioClient()
            .WithEndpoint(opt.Endpoint)
            .WithCredentials(opt.AccessKey, opt.SecretKey)
            .WithSSL(opt.UseSSL)
            .Build();
    }

    public MinioClient GetClient() => _client;
}