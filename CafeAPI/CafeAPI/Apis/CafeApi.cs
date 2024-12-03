

using Cafe.Domain.Models;
using CafeAPI.Application.Commands;
using CafeAPI.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace CafeAPI.Apis
{
    public static class CafeApi
    {

        public static RouteGroupBuilder CafeApis(this IEndpointRouteBuilder app)
        {
            var api = app.MapGroup("api/cafes/");

            api.MapGet("/cafe/{location}", GetLocationAsync);
            api.MapGet("/employee/{cafe}", GetEmloyeesByCafeAsync);
            api.MapPost("/cafe/create", CreateCafeAsync);
            api.MapPost("/employee/create", CreateEmployeeAsync);

            api.MapPut("/cafe/update", UpdateCafeAsync);
            api.MapPut("/employee/update", UpdateEmployeeAsync);
            api.MapDelete("/cafe/{Id:guid}", RemoveCafeAsync);
            api.MapDelete("/employee/{Id}", RemoveEmployeeAsync);
            return api;
        }

        private static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> CreateEmployeeAsync([FromHeader(Name = "x-requestid")] Guid requestId,
            CreateEmployeeCommand command, [AsParameters] CafeServices services)
        {
            var commandCreate = new IdentifiedCommand<CreateEmployeeCommand, bool>(command, requestId);
            var cafeResult = await services.Mediator.Send(commandCreate);
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> UpdateEmployeeAsync([FromHeader(Name = "x-requestid")] Guid requestId,
           UpdateEmployeeCommand command, [AsParameters] CafeServices services)
        {
            var commandCreate = new IdentifiedCommand<UpdateEmployeeCommand, bool>(command, requestId);
            var cafeResult = await services.Mediator.Send(commandCreate);
            return TypedResults.Ok();
        }
        private static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> UpdateCafeAsync([FromHeader(Name = "x-requestid")] Guid requestId,
            UpdateCafeCommand command, [AsParameters] CafeServices services)
        {
            var commandCreate = new IdentifiedCommand<UpdateCafeCommand, bool>(command, requestId);
            var cafeResult = await services.Mediator.Send(commandCreate);
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> RemoveEmployeeAsync([FromHeader(Name = "x-requestid")] string requestId,
             [AsParameters] CafeServices services)
        {

            string customUuid = requestId;
            Guid result = ConvertToGuid(customUuid);

            var command = new RemoveEmployeeCommand(result);
            var removeCommand = new IdentifiedCommand<RemoveEmployeeCommand, bool>(command, result);
            var cafeResult = await services.Mediator.Send(removeCommand);
            return TypedResults.Ok();
        }
        private static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> RemoveCafeAsync([FromHeader(Name = "x-requestid")] Guid requestId,
             [AsParameters] CafeServices services)
        {
            var command = new RemoveCafeCommand(requestId);
            var removeCommand = new IdentifiedCommand<RemoveCafeCommand, bool>(command, requestId);
            var cafeResult = await services.Mediator.Send(removeCommand);
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> CreateCafeAsync([FromHeader(Name = "x-requestid")] Guid requestId,
            CreateCafeCommand command, [AsParameters] CafeServices services)
        {

            var commandCreate = new IdentifiedCommand<CreateCafeCommand, bool>(command, requestId);
            var cafeResult = await services.Mediator.Send(commandCreate);
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok<IEnumerable<CafeAPI.Application.Queries.Employee>>, NotFound>> GetEmloyeesByCafeAsync(string cafe, [AsParameters] CafeServices services, IMediator mediator)
        {
            try
            {
                var empByCafe = await services.Queries.GetEmployeesByCafe(cafe);
                return TypedResults.Ok(empByCafe);
            }
            catch
            {
                return TypedResults.NotFound();
            }
        }

        private static async Task<Results<Ok<IEnumerable<CafeAPI.Application.Queries.Cafe>>, NotFound>> GetLocationAsync(string location, [AsParameters] CafeServices services, IMediator mediator)
        {
            try
            {
                var cafeByLoc = await services.Queries.GetCafeByLocationAsync(location);
                return TypedResults.Ok(cafeByLoc);
            }
            catch
            {
                return TypedResults.NotFound();
            }
        }

        public static Guid ConvertToGuid(string customUuid)
        {
            if (customUuid.Length != 9 || !customUuid.StartsWith("UI"))
            {
                throw new ArgumentException("Input must be in the format 'UIXXXXXXX' where X is an alphanumeric character.");
            }
            string strippedInput = customUuid.Substring(2);
            string paddedInput = strippedInput.PadRight(32, '0');
            return Guid.Parse(paddedInput);
        }
    }
}
