using DNABank.Domain.Enums;

namespace DNABank.API.Models
{
    public class AccountRequest
    {
        public string FullName { get; set; } = default!;

        public AccountType AccountType { get; set; }
    }
}
