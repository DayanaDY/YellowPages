using Microsoft.EntityFrameworkCore;
using YellowPages.Infrastructure.Data;

namespace YellowPages.Infrastructure.DataAccess;
public class DataAccess
{
    public void RecordData(string sqlQuery, params object[] parameters)
    {
        using (var context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>()))
        {
            context.Database.ExecuteSqlRaw(sqlQuery, parameters);
        }
    }

    public void EditData(string sqlQuery, params object[] parameters)
    {
        using (var context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>()))
        {
            context.Database.ExecuteSqlRaw(sqlQuery, parameters);
        }
    }

    public List<Person> RetrieveData(string sqlQuery, params object[] parameters)
    {
        using (var context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>()))
        {
            return context.People.FromSqlRaw(sqlQuery, parameters).ToList();
        }
    }
}
