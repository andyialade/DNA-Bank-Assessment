namespace DNABank.Domain.Entities.Base;

public abstract class BankAccount
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = default!;

    public string AccountNumber { get; set; } = default!;

    public AccountType AccountType { get; set; }

    public bool IsFrozen { get; set; }
}
