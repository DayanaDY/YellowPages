using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using YellowPages.Domain.Entities;
using YellowPages.Infrastructure.Data;

namespace YellowPages.Web.Services;

public class TelephoneNumberService
{
    private readonly ApplicationDbContext context;

    public TelephoneNumberService(ApplicationDbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Adds a new telephone number to the database.
    /// </summary>
    public void AddTelephoneNumber(string number, int addressId)
    {
        var parameters = new[]
        {
            new SqlParameter("@Number", number),
            new SqlParameter("@AddressId", addressId)
        };

        context.Database.ExecuteSqlRaw(
            "INSERT INTO TelephoneNumbers (Number, AddressId) VALUES (@Number, @AddressId)",
            parameters);
    }

    /// <summary>
    /// Retrieves all telephone numbers associated with a specific address.
    /// </summary>
    public List<TelephoneNumber> GetTelephoneNumbersByAddressId(int addressId)
    {
        return context.TelephoneNumbers.FromSqlRaw(
            "SELECT * FROM TelephoneNumbers WHERE AddressId = @AddressId",
            new SqlParameter("@AddressId", addressId)).ToList();
    }

    /// <summary>
    /// Updates an existing telephone number.
    /// </summary>
    public void UpdateTelephoneNumber(int telephoneNumberId, string newNumber)
    {
        var parameters = new[]
        {
            new SqlParameter("@NewNumber", newNumber),
            new SqlParameter("@Id", telephoneNumberId)
        };

        context.Database.ExecuteSqlRaw(
            "UPDATE TelephoneNumbers SET Number = @NewNumber WHERE Id = @Id",
            parameters);
    }

    /// <summary>
    /// Deletes a telephone number from the database.
    /// </summary>
    public void DeleteTelephoneNumber(int telephoneNumberId)
    {
        var parameters = new[]
        {
            new SqlParameter("@Id", telephoneNumberId)
        };

        context.Database.ExecuteSqlRaw(
            "DELETE FROM TelephoneNumbers WHERE Id = @Id",
            parameters);
    }
}
