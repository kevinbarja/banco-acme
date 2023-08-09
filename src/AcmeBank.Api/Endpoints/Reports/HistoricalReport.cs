using AcmeBank.Contracts;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace AcmeBank.Api.Endpoints.Reports
{
    public class HistoricalReport : EndpointBaseAsync
            .WithRequest<HistoricalReportRequest>
            .WithActionResult
    {
        private readonly IReportRepository _repository;

        public HistoricalReport(
            IReportRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get historical report
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// GET reports/historical?page=0&perPage=10&customerId=12&startDate=2023-08-01&endDate=2023-08-30
        /// 
        /// </remarks>
        /// <response code = "200" >Returns report data</response>
        [HttpGet("reports/historical", Name = "GetHistoricalReport")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(OperationId = "GetHistoricalReport", Tags = new[] { "Reports" })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<HistoricalReportData>))]
        public override async Task<ActionResult> HandleAsync(
            [FromQuery] HistoricalReportRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _repository.GetHistoricalReportAsync(
                request.Page, request.PerPage, request.StartDate, request.EndDate, request.CustomerId, cancellationToken);
            return Ok(result);
        }
    }
}
