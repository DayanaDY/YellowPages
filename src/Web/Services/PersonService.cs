using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using YellowPages.Infrastructure.Data;

namespace YellowPages.Web.Services;

public class PersonService
{
    private readonly ApplicationDbContext context;

    public PersonService(ApplicationDbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Inserts a new person into the database.
    /// </summary>
    /// <param name="personName">The full name of the person to add.</param>
    public void AddPerson(string personName)
    {
        context.Database.ExecuteSqlRaw(
            "INSERT INTO People (FullName) VALUES (@Name)",
            new SqlParameter("@Name", personName));
    }

    /// <summary>
    /// Retrieves a list of people matching the specified name filter.
    /// </summary>
    /// <param name="nameFilter">The filter pattern to match against people's names.</param>
    /// <returns>A list of people matching the filter.</returns>
    public List<Person> GetPeople(string nameFilter)
    {
        return context.People.FromSqlRaw(
            "SELECT * FROM People WHERE FullName LIKE @Name",
            new SqlParameter("@Name", $"%{nameFilter}%")).ToList();
    }

    /// <summary>
    /// Updates the full name of a person.
    /// </summary>
    /// <param name="oldName">The current name of the person.</param>
    /// <param name="newName">The new name to update to.</param>
    public void UpdatePersonName(string oldName, string newName)
    {
        context.Database.ExecuteSqlRaw(
            "UPDATE People SET FullName = @NewName WHERE FullName = @OldName",
            new SqlParameter("@NewName", newName),
            new SqlParameter("@OldName", oldName));
    }

    /// <summary>
    /// Deletes a person from the database based on their full name.
    /// </summary>
    /// <param name="nameToDelete">The name of the person to delete.</param>
    public void DeletePerson(string nameToDelete)
    {
        context.Database.ExecuteSqlRaw(
            "DELETE FROM People WHERE FullName = @Name",
            new SqlParameter("@Name", nameToDelete));
    }
}

