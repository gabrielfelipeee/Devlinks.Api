using System.Security.Claims;
using Api.Application.Middleware;
using Api.CrossCutting.DependencyInjection;
using Api.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});

// Configurções de injeção de dependências
ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services, builder.Configuration);
ConfigureAutoMapper.ConfigureDependenciesAutoMapper(builder.Services);
ConfigureFluentValidation.ConfigureDependenciesFluentValidation(builder.Services);


// Jwt Configurações
var signingConfigurations = new SigningConfigurations();
builder.Services.AddSingleton(signingConfigurations);

var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
        .Configure(tokenConfigurations);
builder.Services.AddSingleton(tokenConfigurations);

// Autenticação
builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Jwt como esquema de autenticação padrão
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions =>
{
    var paramsValidation = bearerOptions.TokenValidationParameters;

    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
    paramsValidation.ValidAudience = tokenConfigurations.Audience;
    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
    paramsValidation.ValidateIssuerSigningKey = true; // Verifica se a chave usada é válida
    paramsValidation.ValidateLifetime = true; // Verifica se o token ainda está dentro do tempo  de validação
    paramsValidation.ClockSkew = TimeSpan.Zero;
});

// Autorização
builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder() // Cria uma politica chamada 'Bearer'
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme) // Especifica o esquema a ser usado, nesse caso o bearer
        .RequireAuthenticatedUser()
        .Build()); // Apenas usuários autenticados podem fazer 

    auth.AddPolicy("Admin", new AuthorizationPolicyBuilder()
        .RequireClaim(ClaimTypes.Role, "Admin")
        .Build());
});

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
    new OpenApiInfo
    {
        Title = "Api DevLinks",
        Version = "v1",
        Description = "API REST",
        Contact = new OpenApiContact
        {
            Name = "Gabriel Felipe",
            Email = "gabrielfelipe0722@gmail.com",
            Url = new Uri("https://github.com/gabrielfelipeee")
        }
    });

    // Adicionar o botão de Authorize
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Entre com o token Jwt",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
                   {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                             {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                   }
    });
});
// Cors 
builder.Services.AddCors(options =>
{
    options.AddPolicy("localhost",
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("localhost");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ControllerExceptionMiddleware>();

app.Run();
