namespace PMRBDOdata.TokenValidation
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _validTokens;

        public TokenValidationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _validTokens = configuration.GetSection("TokenSettings:Tokens").GetChildren().Select(child => child.Value).ToList();
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
            if (!_validTokens.Contains(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized Access");
                return;
            }

            await _next(context);
        }
    }
}
