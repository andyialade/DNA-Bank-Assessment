namespace DNABank.Application.Dtos;

public class SavingsAccountDto : BankAccountDto
{
    public decimal InterestRate { get; set; }

    public int WithdrawCount { get; set; }

    public override string AccountType => nameof(SavingsAccount);
}
