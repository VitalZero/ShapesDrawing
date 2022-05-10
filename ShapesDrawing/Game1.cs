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
        float rotation;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            rotation = MathHelper.TwoPi;
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

            rotation -= 2f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            screen.Set();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            shapes.Begin();

            Vector2[] vertices = new Vector2[5];

            vertices[0] = new Vector2(0, -10);
            vertices[1] = new Vector2(10, 10);
            vertices[2] = new Vector2(3, 3);
            vertices[3] = new Vector2(-3, 3);
            vertices[4] = new Vector2(-10, 10);

            int[] triIndices = new int[(vertices.Length - 2) * 3];

            triIndices[0] = 0;
            triIndices[1] = 1;
            triIndices[2] = 2;
            triIndices[3] = 0;
            triIndices[4] = 2;
            triIndices[5] = 3;
            triIndices[6] = 0;
            triIndices[7] = 3;
            triIndices[8] = 4;

            Matrix transform = Matrix.CreateScale(1f) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateTranslation(50f, 50f, 1f);

            shapes.DrawPolygonFill(vertices, triIndices, transform, Color.White);

            shapes.End();

            screen.UnSet();
            screen.Present(_spriteBatch);

            base.Draw(gameTime);

        }
    }
}
