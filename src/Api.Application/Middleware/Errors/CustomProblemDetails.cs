using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Middleware.Errors
{
    public class CustomProblemDetails : ProblemDetails
    {
        public int Status { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public object Errors { get; set; }


        public CustomProblemDetails(HttpStatusCode statusCode, string title, string detail, object? errors = null)
        {
            Status = (int)statusCode;
            Title = title;
            Detail = detail;
            Errors = errors;
        }
    }
}
