public class AuthRedirectMiddleware
{
    private readonly RequestDelegate _next;

    public AuthRedirectMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path == "/auth")
        {
            string key = context.Request.Query["key"];
            string destinationUrl = $"http://api.sahmab.co/accounts/loggedin-user?api_key={key}";
            context.Response.Redirect(destinationUrl);
            return;
        }

        await _next(context);
    }
}
