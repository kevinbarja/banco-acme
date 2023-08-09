using AcmeBank.Api.Endpoints.Customers;
using AcmeBank.Contracts;
using AcmeBank.Persistence.Entities;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace AcmeBank.Api.Endpoints.Accounts
{
    public class Get : EndpointBaseAsync
            .WithRequest<int>
            .WithActionResult<AccountResult>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Account> _repository;

        public Get(
            IMapper mapper,
            IAsyncRepository<Account> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Get a specific account
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// GET accounts/{id}
        /// 
        /// </remarks>
        /// <response code = "200" >Returns an account</response>
        /// <response code = "404" >Account not found</response> 
        [HttpGet("accounts/{id}", Name = "GetAccount")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(OperationId = "GetAccount", Tags = new[] { "Accounts" })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public override async Task<ActionResult<AccountResult>> HandleAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var account = await _repository.GetByIdAsync(id, cancellationToken);

            if (account is null) return NotFound();

            var result = _mapper.Map<AccountResult>(account);

            return result;
        }
    }
}
