using AcmeBank.Contracts;
using AcmeBank.Persistence.Entities;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace AcmeBank.Api.Endpoints.Customers
{
    public class Delete : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult
    {
        private readonly IAsyncRepository<Customer> _repository;

        public Delete(IAsyncRepository<Customer> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Deletes an customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// DELETE customers/{id}
        /// 
        /// </remarks>
        /// <response code = "204" >Deleted customer</response>
        [HttpDelete("customers/{id}", Name = "DeleteCustomer")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(OperationId = "DeleteCustomer", Tags = new[] { "Customers" })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public override async Task<ActionResult> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(id, cancellationToken);
            if (customer is not null)
            {
                //TODO: Soft delete
                await _repository.DeleteAsync(customer, cancellationToken);
            }
            // see https://restfulapi.net/http-methods/#delete
            return NoContent();
        }
    }
}
