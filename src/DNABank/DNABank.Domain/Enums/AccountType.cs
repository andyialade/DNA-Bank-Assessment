using System.Runtime.Serialization;

namespace DNABank.Domain.Enums;

public enum AccountType
{
    [EnumMember(Value = "Current")]
    Current = 1,

    [EnumMember(Value = "Savings")]
    Savings
}
