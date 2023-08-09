using AcmeBank.Contracts;
using AcmeBank.Persistence.Entities;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace AcmeBank.Api.Endpoints.Customers
{
    public class Create : EndpointBaseAsync
            .WithRequest<CreateCustomerRequest>
            .WithActionResult<CreateCustomerResult>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Customer> _repository;

        public Create(
            IAsyncRepository<Customer> repository,
            IMapper mapper)
        {
            _repository = repository
            _mapper = mapper;
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
        [HttpPost("customers", Name = "CreateCustomer")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(OperationId = "CreateCustomer", Tags = new[] { "Customers" })]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCustomerResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public override async Task<ActionResult<CreateCustomerResult>> HandleAsync(
                [FromBody] CreateCustomerRequest request,
                CancellationToken cancellationToken)
        {
            var customer = new Customer();
            _mapper.Map(request, customer);
            //TODO: Encrypt password
            customer = await _repository.AddAsync(customer, cancellationToken);
            var result = _mapper.Map<CreateCustomerResult>(customer);
            //TODO: Add logs
            return CreatedAtRoute("GetCustomer", new { id = result.Id }, result);
        }
    }
}
