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
                Left = (int)canvasReference.Width / 2,
                Top = 1,
                ParticleDistance = 8,
                ParticleSize = 4,
                NumParticles = 30,
                UpdatePeriod = 10,
                Iterations = 20,
                Gravity = new Force { X = -1.2, Y = 0 },
                Wind = new Force { X = 0, Y = 0.9 },
                Width = canvasReference.Width,
                Height = canvasReference.Height
            };

            scene = new Scene(options, context);
        }

        public void Dispose()
        {
            scene.Dispose();
        }
    }
}
