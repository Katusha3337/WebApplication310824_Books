namespace WebApplication310824_Books.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private const string Token = "token12345";

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/getbooks"))
            {
                var token = context.Request.Query["token"].ToString();
                if (token != Token)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
            }

            await _next(context);
        }
    }
}
