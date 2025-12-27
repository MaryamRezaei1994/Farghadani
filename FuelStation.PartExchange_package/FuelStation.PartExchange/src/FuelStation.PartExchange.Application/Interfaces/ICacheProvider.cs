using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore.Storage;

namespace FuelStation.PartExchange.Application.Interfaces;

public interface ICacheProvider
{
    IDatabase Database { get; }
    IServer Server { get; }
}