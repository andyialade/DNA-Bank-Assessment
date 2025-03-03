namespace DNABank.Application.Validators;

public class CurrentAccountValidator : AbstractValidator<CurrentAccount>
{
    public CurrentAccountValidator()
    {
        RuleFor(v => v.FullName).NotEmpty();
    }
}
