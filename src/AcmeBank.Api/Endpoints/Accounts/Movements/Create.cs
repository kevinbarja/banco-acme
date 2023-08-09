using AcmeBank.Contracts;
using AcmeBank.Persistence.Entities;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace AcmeBank.Api.Endpoints.Accounts.Movements
{
    public class Create : EndpointBaseAsync
            .WithRequest<CreateMovementRequest>
            .WithActionResult<CreateMovementResult>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Movement> _movementRepository;
        private readonly IAsyncRepository<Account> _accountRepository;
        private readonly ApiConfig _apiConfig;

        public Create(IMapper mapper,
            IAsyncRepository<Movement> movementRepository,
            IAsyncRepository<Account> accountRepository,
            IOptions<ApiConfig> apiConfig)
        {
            _mapper = mapper;
            _movementRepository = movementRepository;
            _accountRepository = accountRepository;
            _apiConfig = apiConfig.Value;
        }

        /// <summary>
        /// Creates a new movement
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// POST accounts/5/movements
        /// 
        ///     {
        ///         "amount": 105.5
        ///     }
        ///
        /// </remarks>
        /// <response code = "201" >Returns movement created</response>
        /// <response code = "400" >Bad request, account not exist</response>  
        [HttpPost("accounts/{AccountId}/movements", Name = "CreateMovement")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(OperationId = "CreateMovement", Tags = new[] { "Accounts and movements" })]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateAccountResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public override async Task<ActionResult<CreateMovementResult>> HandleAsync(
                [FromRoute] CreateMovementRequest request,
                CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId, cancellationToken);
            if (account is null)
            {
                return BadRequest();
            }



            var movementAmount = request.Body.Amount;
            var movement = new Movement();
            movement.Balance = account.InitialBalance;
            movement.Date = DateTime.Now;

            //TODO: Convet type to computed field
            if (movementAmount > 0)
            {
                movement.Type = (short)MovementType.Credit;
            }
            else
            {
                movement.Type = (short)MovementType.Debit;
            }

            movement.Amount = movementAmount;
            movement.AccountId = request.AccountId;

            movement = await _movementRepository.AddAsync(movement, cancellationToken);
            var result = _mapper.Map<CreateMovementResult>(movement);

            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
