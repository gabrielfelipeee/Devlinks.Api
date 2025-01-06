using System.Net;
using Api.Application.Middleware.Errors;
using Api.Service.Shared.Exceptions;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Application.Middleware
{
    public class ControllerExceptionMiddleware
    {
        public ControllerExceptionMiddleware(
            RequestDelegate next,
            ILogger<ControllerExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        private readonly RequestDelegate _next; // Próximo middleware na pipeline de requisições
        private readonly ILogger<ControllerExceptionMiddleware> _logger; // Log para registrar erros
        private readonly IHostEnvironment _env;
        // Método principal que captura exceções durante a requisição
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Chama o próximo middleware na cadeia (Se não houver nenhuma Exception)
                await _next(context);
            }
            catch (Exception ex)
            {
                bool isDevelopment = _env.IsDevelopment();
                HttpStatusCode statusCode;
                string title;
                string detail;
                object errors = null;

                switch (ex)
                {
                    case ForbiddenAccessException:
                        statusCode = HttpStatusCode.Forbidden;
                        title = "Acesso negado.";
                        detail = isDevelopment
                            ? ex.StackTrace ?? ex.Message
                            : ex.Message;
                        break;
                    case NotFoundException:
                        statusCode = HttpStatusCode.NotFound;
                        title = "Recurso não encontrado.";
                        detail = isDevelopment
                            ? ex.StackTrace ?? ex.Message
                            : ex.Message;
                        break;
                    case ValidationException validationException:
                        statusCode = HttpStatusCode.BadRequest;
                        title = "Erro de validação.";
                        detail = isDevelopment
                            ? ex.StackTrace ?? ex.Message
                            : "Um ou mais erros de validação ocorreram.";

                        errors = validationException.Errors
                            .GroupBy(x => x.PropertyName)
                            .ToDictionary(
                                x => x.Key,
                                x => x.Select(x => x.ErrorMessage).ToList()
                            );
                        break;
                    case ConflictException:
                        statusCode = HttpStatusCode.Conflict;
                        title = "Conflito de dados.";
                        detail = isDevelopment
                            ? ex.StackTrace ?? ex.Message
                            : ex.Message;
                        break;
                    case ArgumentException:
                        statusCode = HttpStatusCode.BadRequest;
                        title = "Argumento inválido.";
                        detail = isDevelopment
                            ? ex.StackTrace ?? ex.Message
                            : ex.Message;
                        break;
                    default:
                        statusCode = HttpStatusCode.InternalServerError;
                        title = "Erro interno do servidor.";
                        detail = isDevelopment
                            ? ex.StackTrace ?? ex.Message
                            : ex.Message; // "Ocorreu um erro inesperado. Tente novamente mais tarde.";
                        break;
                };

                // Registra o erro no log
                _logger.LogError(ex, ex.Message);

                // Prepara a resposta para o cliente
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;

                // Cria um objeto com os detalhes do erro
                var problemDetails = new CustomProblemDetails(statusCode, title, detail, errors);

                // Configura a serialização para JSON, usando camelCase e ignorando valores nulos
                var options = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore };

                // Converte os detalhes do problema para JSON
                var json = JsonConvert.SerializeObject(problemDetails, options);

                // Escreve a resposta JSON para o cliente
                await context.Response.WriteAsync(json);
            }
        }
    }
}
