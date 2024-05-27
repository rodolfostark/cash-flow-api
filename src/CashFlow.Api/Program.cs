using CashFlow.Api.Filters;
using CashFlow.Api.Middlewares;
using CashFlow.Application;
using CashFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting(option => option.LowercaseUrls = true);


builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
// Dependecy Injection from Infrastrucute project
builder.Services.AddInfrastructure(builder.Configuration);
// Dependecy Injection from Application project
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
