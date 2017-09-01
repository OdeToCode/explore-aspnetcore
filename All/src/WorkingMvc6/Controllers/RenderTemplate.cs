using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using Microsoft.AspNetCore.Mvc.Razor;

namespace WorkingMvc6.Controllers
{
    [Route("[controller]")]
    public class RenderTemplate : Controller
    {
        private IViewEngine _viewEngine;

        public RenderTemplate(IRazorViewEngine viewEngine)
        {
            _viewEngine = viewEngine;
        }

        public async Task<IActionResult> Index()
        {
            var model = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "Scott",
                LastName = "Allen"
            };
            var message = await Render(@"Views\RenderTemplate\PatientTemplate.cshtml", model);
            return View("Index", message);
        }

        private async Task<string> Render(string viewName, object model)
        { 
            var viewEngineResult = _viewEngine.GetView(null, viewName, true);
            if (viewEngineResult.Success)
            {
                using (var writer = new StringWriter())
                {                    
                    var view = viewEngineResult.View;
                    var actionContext = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor, ModelState);
                    var viewContext = new ViewContext(actionContext, view, ViewData, TempData, writer, new HtmlHelperOptions());

                    ViewData.Model = model;
                    await view.RenderAsync(viewContext);

                    return writer.ToString();
                }
            }
            throw new InvalidOperationException($"Could not render {viewName}");
            
        }
    }

    public class Patient
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
