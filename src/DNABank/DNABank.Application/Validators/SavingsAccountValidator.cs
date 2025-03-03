namespace DNABank.Application.Validators;

public class SavingsAccountValidator : AbstractValidator<SavingsAccount>
{
    public SavingsAccountValidator()
    {
        RuleFor(v => v.FullName).NotEmpty();
    }
}
