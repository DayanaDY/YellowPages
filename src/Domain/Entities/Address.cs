namespace YellowPages.Domain.Entities;

public class Address : BaseAuditableEntity
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; } 
    public AddressType? AddressType { get; set; }

    public List<TelephoneNumber> TelephoneNumbers { get; set; } = new List<TelephoneNumber>();
    public int PersonId { get; set; }
    public Person? Person { get; set; } = new Person();
}
