using System.Text.Json.Serialization;

namespace DNABank.Application.Dtos;

[JsonDerivedType(typeof(CurrenAccountDto))]
[JsonDerivedType(typeof(SavingsAccountDto))]
public abstract class BankAccountDto
{
    public string FullName { get; set; } = default!;

    public string AccountNumber { get; set; } = default!;

    public abstract string AccountType { get; }

    public bool IsFrozen { get; set; }
}
