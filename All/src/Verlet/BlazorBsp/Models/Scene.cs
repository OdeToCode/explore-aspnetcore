using Blazor.Extensions.Canvas.Canvas2D;
using System;
using System.Threading;

namespace BlazorBsp.Models
{
    public class Scene : IDisposable
    {
        private readonly Timer timer;
        private readonly Rope rope;
        private readonly SceneOptions options;
        private readonly Point totalForce;

        public Scene(SceneOptions options, Canvas2DContext context)
        {
            timer = new Timer(_ => Update(context), context, options.UpdatePeriod, options.UpdatePeriod);
            rope = new Rope(options);
            this.options = options;
            
            this.totalForce = new Point();
            totalForce.Add(options.Gravity);
            totalForce.Add(options.Wind);
        }

        public void Update(Canvas2DContext context)
        {
            rope.Verlet(totalForce);
            var _ = rope.Draw(context);
        }
        public void Dispose()
        {
            timer.Dispose();
        }
    }
}
