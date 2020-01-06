using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBsp.Pages
{
    public class IndexComponent : ComponentBase
    {

        protected Canvas2DContext _context;
        protected BECanvasComponent _canvasReference;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            this._context = await this._canvasReference.CreateCanvas2DAsync();
            await this._context.SetFillStyleAsync("green");

            await this._context.FillRectAsync(10, 100, 100, 100);

            await this._context.SetFontAsync("48px serif");
            await this._context.StrokeTextAsync("Hello Blazor!!!", 10, 100);
        }
    }
}
