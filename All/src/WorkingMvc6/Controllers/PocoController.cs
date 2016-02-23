using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.AspNet.Mvc.ViewFeatures;

namespace WorkingMvc6.Controllers
{
    [Route("[controller]")]
    public class PocoController
    {
        public PocoController()
        {
            
        }

        public string Index(ActionContext c)
        {
            return c.ActionDescriptor.Name;            
        }
    }
}
