using AcmeBank.Contracts;
using AcmeBank.Persistence.Entities;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace AcmeBank.Api.Endpoints.Customers
{
    public class Patch : EndpointBaseAsync
        .WithRequest<PatchCustomerRequest>
        .WithActionResult<PatchCustomerResult>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Customer> _repository;

        public Patch(
            IMapper mapper,
            IAsyncRepository<Customer> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Patches an existing customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///    {
        ///      "jsonPatchDocument": [
        ///         {
        ///             "op": "replace",
        ///             "value": "Marco Martinez",
        ///				"path":"/fullName"
        ///			}
        ///	     ]
        ///    }
        /// 
        /// </remarks>
        /// <response code = "200" >Returns customer patched</response>
        /// <response code = "400" >Bad request</response>
        /// <response code = "404" >Customer not found</response>         
        [HttpPatch("customers/{Id}", Name = "PatchCustomer")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(OperationId = "PatchCustomer", Tags = new[] { "Customers" })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateCustomerResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public override async Task<ActionResult<PatchCustomerResult>> HandleAsync(
            [FromRoute] PatchCustomerRequest request,
            CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (customer is null) return NotFound();

            var updateCustomerRequest = _mapper.Map<PatchCustomerDocument>(customer);
            request.Body.JsonPatchDocument.ApplyTo(updateCustomerRequest);

            // perform model validation of the changes
            TryValidateModel(updateCustomerRequest);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _mapper.Map(updateCustomerRequest, customer);
            await _repository.UpdateAsync(customer, cancellationToken);

            var result = _mapper.Map<PatchCustomerResult>(customer);
            return result;
            // NOTE: Alternately you could return a 204 No Content with a Location Header pointing to /customers/{id}
        }
    }
}
