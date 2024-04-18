using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YellowPages.Application.Common.Helpers;
public class CustomResult : IActionResult
{
    private readonly List<Person> _people;

    public CustomResult(List<Person> people)
    {
        _people = people;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        response.ContentType = "text/plain"; // Set the content type to plain text

        foreach (var person in _people)
        {
            await response.WriteAsync($"Name: {person.FullName}\n");
            if (person.Addresses != null && person.Addresses.Any())
            {
                foreach (var address in person.Addresses)
                {
                    await response.WriteAsync($"{"Office Address:"} {address.Street}, {address.City}, {address.PostalCode}\n");
                    foreach (var phoneNumber in address.TelephoneNumbers)
                    {
                        await response.WriteAsync($"Tel: {phoneNumber.Number}\n");
                    }
                }
            }
        }
    }
}
