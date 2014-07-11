namespace BattleFiled.CellViews
{
    using BattleFiled.Renderer.Context;
    using System;

    interface ICellView
    {
        void Draw(RenderingContext context);
    }
}