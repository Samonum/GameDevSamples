using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DoubleBubble
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public List<GameObject> GameObjects { get; private set; }
        
        

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
            GameObjects = new List<GameObject>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D bubbleTexture = Content.Load<Texture2D>("Ball");
            Texture2D tankTexture = Content.Load<Texture2D>("Tank");
            Viewport viewport = _graphics.GraphicsDevice.Viewport;


            //GameObjects.Add(new Tank(tankTexture, new Vector2(viewport.Width / 2, viewport.Height)));

            GameObjects.Add(new Bubble(new Vector2(400, 50), bubbleTexture, Vector2.UnitX * 150, viewport.Bounds));
            GameObjects.Add(new Bubble(new Vector2(200, 100), bubbleTexture, Vector2.UnitX * 120, viewport.Bounds));
            GameObjects.Add(new Bubble(new Vector2(800, 200), bubbleTexture, Vector2.UnitX * 220, viewport.Bounds));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Update(gameTime);
            }

            for (int i = 0; i < GameObjects.Count; i++)
            {
                for (int j = i + 1; j < GameObjects.Count; j++)
                {
                    if (GameObjects[i].CheckCollision(GameObjects[j]))
                    {
                        GameObjects[i].OnCollision(GameObjects[j]);
                        GameObjects[j].OnCollision(GameObjects[i]);
                    }
                }
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
