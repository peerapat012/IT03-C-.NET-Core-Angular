using Microsoft.EntityFrameworkCore;
using no3_api.Data;
using no3_api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Request")));
builder.Services.AddScoped<IRequestServices, RequestServices>();
builder.AddRequestDb();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MigrateDatabase();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
