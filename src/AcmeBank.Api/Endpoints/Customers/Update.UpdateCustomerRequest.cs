using Microsoft.AspNetCore.Mvc;

namespace AcmeBank.Api.Endpoints.Customers
{
    public class UpdateCustomerRequest
    {
        [FromRoute]
        public int Id { get; set; }
        [FromBody]
        public required CreateCustomerRequest Body { get; set; }
    }
}
