using AcmeBank.Contracts;
using AcmeBank.Persistence.Entities;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace AcmeBank.Api.Endpoints.Customers
{
    public class Get : EndpointBaseAsync
            .WithRequest<int>
            .WithActionResult<CustomerResult>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Customer> _repository;

        public Get(
            IMapper mapper,
            IAsyncRepository<Customer> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Get a specific customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// GET customers/{id}
        /// 
        /// </remarks>
        /// <response code = "200" >Returns a customer</response>
        /// <response code = "404" >Customer not found</response> 
        [HttpGet("customers/{id}", Name = "GetCustomer")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(OperationId = "GetCustomer", Tags = new[] { "Customers" })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public override async Task<ActionResult<CustomerResult>> HandleAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(id, cancellationToken);

            if (customer is null) return NotFound();

            var result = _mapper.Map<CustomerResult>(customer);

            return result;
        }
    }
}
