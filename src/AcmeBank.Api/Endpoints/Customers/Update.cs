using AcmeBank.Contracts;
using AcmeBank.Persistence.Entities;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace AcmeBank.Api.Endpoints.Customers
{
    public class Update : EndpointBaseAsync
        .WithRequest<UpdateCustomerRequest>
        .WithActionResult<UpdateCustomerResult>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Customer> _repository;

        public Update(
            IMapper mapper,
            IAsyncRepository<Customer> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Updates an existing customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///       "fullName": "Juan Perez Ortiz",
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
        /// <response code = "200" >Returns customer updated</response>
        /// <response code = "400" >Bad request</response>
        /// <response code = "404" >Customer not found</response>         
        [HttpPut("customers/{Id}", Name = "UpdateCustomer")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(OperationId = "UpdateCustomer", Tags = new[] { "Customers" })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateCustomerResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public override async Task<ActionResult<UpdateCustomerResult>> HandleAsync(
            [FromRoute] UpdateCustomerRequest request,
            CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (customer is null) return NotFound();

            _mapper.Map(request.Body, customer);
            await _repository.UpdateAsync(customer, cancellationToken);

            var result = _mapper.Map<UpdateCustomerResult>(customer);
            return result;
        }
    }
}
