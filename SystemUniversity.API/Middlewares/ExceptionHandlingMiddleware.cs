using System.Text.Json;
using SystemUniversity.Contracts.Exceptions;

namespace SystemUniversity.API.Middlewares{
    public class ExceptionHandlingMiddleware{
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context){
            
            try{
                await next(context);
            }catch (ExpectedException ex){
                context.Response.StatusCode = ex.Code;
                ErrorInformation error = new ErrorInformation(ex.Code, ex.Message);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
        }

        class ErrorInformation{
            public int StatusCode { get; set; }
            public string Message { get; set; }

            public ErrorInformation(int statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }
    }
}
