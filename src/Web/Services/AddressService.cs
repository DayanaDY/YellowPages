using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using YellowPages.Domain.Entities;
using YellowPages.Domain.Enums;
using YellowPages.Infrastructure.Data;

namespace YellowPages.Web.Services;

public class AddressService
{
    private readonly ApplicationDbContext context;

    public AddressService(ApplicationDbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Adds a new address to the database.
    /// </summary>
    public void AddAddress(Address address)
    {
        var addressType = address.AddressType;
        var parameters = new[]
        {
            new SqlParameter("@Street", address.Street),
            new SqlParameter("@City", address.City),
            new SqlParameter("@AddressType", (int)addressType!),
            new SqlParameter("@PersonId", address.PersonId)
        };

        context.Database.ExecuteSqlRaw(
            "INSERT INTO Addresses (Street, City, AddressType, PersonId) VALUES (@Street, @City, @AddressType, @PersonId)",
            parameters);
    }

    /// <summary>
    /// Retrieves all addresses associated with a specific person.
    /// </summary>
    public List<Address> GetAddressesByPersonId(int personId)
    {
        return context.Addresses.FromSqlRaw(
            "SELECT * FROM Addresses WHERE PersonId = @PersonId",
            new SqlParameter("@PersonId", personId)).ToList();
    }

    /// <summary>
    /// Updates an existing address.
    /// </summary>
    public void UpdateAddress(int addressId, int personId, string newStreet, string newCity, AddressType newAddressType)
    {
        var parameters = new[]
        {
            new SqlParameter("@Street", newStreet),
            new SqlParameter("@City", newCity),
            new SqlParameter("@AddressType", (int)newAddressType),
            new SqlParameter("@PersonId", personId),
            new SqlParameter("@AddressId", addressId)
        };

        context.Database.ExecuteSqlRaw(
            "UPDATE Addresses SET Street = @Street, City = @City, AddressType = @AddressType WHERE PersonId = @PersonId AND AddressId = @AddressId",
            parameters);
    }

    /// <summary>
    /// Deletes an address.
    /// </summary>
    public void DeleteAddress(int personId, int addressId)
    {
        var parameters = new[]
        {
            new SqlParameter("@PersonId", personId),
            new SqlParameter("@AddressId", addressId)
        };

        context.Database.ExecuteSqlRaw(
            "DELETE FROM Addresses WHERE PersonId = @PersonId AND AddressId = @AddressId",
            parameters);
    }
}
