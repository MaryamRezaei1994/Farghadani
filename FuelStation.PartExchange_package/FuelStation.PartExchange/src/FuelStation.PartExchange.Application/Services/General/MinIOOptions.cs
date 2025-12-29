namespace FuelStation.PartExchange.Application.Services.General;

public class MinIOOptions
{
    public const string MinIO = "MinIO";
    public string Endpoint { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public bool UseSSL { get; set; } = false;
}