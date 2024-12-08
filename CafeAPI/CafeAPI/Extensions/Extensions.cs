using Cafe.Infrastructure;
using Microsoft.EntityFrameworkCore;
using CafeAPI.Application.Queries;
using System.Text.Json.Serialization;

using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Cafe.Domain.Repository;
using eShop.Ordering.Infrastructure.Repositories;
using CafeAPI.Application.Commands;
namespace CafeAPI.Extensions
{
    internal static class Extensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            //var services = builder.Services;
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
                //cfg.RegisterServicesFromAssemblies(typeof(CafeAPI.Application.Commands.CreateCafeCommandHandler).Assembly);


            });

            //builder.Services.AddMediatR(typeof(Program).Assembly);
            builder.Services.AddDbContext<CafeContext>(options => options.UseInMemoryDatabase("CafeManagementDb"));

            builder.Services.AddScoped<ICafeQueries, CafeQueries>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });

            });
            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
            });

            //builder.Services.AddSingleton<CreateCafeCommand, CreateCafeCommandHandler>();
            builder.Services.AddTransient(typeof(IRequestHandler<IdentifiedCommand<CreateCafeCommand, bool>, bool>), typeof(IdentifiedCommandHandler<CreateCafeCommand, bool>));
            builder.Services.AddTransient(typeof(IRequestHandler<IdentifiedCommand<CreateEmployeeCommand, bool>, bool>), typeof(IdentifiedCommandHandler<CreateEmployeeCommand, bool>));
            builder.Services.AddTransient(typeof(IRequestHandler<IdentifiedCommand<UpdateCafeCommand, bool>, bool>), typeof(IdentifiedCommandHandler<UpdateCafeCommand, bool>));
            builder.Services.AddTransient(typeof(IRequestHandler<IdentifiedCommand<UpdateEmployeeCommand, bool>, bool>), typeof(IdentifiedCommandHandler<UpdateEmployeeCommand, bool>));
            builder.Services.AddTransient(typeof(IRequestHandler<IdentifiedCommand<RemoveCafeCommand, bool>, bool>), typeof(IdentifiedCommandHandler<RemoveCafeCommand, bool>));
            builder.Services.AddTransient(typeof(IRequestHandler<IdentifiedCommand<RemoveEmployeeCommand, bool>, bool>), typeof(IdentifiedCommandHandler<RemoveEmployeeCommand, bool>));


            builder.Services.AddScoped<ICafeRepository, CafeRepository>();

           

        }
        public static IApplicationBuilder UseApplicationServices(this WebApplication app)
        {
            app.UseCors("AllowAnyOrigin");
            return app;
        }
    }

    [JsonSerializable(typeof(object))]
    [JsonSerializable(typeof(CafeAPI.Application.Queries.Employee))]
    [JsonSerializable(typeof(CafeAPI.Application.Queries.Cafe))]
    [JsonSerializable(typeof(IEnumerable<CafeAPI.Application.Queries.Cafe>))]
    
    [JsonSerializable(typeof(CafeAPI.Application.Queries.ICafeQueries))]
    [JsonSerializable(typeof(MediatR.IMediator))]
    [JsonSerializable(typeof(System.String))]
    [JsonSerializable(typeof(string))]
  
    public partial class AppJsonSerializerContext : JsonSerializerContext { }

    public partial class Program { }
}
