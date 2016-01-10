using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.PlatformAbstractions;

namespace WorkingMvc6.Components
{
    public class EnvironmentViewComponent : ViewComponent
    {
        private readonly IApplicationEnvironment _environment;

        public EnvironmentViewComponent(IApplicationEnvironment environment)
        {
            _environment = environment;
        }

        public IViewComponentResult Invoke()
        {
            return View(_environment.RuntimeFramework);
        }

    }
}