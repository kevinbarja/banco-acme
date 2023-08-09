using AcmeBank.Persistence;
using System.ComponentModel.DataAnnotations;

namespace AcmeBank.Api.Endpoints.Accounts
{
    public class CreateAccountRequest
    {
        [MaxLength(ConfigConstants.SMALL_LENGTH)]
        public string Number { get; set; } = null!;

        public AccountType Type { get; set; }

        [Range(0, int.MaxValue)]
        public decimal InitialBalance { get; set; }

        public bool IsActive { get; set; }

        [Range(0, int.MaxValue)]
        public int CustomerId { get; set; }
    }

    public enum AccountType
    {
        Savings,
        Checking
    }
}
