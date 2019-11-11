using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WorkingMvc6.TagHelpers
{
    [HtmlTargetElement("my-environment")]
    public class MyEnvironment : TagHelper
    {
        private readonly IWebHostEnvironment _env;

        public MyEnvironment(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string Names { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Names.Split().Any(n => _env.EnvironmentName == n))
            {
                return;
            }

            output.SuppressOutput();
        }
    }
}