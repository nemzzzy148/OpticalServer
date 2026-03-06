var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.WebHost.UseUrls("http://0.0.0.0:100");

var app = builder.Build();

app.MapControllers();

app.Run();