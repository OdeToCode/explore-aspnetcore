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
        protected Scene scene;
        protected SceneOptions options;

        public IndexComponent()
        {
            options = new SceneOptions
            {
                ParticleDistance = 8,
                ParticleSize = 4,
                ParticleCount = 30,
                UpdatePeriod = 10,
                Iterations = 20,
                Gravity = new Force { X = 0, Y = 1 },
                Wind = new Force { X = 0, Y = 0 }
            };
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            options.Left = (int)canvasReference.Width / 2;
            options.Top = 1;
            options.Width = canvasReference.Width;
            options.Height = canvasReference.Height;
            context = await canvasReference.CreateCanvas2DAsync();
        }

        public void Start()
        {
            if(scene != null)
            { 
                scene.Dispose();
            }
            scene = new Scene(options, context);
        }

        public void Dispose()
        {
            if (scene != null)
            {
                scene.Dispose();
            }
        }
    }
}
