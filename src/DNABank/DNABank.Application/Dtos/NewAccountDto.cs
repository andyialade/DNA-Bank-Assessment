namespace DNABank.Application.Dtos;

public class NewAccountDto
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; } = default!;

    public string AccountType { get; set; } = default!;
}
