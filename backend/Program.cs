using Microsoft.EntityFrameworkCore;
using Lab.Api.Infrastructure;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.Commands;
using Lab.Api.Application.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Lab.Api.Application.Services;
using Lab.Api.Application.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LabDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Authentication
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtSettings>(jwtSection);
var jwtSettings = jwtSection.Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings?.Key ?? "replace_this_with_a_real_key");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddAuthorization();

// CQRS Dispatcher
builder.Services.AddSingleton<IDispatcher, Dispatcher>();

// Register handlers
builder.Services.AddTransient<ICommandHandler<CreatePacienteCommand, long>, CreatePacienteHandler>();
builder.Services.AddTransient<IQueryHandler<GetPacientesQuery, IEnumerable<Lab.Api.Application.DTOs.PacienteDto>>, GetPacientesHandler>();

builder.Services.AddTransient<ICommandHandler<CreateCUPSCommand, long>, CreateCUPSHandler>();
builder.Services.AddTransient<IQueryHandler<GetCUPSQuery, IEnumerable<Lab.Api.Application.DTOs.CUPSDto>>, GetCUPSHandler>();

builder.Services.AddTransient<ICommandHandler<CreateExamenCommand, long>, CreateExamenHandler>();
builder.Services.AddTransient<IQueryHandler<GetExamenesQuery, IEnumerable<Lab.Api.Application.DTOs.ExamenDto>>, GetExamenesHandler>();

builder.Services.AddTransient<ICommandHandler<CreateSolicitudCommand, long>, CreateSolicitudHandler>();
builder.Services.AddTransient<ICommandHandler<CreateFacturaCommand, long>, CreateFacturaHandler>();
builder.Services.AddTransient<ICommandHandler<PublishSlotsCommand, int>, PublishSlotsHandler>();
builder.Services.AddTransient<ICommandHandler<ProgramarCitaCommand, long>, ProgramarCitaHandler>();
builder.Services.AddTransient<ICommandHandler<EnqueueNotificationCommand, long>, EnqueueNotificationHandler>();
builder.Services.AddTransient<ICommandHandler<ProcessPendingNotificationsCommand, int>, ProcessPendingNotificationsHandler>();
builder.Services.AddTransient<ICommandHandler<GenerarRipsCommand, int>, GenerarRipsHandler>();
builder.Services.AddTransient<ICommandHandler<GenerarLoteRipsCommand, long>, GenerarLoteRipsHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
