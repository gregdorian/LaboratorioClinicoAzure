using Microsoft.EntityFrameworkCore;
using Lab.Api.Infrastructure;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.Commands;
using Lab.Api.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LabDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CQRS Dispatcher
builder.Services.AddSingleton<IDispatcher, Dispatcher>();

// Register handlers
builder.Services.AddTransient<ICommandHandler<CreatePacienteCommand, long>, CreatePacienteHandler>();
builder.Services.AddTransient<IQueryHandler<GetPacientesQuery, IEnumerable<Lab.Api.Application.DTOs.PacienteDto>>, GetPacientesHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
