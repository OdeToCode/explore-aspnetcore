using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Controllers.Controllers
{
    public class HttpPostWithValidForgeryToken
        : Attribute, IActionHttpMethodProvider, IFilterFactory
    {
        private readonly HttpPostAttribute _postAttribute;
        private readonly ValidateAntiForgeryTokenAttribute _antiForgeryAttribute;

        public HttpPostWithValidForgeryToken()
        {
            _postAttribute = new HttpPostAttribute();
            _antiForgeryAttribute = new ValidateAntiForgeryTokenAttribute();
        }

        public IEnumerable<string> HttpMethods => _postAttribute.HttpMethods;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return _antiForgeryAttribute.CreateInstance(serviceProvider);
        }

        public bool IsReusable => _antiForgeryAttribute.IsReusable;
    }

    [Route("[controller]")]
    public class PostToMe : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPostWithValidForgeryToken]
        public IActionResult Index(string button)
        {
            return Content("yes");
        }

    }
}
