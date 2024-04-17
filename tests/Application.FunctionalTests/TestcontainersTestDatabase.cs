using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;
using YellowPages.Application.FunctionalTests;
using YellowPages.Infrastructure.Data;

namespace YellowPages.Application.FunctionalTests;

public class TestcontainersTestDatabase : ITestDatabase
{
    private readonly PostgreSqlContainer _container; // MsSqlContainer
    private NpgsqlConnection _connection = null!;
    private string _connectionString = null!;
    private Respawner _respawner = null!;

    public TestcontainersTestDatabase()
    {
        _container = new PostgreSqlBuilder() // MsSqlBuilder
            .WithAutoRemove(true)
            .Build();
    }

    public async Task InitialiseAsync()
    {
        await _container.StartAsync();

        _connectionString = _container.GetConnectionString();

        _connection = new NpgsqlConnection(_connectionString);
        await _connection.OpenAsync(); // Added

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_connectionString) // UseSqlServer
            .Options;

        var context = new ApplicationDbContext(options);

        await context.Database.MigrateAsync(); // Modified

        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres, // Added
            SchemasToInclude = ["public"], // Added
            TablesToIgnore = ["__EFMigrationsHistory"] // (Optional) Equal to: new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
        });
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connection); // await _respawner.ResetAsync(_connectionString);
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _container.DisposeAsync();
    }
}
