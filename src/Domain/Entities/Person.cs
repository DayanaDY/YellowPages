using System.Net;

public class Person : BaseAuditableEntity
{
    public string? FullName { get; set; }
    public List<Address>? Addresses { get; set; } = new List<Address>();
}
