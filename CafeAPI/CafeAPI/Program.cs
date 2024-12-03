using Cafe.Infrastructure;
using CafeAPI.Apis;
using CafeAPI.Extensions;


var builder = WebApplication.CreateSlimBuilder(args);

builder.AddDefaultOpenApi();
builder.AddApplicationServices();



var app = builder.Build();

// Apply the seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CafeContext>();
    dbContext.Database.EnsureCreated();
}


app.UseApplicationServices();

app.CafeApis();
app.UseDefaultOpenApi();
app.Run();

