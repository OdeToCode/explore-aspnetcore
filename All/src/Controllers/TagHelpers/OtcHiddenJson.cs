using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;

namespace Controllers.TagHelpers
{
    [HtmlTargetElement("otc-hiddenjson", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class OtcHiddenJson : TagHelper
    {
        [HtmlAttributeName("value")]
        public ModelExpression Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {            
            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.Add("name", Value.Name);
            output.Attributes.Add("type", "hidden");
            output.Attributes.Add("value", SerializeValue());
        }

        private string SerializeValue()
        {
            var sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                writer.QuoteChar = '\'';

                var ser = new JsonSerializer();
                ser.Serialize(writer, Value.Model);
            }
            return sb.ToString();
        }
    }
}
