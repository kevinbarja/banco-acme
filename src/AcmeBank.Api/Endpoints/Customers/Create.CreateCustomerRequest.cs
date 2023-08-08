using AcmeBank.Persistence;
using System.ComponentModel.DataAnnotations;

namespace AcmeBank.Api.Endpoints.Customers
{
    public class CreateCustomerRequest
    {
        [MaxLength(ConfigConstants.MEDIUM_LENGTH)]
        public string FullName { get; set; } = null!;

        public Gender Gender { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        [Range(1, int.MaxValue)]
        public int IdentityNumber { get; set; }

        [MaxLength(ConfigConstants.LARGE_LENGTH)]
        public string? Address { get; set; }

        [MaxLength(ConfigConstants.SMALL_LENGTH)]
        public string? PhoneNumber { get; set; }

        [MaxLength(ConfigConstants.MEDIUM_LENGTH)]
        public string Password { get; set; } = null!;

        public bool IsActive { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
