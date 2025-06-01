using ContractsApi.Application.Interfaces;
using ContractsApi.Application.UseCases;
using ContractsApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<ContractService>();

builder.Services.AddScoped<ITarifaRepository, TarifaRepository>();
builder.Services.AddScoped<TarifaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
    app.UseHsts();

    app.UseExceptionHandler("/error");
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Map("/error", () => Results.Problem("Ocurrió un error inesperado.")).AllowAnonymous();

app.Run();
