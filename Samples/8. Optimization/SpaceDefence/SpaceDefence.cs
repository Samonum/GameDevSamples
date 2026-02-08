using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SpaceDefence
{
    public class SpaceDefence : Game
    {
        public static int XSpacing = 70;
        public static int YSpacing = 200;
        public static int ShipRows = 3;
        public static int ShipColumns = 7;
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        private GameManager _gameManager;

        public SpaceDefence()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;

            // Set the size of the screen
            _graphics.PreferredBackBufferWidth = 2000;
            _graphics.PreferredBackBufferHeight = 1200;
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Initialize the GameManager
            _gameManager = GameManager.GetGameManager();
            base.Initialize();
            Random r = new Random(7);
            // Place the player at the center of the screen
            for(int i = 0; i < ShipRows; i++)
            {
                for(int j = 0;  j < ShipColumns; j++)
                {
                    Point team1Pos =  new Point(r.Next(20) + 200 + j * XSpacing * ShipRows + i * XSpacing, r.Next(20) + 200 + i * YSpacing);
                    Point team2Pos =  new Point(r.Next(20) + 200 + j * XSpacing * ShipRows + i * XSpacing, 2000 + r.Next(20) + 200 + i * YSpacing);
                    Ship player = new Ship(team1Pos, CollisionType.Team1, Color.Red);
                    Ship player2 = new Ship(team2Pos, CollisionType.Team2, Color.Blue);
                    _gameManager.AddGameObject(player);
                    _gameManager.AddGameObject(player2);
                }
            }

            // Add the starting objects to the GameManager
            _gameManager.Initialize(Content, this);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gameManager.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _gameManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _gameManager.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);

        }



    }
}
