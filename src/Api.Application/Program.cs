using System.Security.Claims;
using System.Text;
using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Mappings;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers();

            // Configurções de injeção de dependências
            ConfigureService.ConfigureDependenciesService(builder.Services);
            ConfigureRepository.ConfigureDependenciesRepository(builder.Services);


            // AutoMapper configuration
            var autoMapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new DtoToModelProfile());
                config.AddProfile(new EntityToDtoProfile());
                config.AddProfile(new ModelToEntityProfile());
            });
            IMapper mapper = autoMapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);


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
            }).AddJwtBearer(bearerOptios =>
            {
                var paramsValidation = bearerOptios.TokenValidationParameters;

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
                    .Build()); // Apenas usuários autenticados podem  fazer 

                auth.AddPolicy("AdminPolicy", new AuthorizationPolicyBuilder()
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

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
