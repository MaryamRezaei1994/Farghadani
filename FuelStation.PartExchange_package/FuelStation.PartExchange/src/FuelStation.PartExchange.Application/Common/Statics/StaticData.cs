using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Application.Common.Statics;

public static class StaticData
{
    private readonly static JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public static string ToJson(object data)
    {
        try
        {
            return JsonSerializer.Serialize(data, JsonOptions);
        }
        catch (Exception)
        {
            return "NOErrorConvertObjectToJson";
        }
    }

    public static string DatabaseEncryptionKey { get; set; } = "SgVkYp3s6v9y$B&E)H@McQfThWmZq4t7";
    public static string ReminderDatabaseEncryptionKey { get; set; } = "SgVkYp3s6v9y$B&E)H@McQfThWmZq4t7";
    public static string WashingMachineDatabaseEncryptionKey { get; set; } = "SgVkYp3s6v9y$B&E)H@McQfThWmZq4t7";
    public static string ScenarioDatabaseEncryptionKey { get; set; } = "SgVkYp3s6v9y$B&E)H@McQfThWmZq4t7";
    // JWT handled by API Gateway; key removed from app static config
    public static string? All_PostgreSqlCon { get; set; }
    public static string? WM_PostgreSqlCon { get; set; }
    public static string? Redis { get; set; }
    public static string? RedisUsername { get; set; }
    public static string? RedisPassword { get; set; }
    public static string? Kafka { get; set; }
    public static string? KafkaLogTopic { get; set; }
    public static string? LWAALLUrl { get; set; }
    public static string? ScenarioServiceUrl { get; set; }
    public static string NotificationURL { get; set; }
    public static string? WMUrl { get; set; }
    public static bool? IsDevelopment { get; set; } = false;
    public static bool? IsRequiredFeedback { get; set; } = true;
    public static string? RedisTransactionUrl { get; set; }
    public static int ServicePort { get; set; }
    public static string version { get; set; } = "0.9.4.0";
    public static string? AddressUrl { get; set; }
    public static string? FileServiceUrl { get; set; }
    public static string? OutputRelationUrl { get; set; }
    public static string? Hour { get; set; }
    public static string? Minute { get; set; }
    public static string? Enviroment { get; set; }
    public static string? OutputRelationApiKey { get; set; }
    public static string? NotificationServiceApiKey { get; set; }
    public static string DeviceMonitorUrl { get; set; } = string.Empty;
    public static string RabbitMqUsername { get; set; } = string.Empty;
    public static string RabbitMqPassword { get; set; } = string.Empty;
    public static string RabbitMqHost { get; set; } = string.Empty;
    public static ushort RabbitMqPort { get; set; } = 0;


    public static string? CephAddress { get; set; }
    public static string? CephAccessKey { get; set; }
    public static string? CephSecretKey { get; set; }

    public static int DefaultAppPort { get; set; }
    public static int DefaultDevicePort { get; set; }


