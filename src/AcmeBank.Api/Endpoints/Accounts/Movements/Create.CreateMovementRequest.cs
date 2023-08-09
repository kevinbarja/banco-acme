using Microsoft.AspNetCore.Mvc;

namespace AcmeBank.Api.Endpoints.Accounts.Movements
{
    public class CreateMovementRequest
    {
        [FromRoute]
        public int AccountId { get; set; }

        [FromBody]
        public required CreateMovementRequestBody Body { get; set; }
    }

    public class CreateMovementRequestBody
    {
        public decimal Amount { get; set; }
    }
}
