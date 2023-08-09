using System.Text.Json.Serialization;

namespace AcmeBank.Api.Endpoints.Customers
{
    public class CreateCustomerResult : CreateCustomerRequest
    {
        public int Id { get; set; }
        [JsonIgnore]
        private new string Password { get; set; } = null!;
    }
}
