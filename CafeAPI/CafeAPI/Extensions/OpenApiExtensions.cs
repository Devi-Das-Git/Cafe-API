using Scalar.AspNetCore;

namespace CafeAPI.Extensions
{

    public static class OpenApiOptionsExtensions
    {
        public static IApplicationBuilder UseDefaultOpenApi(this WebApplication app)
        {
            app.MapOpenApi();

            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference(options =>
                {
                    options.DefaultFonts = false;
                });
                app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();
            }

            return app;
        }

        public static IHostApplicationBuilder AddDefaultOpenApi(
            this IHostApplicationBuilder builder)
        {
            builder.Services.AddOpenApi(options =>
            {
            });
            return builder;
        }
    }

}
