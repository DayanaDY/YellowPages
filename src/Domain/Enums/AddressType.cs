using System.Runtime.Serialization;

namespace YellowPages.Domain.Enums;
public enum AddressType
{
    [EnumMember(Value = "Home")]
    Home = 0,

    [EnumMember(Value = "Office")]
    Office = 1,
}
