namespace YellowPages.Domain.Entities;
public class TelephoneNumber : BaseAuditableEntity
{
    public string? Number { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; } = new Address();
}
