using Microsoft.EntityFrameworkCore;
using People.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// configure Kestrel to listen on a port, e.g., 5001 (https) or 5000 (http)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000);  // HTTP
});

builder.Services.AddGrpc();

builder.Services.AddDbContext<PeopleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

app.MapGrpcService<PersonServiceImpl>();

app.Run();
