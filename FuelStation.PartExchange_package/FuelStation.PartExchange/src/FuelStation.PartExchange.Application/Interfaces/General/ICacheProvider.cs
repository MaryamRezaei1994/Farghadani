using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore.Storage;
using IDatabase = StackExchange.Redis.IDatabase;
using IServer = StackExchange.Redis.IServer;

namespace FuelStation.PartExchange.Application.Interfaces;

public interface ICacheProvider
{
    IDatabase Database { get; }
    IServer Server { get; }
}