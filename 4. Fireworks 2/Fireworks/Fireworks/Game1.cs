using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Fireworks
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Firework _firework;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Increase the size of the screen
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D rocketSprite = Content.Load<Texture2D>("Rocket");
            Texture2D explosionSprite = Content.Load<Texture2D>("particle");

            // Source: https://freesound.org/people/theplax/sounds/560578/
            SoundEffect explosion = Content.Load<SoundEffect>("Explosion");

            Vector2 location = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2, _graphics.GraphicsDevice.Viewport.Height - 50);
            Vector2 velocity = new Vector2(0, -5);


            _firework = new Firework(location, velocity, rocketSprite, explosionSprite, explosion);
        }

        protected override void Update(GameTime gameTime)
        {
            _firework.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _firework.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
