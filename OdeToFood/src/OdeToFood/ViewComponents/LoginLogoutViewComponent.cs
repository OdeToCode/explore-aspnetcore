using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.ViewComponents
{
    public class LoginLogoutViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
