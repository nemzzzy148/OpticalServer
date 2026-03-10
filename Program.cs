using Microsoft.EntityFrameworkCore;
using OpticalServer.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.WebHost.UseUrls("https://0.0.0.0:5001");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<DatabaseContext>( options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnections"),
    o => o.MapEnum<ReactionType>("reaction_type"))
);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();