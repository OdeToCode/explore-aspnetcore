using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace WorkingMvc6.Pages
{
    public class SecretsModel : PageModel
    {
        public bool IsLucky { get; set; }
        private readonly IAuthorizationService authorization;

        public SecretsModel(IAuthorizationService authorization)
        {
            this.authorization = authorization;
        }

        public async Task OnGet()
        {
            var result = await authorization.AuthorizeAsync(User, "IsLucky");
            IsLucky = result.Succeeded;
        }
    }

    //[Authorize(Policy = "IsLucky")]
    //public class SecretsModel : PageModel
    //{
            // ...
    //}
}