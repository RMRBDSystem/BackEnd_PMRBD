namespace PMRBDOdata.TokenValidation
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _validToken;

        public TokenValidationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _validToken = configuration["TokenSettings:Token"];
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Token"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized Access");
                return;
            }

            var token = context.Request.Headers["Token"].ToString();
            if (token != _validToken)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized Access");
                return;
            }

            await _next(context);
        }
    }
}
