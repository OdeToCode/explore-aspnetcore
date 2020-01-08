using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorBsp.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorBsp.Pages
{
    public class IndexComponent : ComponentBase, IDisposable
    {
        protected Canvas2DContext context;
        protected BECanvasComponent canvasReference;
        private Scene scene;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            context = await canvasReference.CreateCanvas2DAsync();

            var options = new SceneOptions
            {
                Left = 612,
                Top = 1,
                ParticleDistance = 12,
                ParticleSize = 4,
                NumParticles = 20,
                UpdatePeriod = 200,
                Iterations = 20,
                Gravity = new Force { X = -1.2, Y = 0 },
                Wind = new Force { X = 0, Y = 0.9 }
            };

            scene = new Scene(options, context);
        }

        public void Dispose()
        {
            scene.Dispose();
        }
    }
}
