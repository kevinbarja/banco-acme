using AcmeBank.Contracts;
using AcmeBank.Persistence.Entities;
using Ardalis.ApiEndpoints;
using BackendData.Security;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace AcmeBank.Api.Endpoints.Customers
{
    public class Create : EndpointBaseAsync
            .WithRequest<CreateCustomerRequest>
            .WithActionResult<CreateCustomerResult>
    {
        private readonly IAsyncRepository<Customer> _repository;
        private readonly IPasswordHasher _passwordHasher;

        public Create(IAsyncRepository<Customer> repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///       "fullName": "Juan Perez",
        ///       "gender": "Male",
        ///       "age": 60,
        ///       "identityNumber": 2147483647,
        ///       "address": "Barrio 13 de Enero",
        ///       "phoneNumber": "07563256",
        ///       "password": "123",
        ///       "isActive": true
        ///     }
        /// 
        /// </remarks>
        /// <response code = "201" >Returns customer created</response>
        /// <response code = "400" >Bad request</response>  
        /// <returns></returns>
        [HttpPost("customers")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(OperationId = "CreateCustomer", Tags = new[] { "Customers" })]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCustomerResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public override async Task<ActionResult<CreateCustomerResult>> HandleAsync(
                [FromBody] CreateCustomerRequest request,
                CancellationToken cancellationToken)
        {



            var customer = new Customer();
            customer.FullName = request.FullName;
            customer.Gender = 1;
            customer.Age = 28;
            customer.IdentityNumber = 89357122;
            customer.Address = null;
            customer.PhoneNumber = null;
            customer.Password = "123";
            customer.Status = 1;

            var result = await _repository.AddAsync(customer, cancellationToken);

            var res2 = new CreateCustomerResult();
            return CreatedAtRoute("customers", new { id = result.Id }, res2);
        }
    }
}
