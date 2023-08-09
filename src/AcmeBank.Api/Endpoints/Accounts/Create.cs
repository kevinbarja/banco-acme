using AcmeBank.Contracts;
using AcmeBank.Persistence.Entities;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace AcmeBank.Api.Endpoints.Accounts
{
    public class Create : EndpointBaseAsync
            .WithRequest<CreateAccountRequest>
            .WithActionResult<CreateAccountResult>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Account> _repository;

        public Create(IMapper mapper, IAsyncRepository<Account> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///    {
        ///      "number": "441345",
        ///      "type": "Savings",
        ///      "initialBalance": 0,
        ///      "isActive": true,
        ///      "customerId": 14
        ///    }
        ///
        /// </remarks>
        /// <response code = "201" >Returns account created</response>
        /// <response code = "400" >Bad request</response>  
        [HttpPost("accounts", Name = "CreateAccount")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(OperationId = "CreateAccount", Tags = new[] { "Accounts" })]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateAccountResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public override async Task<ActionResult<CreateAccountResult>> HandleAsync(
                CreateAccountRequest request,
                CancellationToken cancellationToken)
        {
            var account = new Account();
            _mapper.Map(request, account);
            account = await _repository.AddAsync(account, cancellationToken);
            var result = _mapper.Map<CreateAccountResult>(account);
            //TODO: Add logs
            return CreatedAtRoute("GetAccount", new { id = result.Id }, result);
        }
    }
}
