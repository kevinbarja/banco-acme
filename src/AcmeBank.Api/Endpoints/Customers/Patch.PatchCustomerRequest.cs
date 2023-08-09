using AcmeBank.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using SystemTextJsonPatch;

namespace AcmeBank.Api.Endpoints.Customers
{
    public class PatchCustomerRequest
    {
        [FromRoute]
        public int Id { get; set; }

        [FromBody]
        public required PatchDocument Body { get; set; }
    }

    public class PatchDocument
    {
        public required JsonPatchDocument<PatchCustomerDocument> JsonPatchDocument { get; set; }
    }

    public class PatchCustomerDocument
    {
        public int Id { get; set; }

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
}
