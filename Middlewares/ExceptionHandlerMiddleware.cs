namespace Shorten.io.Middlewares
{
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using FluentValidation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    namespace Expensely.Api.Middleware
    {
        /// <summary>
        /// Represents the exception handler middleware.
        /// </summary>
        internal class ExceptionHandlerMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger<ExceptionHandlerMiddleware> _logger;

            public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
            {
                _next = next;
                _logger = logger;
            }

            /// <summary>
            /// Invokes the exception handler middleware with the specified <see cref="HttpContext"/>.
            /// </summary>
            /// <param name="httpContext">The HTTP httpContext.</param>
            /// <returns>The task that can be awaited by the next middleware.</returns>
            public async Task Invoke(HttpContext httpContext)
            {
                try
                {
                    await _next(httpContext);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An exception occurred: {Message}", ex.Message);

                    await HandleExceptionAsync(httpContext, ex);
                }
            }

            /// <summary>
            /// Handles the specified <see cref="Exception"/> for the specified <see cref="HttpContext"/>.
            /// </summary>
            /// <param name="httpContext">The HTTP httpContext.</param>
            /// <param name="exception">The exception.</param>
            /// <returns>The HTTP response that is modified based on the exception.</returns>
            private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
            {
                HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

                string result = string.Empty;

                if (exception is ValidationException validationException)
                {
                    httpStatusCode = HttpStatusCode.BadRequest;

                    result =
                           string.Join(" ,", validationException.Errors.Select(x => x.ErrorMessage).ToList());
                }

                httpContext.Response.ContentType = "application/json";

                httpContext.Response.StatusCode = (int)httpStatusCode;

                if (result.Length == 0)
                {
                    result = "Internal Server Error";
                }

                return httpContext.Response.WriteAsync(result);
            }
        }

        /// <summary>
        /// Contains extension methods for configuring the exception handler middleware.
        /// </summary>
        internal static class ExceptionHandlerMiddlewareExtensions
        {
            /// <summary>
            /// Configure the custom exception handler middleware.
            /// </summary>
            /// <param name="builder">The application builder.</param>
            /// <returns>The configured application builder.</returns>
            internal static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
                => builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
