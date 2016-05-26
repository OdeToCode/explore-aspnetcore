using Microsoft.AspNetCore.Mvc;
using OdeToFood.Services;

namespace OdeToFood.ViewComponents
{
    public class Greeting : ViewComponent
    {
        private IGreeter _greeter;

        public Greeting(IGreeter greeter)
        {
            _greeter = greeter;
        }

        public IViewComponentResult Invoke()
        {
            var model = _greeter.GetGreeting();
            return View("Default", model);
        }
    }
}
