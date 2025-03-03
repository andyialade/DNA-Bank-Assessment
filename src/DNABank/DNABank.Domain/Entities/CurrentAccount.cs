namespace DNABank.Domain.Entities;

public class CurrentAccount : BankAccount
{
    public bool HasOverdraft { get; set; }

    public decimal OverdraftLimit { get; set; }
}
