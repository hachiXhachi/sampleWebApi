namespace sampleWebApi.Middleware
{
    public class ApiMiddleware
    {
        private const string ApiKeyName = "ApiKey";
        private readonly RequestDelegate _delegate;
        public ApiMiddleware(RequestDelegate requestDelegate)
        {
            _delegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if(!httpContext.Request.Query.TryGetValue(ApiKeyName, out var apiFromUser))
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("No API Key Provided");
                return;
            }
            var appsettings = httpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appsettings.GetValue<string>(ApiKeyName);
            if(apiKey != apiFromUser)
            {
                httpContext.Response.StatusCode = 403;
                await httpContext.Response.WriteAsync("Api Keys Unauthorized");
                return;
            }

            await _delegate(httpContext);
        }
    }
}
