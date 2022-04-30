using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using VZShapes;

namespace ShapesDrawing
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Screen screen;
        Shapes shapes;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            shapes = new Shapes(this);
            screen = new Screen(this, 640, 480);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            screen.Set();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            shapes.Begin();

            shapes.DrawRectangleFill(100, 100, 50, 50, Color.Indigo);
            shapes.DrawRectangleFill(320-60, 240-60, 50, 50, Color.BlueViolet);

            shapes.DrawLine(new Vector2(10, 10), new Vector2(200, 200), 1f, Color.Black);
            shapes.DrawLine(new Vector2(50, 50), new Vector2(150, 260), 1f, Color.Black);


            shapes.DrawRectangle(10, 20, 60, 60, 1f, Color.Black);

            shapes.DrawCircle(100, 100, 40, 32, 1f, Color.BurlyWood);

            Vector2[] vertices = new Vector2[4];

            vertices[0] = new Vector2(10, 10);
            vertices[1] = new Vector2(50, 5);
            vertices[2] = new Vector2(70, 80);
            vertices[3] = new Vector2(40, 100);

            shapes.DrawPolygon(vertices, 1f, Color.White);

            shapes.End();

            screen.UnSet();
            screen.Present(_spriteBatch);

            base.Draw(gameTime);

        }
    }
}
