using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShapesDrawing
{
    public sealed class Screen : IDisposable
    {
        private readonly static int MinDim = 64;
        private readonly static int MaxDim = 4096;

        private RenderTarget2D target;
        private bool isDisposed;
        private Game game;
        private bool isSet;

        public int Width
        {
            get { return target.Width; }
        }

        public int Height
        {
            get { return target.Height; }
        }

        public Screen(Game game, int width, int height)
        {
            this.game = game ?? throw new ArgumentNullException("game is not valid (null)");

            width = (int)Math.Clamp(width, MinDim, MaxDim);
            height = (int)Math.Clamp(height, MinDim, MaxDim);

            target = new RenderTarget2D(game.GraphicsDevice, width, height);

            isSet = false;
        }

        public void Dispose()
        {
            if (isDisposed)
                return;

            target?.Dispose();
            isDisposed = true;
        }

        public void Set()
        {
            if(isSet)
            {
                throw new Exception("Render target already set");
            }

            game.GraphicsDevice.SetRenderTarget(target);
            isSet = true;
        }

        public void UnSet()
        {
            if (!isSet)
            {
                throw new Exception("Render target is not set");
            }

            game.GraphicsDevice.SetRenderTarget(null);
            isSet = false;
        }

        public void Present(SpriteBatch spriteBatch)
        {
#if DEBUG
            game.GraphicsDevice.Clear(Color.HotPink);
#else
            game.GraphicsDevice.Clear(Color.Black); 
#endif
            Rectangle destinationRectangle = CalculateDestinationRectangle();

            spriteBatch.Begin();
            spriteBatch.Draw(target, destinationRectangle, Color.White);
            spriteBatch.End();
        }

        private Rectangle CalculateDestinationRectangle()
        {
            Rectangle backbufferBounds = game.GraphicsDevice.PresentationParameters.Bounds;
            float backbufferAspectRatio = (float)backbufferBounds.Width / (float)backbufferBounds.Height;
            float screenAspectRatio = (float)Width / (float)Height;

            float rx = 0f;
            float ry = 0f;
            float rw = backbufferBounds.Width;
            float rh = backbufferBounds.Height;

            if(backbufferAspectRatio > screenAspectRatio)
            {
                rw = rh * screenAspectRatio;
                rx = (float)(backbufferBounds.Width - rw) / 2f;
            }
            else if(backbufferAspectRatio < screenAspectRatio)
            {
                rh = rw / screenAspectRatio;
                ry = (float)(backbufferBounds.Height - rh) / 2f;
            }

            return new Rectangle((int)rx, (int)ry, (int)rw, (int)rh);
        }
    }
}
