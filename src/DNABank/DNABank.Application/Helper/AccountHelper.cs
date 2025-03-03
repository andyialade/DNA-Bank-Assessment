using System.Runtime.Serialization;

namespace DNABank.Application.Helper;

public class AccountHelper : IAccountHelper
{
    public string GenerateAccountNumber()
    {
        var random = new Random();
        return random.Next(10000000, 100000000).ToString();
    }

    public string GetAccountType(AccountType accountType)
    {
        var type = accountType.GetType();
        var memberInfo = type.GetMember(accountType.ToString());
        if (memberInfo.Length > 0)
        {
            var attributes = memberInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);
            if (attributes.Length > 0)
            {
                return ((EnumMemberAttribute)attributes[0]).Value;
            }
        }
        return "N/A";
    }
}