    public static void Config(IConfiguration config)
    {
#if DEBUG
        ServicePort = 1011;
        Hour = "3";
        Minute = "30";

        var currentConfig = config!.GetSection("Environment")!.Value!.Trim().ToUpper();

        switch (currentConfig)
        {
            case "DEVELOPMENT":
                //Rezaei
                All_PostgreSqlCon = "Server=127.0.0.1;Port=5432;Database=postgres;User Id= postgres;Password=Abc@1234;Include Error Detail=true";
                //other
                //All_PostgreSqlCon = "Server=127.0.0.1;Port=5433;Database=IOT_DB;User Id= postgres;Password=159236;Include Error Detail=true";
                Redis = "192.168.0.245:6379";
                RedisUsername = "iotuser";
                RedisPassword = "da0bd9b20c62de8ade2685d1a4c8c473ccc0586353ec5e1392ccc2da479143a3737b531add5980de";
                Kafka = "192.168.1.75:9095";
                KafkaLogTopic = "log-api-service-dev";
                RedisTransactionUrl = "http://192.168.1.165:6380/api/";
                ScenarioServiceUrl = "http://localhost:5127/api/";
                NotificationURL = "http://localhost:5127/api/";
                LWAALLUrl = "http://192.168.0.231:3030/api/";
                WMUrl = "http://192.168.0.231:5050/api/";
                RabbitMqUsername = "guest";
                RabbitMqUsername = "guest";
                RabbitMqHost = "localhost";
                RabbitMqPort = 49154;
                IsDevelopment = true;
                AddressUrl = "http://192.168.1.165:4557/api/";
                FileServiceUrl = "http://192.168.1.165:8571/api/";
                OutputRelationUrl = "http://192.168.1.166:5308/api/";
                OutputRelationApiKey = "123456";
                DeviceMonitorUrl = "http://192.168.1.165:4451/api/";
                Enviroment = "DEV";
                break;
            case "TEST":
                All_PostgreSqlCon =
                    "Server=192.168.0.247;Port=5434;Database=IOT_DB;User Id= postgres;Password=Abc@1234;Include Error Detail=true";
                //All_PostgreSqlCon = "Host=192.168.159.51:5433,192.168.159.62:5433;Database=citus;Username=postgres;";
                WM_PostgreSqlCon =
                    "Server=192.168.0.247;Port=5434;Database=WM-Project;User Id= postgres;Password=Abc@1234;";
                Redis = "192.168.0.245:6379";
                RedisUsername = "iotuser";
                RedisPassword = "da0bd9b20c62de8ade2685d1a4c8c473ccc0586353ec5e1392ccc2da479143a3737b531add5980de";
                Kafka = "192.168.1.75:9095";
                KafkaLogTopic = "log-api-service-dev";
                DeviceMonitorUrl = "http://192.168.1.165:4451/api/";
                RedisTransactionUrl = "http://192.168.1.165:6380/api/";
                LWAALLUrl = "http://192.168.0.231:3030/api/";
                WMUrl = "http://192.168.1.165:5055/api/";
                IsDevelopment = true;
                IsRequiredFeedback = true;
                NotificationURL = "http://localhost:5127/api/";
                AddressUrl = "http://192.168.1.165:4557/api/";
                FileServiceUrl = "http://192.168.1.165:8571/api/";
                ScenarioServiceUrl = "http://192.168.1.165:1616/";
                OutputRelationUrl = "http://192.168.166:5308/api/";
                OutputRelationApiKey = "123456";
                Enviroment = "DEV";
                Hour = "14";
                Minute = "40";
                CephAddress = "https://storage.smart-boom.com";
                CephAccessKey = "DKN2BZNZ1ALGE32H505Q";
                CephSecretKey = "SuT27hch9ZYopDsIhJ2xnEoXtxqTUAlCOVZCfSWF";
                RabbitMqUsername = "guest";
                RabbitMqPassword = "guest";
                RabbitMqHost = "localhost";
                RabbitMqPort = 5672;
                break;
            case "RELEASE":
                All_PostgreSqlCon =
                    "Server=10.10.2.29;Port=5432;Database=IOT_DB;User Id= postgres;Password=Qweasd123;";
                WM_PostgreSqlCon =
                    "Server=10.10.2.29;Port=5432;Database=LWA-WM-DB;User Id= postgres;Password=Qweasd123;";
                Redis = "10.10.2.29:6379";
                Kafka = "192.168.1.75:9095";
                KafkaLogTopic = "log-api-service-dev";
                RedisTransactionUrl = "http://10.10.2.29:6380/api/";
                LWAALLUrl = "http://10.10.2.29:3030/api/";
                WMUrl = "http://10.10.2.29:5050/api/";
                IsDevelopment = false;
                IsRequiredFeedback = true;
                NotificationURL = "http://localhost:5127/api/";
                AddressUrl = "http://10.10.2.29:4557/api/";
                FileServiceUrl = "";
                OutputRelationUrl = "http://192.168.1.166:5308/api/";
                Enviroment = "REL";
                break;
            case "SQA":
                All_PostgreSqlCon =
                    "Server=192.168.1.76;Port=5434;Database=IOT_DB;User Id= postgres;Password=Abc@1234;";
                WM_PostgreSqlCon =
                    "Server=192.168.1.76;Port=5434;Database=IOT_DB;User Id= postgres;Password=Abc@1234;";
                Redis = "192.168.1.75:6379";
                RedisPassword = "da0bd9b20c62de8ade2685d1a4c8c473ccc0586353ec5e1392ccc2da479143a3737b531add5980de";
                Kafka = "192.168.1.75:9095";
                KafkaLogTopic = "log-api-service-sqa";
                RedisTransactionUrl = "http://192.168.1.166:6380/api/";
                LWAALLUrl = "http://192.168.1.80:3030/api/";
                WMUrl = "http://192.168.1.166:5055/api/";
                IsDevelopment = true;
                IsRequiredFeedback = false;
                OutputRelationApiKey = "";
                DeviceMonitorUrl = "http://192.168.1.166:4451/api/";
                AddressUrl = "http://192.168.1.166:4557/api/";
                NotificationURL = "http://localhost:5127/api/";
                FileServiceUrl = "http://192.168.1.166:8571/api/";
                OutputRelationUrl = "http://192.168.1.166:5308/api/";
                ScenarioServiceUrl = "http://192.168.1.166:1616/";
                CephAddress = "https://storage.smart-boom.com";
                CephAccessKey = "DKN2BZNZ1ALGE32H505Q";
                CephSecretKey = "SuT27hch9ZYopDsIhJ2xnEoXtxqTUAlCOVZCfSWF";
                RabbitMqUsername = "guest";
                RabbitMqPassword = "guest";
                RabbitMqHost = "localhost";
                RabbitMqPort = 5672;
                Enviroment = "SQA";
                break;
            case "SQA2":
                All_PostgreSqlCon =
                    "Server=192.168.1.80;Port=5432;Database=IOT_DB;User Id= postgres;Password=Abc@1234;";
                WM_PostgreSqlCon =
                    "Server=192.168.1.76;Port=5432;Database=IOT_DB;User Id= postgres;Password=Abc@1234;";
                Redis = "192.168.1.166:6379";
                RedisTransactionUrl = "http://192.168.1.166:6380/api/";
                LWAALLUrl = "http://192.168.1.166:3030/api/";
                WMUrl = "http://192.168.1.166:5050/api/";
                IsDevelopment = true;
                IsRequiredFeedback = false;
                NotificationURL = "http://localhost:5127/api/";
                AddressUrl = "http://192.168.1.165:4557/api/";
                FileServiceUrl = "http://192.168.1.166:8571/api/";
                OutputRelationUrl = "http://192.168.1.166:4014/api/";
                Enviroment = "SQA";
                break;
            case "PR":
                All_PostgreSqlCon =
                    "Server=192.168.159.23;Port=5432;Database=LWAAll-DeviceManager;User Id= postgres;Password=Abc@1234;";
                WM_PostgreSqlCon =
                    "Server=192.168.159.23;Port=5432;Database=LWA-WM-DB;User Id= postgres;Password=Abc@1234;";
                Redis = "192.168.159.23:6379";
                RedisTransactionUrl = "http://192.168.159.23:6380/api/";
                LWAALLUrl = "http://192.168.0159.23:3030/api/";
                WMUrl = "http://192.168.0159.23:5050/api/";
                NotificationURL = "http://192.168.1.165:4328/api/";
                IsDevelopment = false;
                IsRequiredFeedback = false;
                FileServiceUrl = "http://192.168.1.166:8571/api/";
                OutputRelationUrl = "http://192.168.1.165:4014/api/";
                Enviroment = "SQA";
                break;
            default:
                break;
        }
#else
                // JwtSecurityKey handled by API Gateway; nothing to set here
                DatabaseEncryptionKey = System.Environment.GetEnvironmentVariable("DB_ENCRYPTION_KEY");
                ReminderDatabaseEncryptionKey = System.Environment.GetEnvironmentVariable("REMINDER_DB_ENCRYPTION_KEY");
                WashingMachineDatabaseEncryptionKey = System.Environment.GetEnvironmentVariable("WASHING_MACHINE_DB_ENCRYPTION_KEY");
                ScenarioDatabaseEncryptionKey = System.Environment.GetEnvironmentVariable("SCENARIO_DB_ENCRYPTION_KEY");
                All_PostgreSqlCon = System.Environment.GetEnvironmentVariable("DB_PG_CONNECTION");
                WM_PostgreSqlCon = System.Environment.GetEnvironmentVariable("DB_PG_CONNECTION");
                Redis = System.Environment.GetEnvironmentVariable("DB_REDIS_CONNECTION");
                RedisUsername = System.Environment.GetEnvironmentVariable("DB_REDIS_USERNAME");
                RedisPassword = System.Environment.GetEnvironmentVariable("DB_REDIS_PASSWORD");
                Kafka = System.Environment.GetEnvironmentVariable("KAFKA_CONNECTION");
                KafkaLogTopic = System.Environment.GetEnvironmentVariable("KAFKA_LOG_TOPIC");
                RedisTransactionUrl = System.Environment.GetEnvironmentVariable("SERVICE_REDIS");
                LWAALLUrl = System.Environment.GetEnvironmentVariable("SERVICE_DEVICEMANAGER");
                WMUrl = System.Environment.GetEnvironmentVariable("SERVICE_WASHINGMACHINE");
                IsDevelopment = Boolean.Parse(System.Environment.GetEnvironmentVariable("IS_DEVELOPMENT")!);
                IsRequiredFeedback = Boolean.Parse(System.Environment.GetEnvironmentVariable("IS_REQUIRED_FEEDBACK")!);
                ServicePort = int.Parse( System.Environment.GetEnvironmentVariable("SERVICE_PORT")!);
                version = System.Environment.GetEnvironmentVariable("VERSION");
                AddressUrl = System.Environment.GetEnvironmentVariable("SERIVICE_ADDRESS");
                FileServiceUrl = System.Environment.GetEnvironmentVariable("SERVICE_FILE_UPLOAD");
                OutputRelationUrl = System.Environment.GetEnvironmentVariable("SERVICE_OUT_REL"); 
                Hour = System.Environment.GetEnvironmentVariable("SERVICE_JOB_HOUR"); 
                Minute = System.Environment.GetEnvironmentVariable("SERVICE_JOB_MINUTE"); 
                Enviroment = System.Environment.GetEnvironmentVariable("SERVICE_ENV");
                NotificationURL=System.Environment.GetEnvironmentVariable("SERVICE_NOTIFICATION");
                ScenarioServiceUrl = System.Environment.GetEnvironmentVariable("SERVICE_SCENARIO");
                OutputRelationApiKey = System.Environment.GetEnvironmentVariable("OUTPUT_RELATION_API_KEY");
                NotificationServiceApiKey = System.Environment.GetEnvironmentVariable("NOTIFICATION_SERVICE_API_KEY");
                CephAddress = System.Environment.GetEnvironmentVariable("CEPH_ADDRESS");
                CephAccessKey = System.Environment.GetEnvironmentVariable("CEPH_ACCESS_KEY");
                CephSecretKey = System.Environment.GetEnvironmentVariable("CEPH_SECRET_KEY");
                DeviceMonitorUrl=System.Environment.GetEnvironmentVariable("SERVICE_DEVICE_MONITOR");
                RabbitMqUsername=System.Environment.GetEnvironmentVariable("RABBITMQ_USERNAME");
                RabbitMqPassword=System.Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
                RabbitMqHost=System.Environment.GetEnvironmentVariable("RABBITMQ_HOST");
                RabbitMqPort=ushort.Parse(System.Environment.GetEnvironmentVariable("RABBITMQ_PORT") ?? "9000");
                DefaultAppPort =int.Parse( System.Environment.GetEnvironmentVariable("DEFAULT_APP_PORT"));
                DefaultDevicePort =int.Parse( System.Environment.GetEnvironmentVariable("DEFAULT_DEVICE_PORT"));

#endif
    }
}