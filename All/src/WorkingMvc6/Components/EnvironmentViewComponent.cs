using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace WorkingMvc6.Components
{
    public class EnvironmentViewComponent : ViewComponent
    {
        private readonly IHostingEnvironment _environment;

        public EnvironmentViewComponent(IHostingEnvironment environment)
        {
            _environment = environment;
        }
       
        public IViewComponentResult Invoke()
        {
            return View(_environment.EnvironmentName);
        }

    }
}