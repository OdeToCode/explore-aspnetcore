using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using static System.String;

namespace Controllers.Controllers
{
    public class Nested
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class Model
    {
        public string Name { get; set; }

        [FormEmbeddedJson]
        public List<Nested> Nesties { get; set; }
    }


    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class FormEmbeddedJsonAttribute : Attribute
    {
        
    }

    public class MyBindingMetadataProvider : IBindingMetadataProvider
    {
        public void CreateBindingMetadata(BindingMetadataProviderContext context)
        {
            if (context.PropertyAttributes != null)
            {
                if (context.PropertyAttributes.Any(a => a is FormEmbeddedJsonAttribute))
                {
                    context.BindingMetadata.BinderType = typeof(EmbeddedJsonModelBinder);
                }
            }
        }
    }

    public class EmbeddedJsonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            try
            {
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject(value, bindingContext.ModelType);
                
                bindingContext.Result = ModelBindingResult.Success(model);
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                bindingContext.ModelState.TryAddModelError(
                    bindingContext.ModelName,
                    exception,
                    bindingContext.ModelMetadata);
                return Task.CompletedTask;
            }
        }
    }

    public class EmbeddedJsonModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            return null;
        }
    }

    [Route("[controller]")]
    public class HiddenJson : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new Model
            {
                Name = "Phase",
                Nesties = new List<Nested>
                {
                    new Nested {Id = 1, Description = "flag"},
                    new Nested {Id = 2, Description = "fren'c'\"h\""}
                }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(Model model)
        {
            return View(model);
        }
    }
}
