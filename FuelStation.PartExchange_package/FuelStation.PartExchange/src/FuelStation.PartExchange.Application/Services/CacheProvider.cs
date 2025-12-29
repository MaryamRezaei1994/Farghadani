using Application.Common.Statics;
using FuelStation.PartExchange.Application.Interfaces;
using StackExchange.Redis;

namespace FuelStation.PartExchange.Application.Services;

public class CacheProvider : ICacheProvider
{
    private readonly Lazy<IConnectionMultiplexer> _connection;
    private readonly IServer _server;    


    public CacheProvider()
    {
        var deltaBackOffMillisecond = Convert.ToInt32(TimeSpan.FromSeconds(5).TotalMilliseconds);

        var maxDeltaBackOffMillisecond = Convert.ToInt32(TimeSpan.FromSeconds(20).TotalMilliseconds);
        var configuration = new ConfigurationOptions()
        {
            EndPoints = { StaticData.Redis! },
            Password = StaticData.RedisPassword,
            ConnectRetry = 5,
            ReconnectRetryPolicy = new ExponentialRetry(deltaBackOffMillisecond, maxDeltaBackOffMillisecond),
            ConnectTimeout = 1000
        };
        _connection = new Lazy<IConnectionMultiplexer>(() =>
        {
            var redis = ConnectionMultiplexer.Connect(configuration);

            return redis;
        });
        _server = Database.Multiplexer.GetServer(Database.Multiplexer.GetEndPoints()[0]);

    }

    private IConnectionMultiplexer Connection => _connection.Value;

    public IDatabase Database => Connection.GetDatabase();

    public IServer Server => _server;
}