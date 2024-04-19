using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YellowPages.Domain.Constants;
using YellowPages.Infrastructure.Identity;

namespace YellowPages.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!_context.People.Any())
        {
            _context.People.Add(new Person
            {
                FullName = "Burns, David Goss"
            });

            await _context.SaveChangesAsync();

            var person = _context.People.First(x => x.FullName == "Burns, David Goss");

            _context.Addresses.Add(new Domain.Entities.Address
            {
                Street = "114 Hill St.", 
                City = "Sofia", 
                AddressType = Domain.Enums.AddressType.Office,
                PersonId = person.Id
            });

            _context.Addresses.Add(new Domain.Entities.Address
            {
                Street = "110 Runnymede St.",
                City = "Sofia",
                AddressType = Domain.Enums.AddressType.Home,
                PersonId = person.Id
            });

            await _context.SaveChangesAsync();

            var address1 = _context.Addresses.First(x => x.Street == "114 Hill St.");
            _context.TelephoneNumbers.Add(new Domain.Entities.TelephoneNumber
            {
                Number = "230235",
                AddressId = address1.Id
            });

            _context.TelephoneNumbers.Add(new Domain.Entities.TelephoneNumber
            {
                Number = "109736",
                AddressId = address1.Id
            });

            await _context.SaveChangesAsync();
        }
    }
}
