using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tiles
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int[,] tiles;
        private Texture2D tileset;
        private int tileWidth = 32;
        private int tileHeight = 32;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            tiles = new int[8, 8];
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    tiles[i, j] = (i&1) ^ (j&1);
                }
            }
            tileset = Content.Load<Texture2D>("tilesBW");
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            for(int i = 0;i < tiles.GetLength(0);i++)
            {
                for(int j = 0;j < tiles.GetLength(1);   j++)
                {
                    Rectangle source = new Rectangle(tiles[i,j] * tileWidth, 0, tileWidth, tileHeight);
                    Vector2 drawLocation = new Vector2(tileWidth * i, tileHeight * j);
                    _spriteBatch.Draw(tileset, drawLocation, source, Color.White);
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
