using System.Runtime.Serialization;

namespace YellowPages.Domain.Enums;
public enum AddressType
{
    [EnumMember(Value = "Home Address")]
    Home = 0,

    [EnumMember(Value = "Business Address")]
    Business = 1,
}
