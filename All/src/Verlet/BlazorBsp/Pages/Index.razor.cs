using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorBsp.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBsp.Pages
{
    public class IndexComponent : ComponentBase
    {

        protected Canvas2DContext context;
        protected BECanvasComponent canvasReference;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            context = await canvasReference.CreateCanvas2DAsync();

            var options = new RopeOptions
            {
                Left = 612, 
                Top = 1,
                ParticleDistance = 2,
                ParticleSize = 2,
                NumParticles = 80
            };

            var rope = new Rope(options);
            await rope.Draw(context);
        }
    }
}
