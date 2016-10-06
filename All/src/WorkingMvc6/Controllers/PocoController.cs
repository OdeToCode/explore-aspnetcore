using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
            return c.ActionDescriptor.DisplayName;            
        }
    }
}
