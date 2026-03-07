using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.WebHost.UseUrls("http://0.0.0.0:100");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnections")));

var app = builder.Build();

app.MapControllers();

app.Run();