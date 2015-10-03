using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Authentication.Cookies;

namespace ProtectStatic
{
    public class ProtectFolder
    {
        private readonly RequestDelegate _next;
        private readonly PathString _path;
        private readonly string _policyName;
        private readonly string _authenticationScheme;
       
        public ProtectFolder(RequestDelegate next, ProtectFolderOptions options)
        {
            _next = next;
            _path = options.Path;
            _policyName = options.PolicyName;
            _authenticationScheme = options.AuthenticationScheme;
        }

        public async Task Invoke(HttpContext httpContext, IAuthorizationService authorizationService)
        {
            if(httpContext.Request.Path.StartsWithSegments(_path))
            {
                var result = await authorizationService.AuthorizeAsync(httpContext.User, null, _policyName);
                if (!result)
                {
                    await httpContext.Authentication.ChallengeAsync(_authenticationScheme);
                    return;
                }
            }

            await _next(httpContext);
        }
    }

    public static class ProtectFolderExtensions
    {
        public static IApplicationBuilder UseProtectFolder(this IApplicationBuilder builder, ProtectFolderOptions options)
        {
            return builder.UseMiddleware<ProtectFolder>(options);
        }
    }

    public class ProtectFolderOptions
    {
        public PathString Path { get; set; }
        public string PolicyName { get; set; }
        public string AuthenticationScheme { get; set; }
    }
}
