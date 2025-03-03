namespace DNABank.Application.Dtos;

public class CurrenAccountDto : BankAccountDto
{
    public bool HasOverdraft { get; set; }

    public decimal OverdraftLimit { get; set; }

    public override string AccountType => nameof(CurrentAccount);
}
