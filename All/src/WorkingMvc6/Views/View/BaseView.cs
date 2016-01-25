using Microsoft.AspNet.Mvc.Razor;
using System.Security.Claims;
using WorkingMvc6.Services;
using System;
using System.Threading.Tasks;

namespace WorkingMvc6.Views.User
{
    public abstract class BaseView : RazorPage<object>
    {        
        public ClaimsPrincipal ViewModel
        {
            get
            {
                return Model as ClaimsPrincipal;
            }
        }

        public bool IsAuthenticated()
        {
            return Context.User.Identity.IsAuthenticated;
        }
    }
}
