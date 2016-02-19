using Microsoft.AspNet.Mvc.Razor;

namespace WorkingMvc6.Views.View
{
    public abstract class BaseView : RazorPage<object>
    {              
        public bool IsAuthenticated()
        {
            return Context.User.Identity.IsAuthenticated;
        }
    }
}
