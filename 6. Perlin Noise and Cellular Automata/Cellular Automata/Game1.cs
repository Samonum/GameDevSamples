using System;
using System.Reflection.Metadata;
using Accord.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


// Use Space to apply automata
// Use P to toggle between modes
enum DrawMode : int
{
    Automata = 0,
    Raw = 1, //Only shows the raw  Perlin noise data
    WaterLand = 2, //Everything above the Cutoff becomes Snow, everything below negative cutoff becomes water
    Count
}

namespace CellularAutomata
{
    public class Game1 : Game
    {
        // Uses Perlin noise when True, completely random data when false.
        bool perlin = true;

        // Perlin Variables:
        DrawMode drawMode = DrawMode.Automata; // Toggles with P
        double zoom = .08; // [0, inf] How zoomed in the image is, more zoomed in means clearer borders
        float perlinCutoff = 0.1f; // [-1, 1] The cutoff point where we decide between tile 1 or 0
        int octaves = 4; // <1,32> Higher octaves makes the image more noisy
        float persistence = 0.65f; // [0,1] How fast values change. Higher values mean more clear borders
        float frequency = 1f; // [0,1] Similar to zoom
        float amplitude = 1f; // [0,inf] Multiplies the output, creates clearer borders

        // Random Variables
        float randomCutoff = 0.5f; // [0,1] The cutoff point where we decide between tile 1 or 0


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int[,] tiles;
        private Texture2D tileset;
        private int tileWidth = 32;
        private int tileHeight = 32;
        Random rnd = new Random();
        KeyboardState previous;
        PerlinNoise perlinGen;
        int seed;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 32*40;
            _graphics.PreferredBackBufferWidth = 32*60;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            perlinGen = new PerlinNoise(octaves, persistence, frequency, amplitude);
            seed = rnd.Next()/2;
            tiles = new int[_graphics.PreferredBackBufferWidth/tileHeight, _graphics.PreferredBackBufferHeight / tileWidth];
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (perlin)
                    {
                        tiles[i, j] = perlinGen.Function2D(i*zoom+seed, j*zoom+seed) > perlinCutoff ? 1 : 0;
                    }
                    else
                        tiles[i, j] = rnd.NextDouble() > randomCutoff ? 1 : 0 ;
                }
            }
            tileset = Content.Load<Texture2D>("tilesBW");
            base.Initialize();
            previous = Keyboard.GetState();

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

            if(Keyboard.GetState().IsKeyDown(Keys.Space) && !previous.IsKeyDown(Keys.Space))
            {
                int[,] next = new int[_graphics.PreferredBackBufferWidth / tileHeight, _graphics.PreferredBackBufferHeight / tileWidth];
                for (int i = 0; i < tiles.GetLength(0); i++)
                {
                    for (int j = 0; j < tiles.GetLength(1); j++)
                    {
                        next[i, j] = GetNeighbourCount(i, j) > 4 ? 1 : 0;
                    }
                }
                tiles = next;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.P) && !previous.IsKeyDown(Keys.P))
                drawMode = (DrawMode)(((int)drawMode + 1)%(int)DrawMode.Count);
             
            base.Update(gameTime);
            previous = Keyboard.GetState();
        }

        public int GetNeighbourCount(int x, int y)
        {
            int count = 0;
            for (int i = x-1; i <= x+1; i++)
            {
                for(int j = y-1; j <= y+1; j++)
                {
                    // Increase count if the neighbours are 1, or out of bounds
                    if (i < 0 || i >= tiles.GetLength(0) ||
                        j < 0 || j >= tiles.GetLength(1) ||
                        tiles[i, j] == 1)
                        count++;
                }
            }
            return count;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            for(int i = 0;i < tiles.GetLength(0);i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    Rectangle source = new Rectangle(tiles[i, j] * tileWidth, 0, tileWidth, tileHeight);
                    Vector2 drawLocation = new Vector2(tileWidth * i, tileHeight * j);
                    _spriteBatch.Draw(tileset, drawLocation, source, Color.White);
                }
                if (drawMode == DrawMode.Raw)
                {
                    for (int j = 0; j < tiles.GetLength(1); j++)
                    {
                        Rectangle source = new Rectangle(0, 0, tileWidth, tileHeight);
                        Vector2 drawLocation = new Vector2(tileWidth * i, tileHeight * j);
                        float val = 1 - (1 + (float)perlinGen.Function2D(i * zoom + seed, j * zoom + seed)) / 2;
                        _spriteBatch.Draw(tileset, drawLocation, source, new Color(val, val, val));
                    }
                }
                else if (drawMode == DrawMode.WaterLand)
                {
                    for (int j = 0; j < tiles.GetLength(1); j++)
                    {
                        Rectangle source = new Rectangle(0, 0, tileWidth, tileHeight);
                        Vector2 drawLocation = new Vector2(tileWidth * i, tileHeight * j);
                        float rawVal = (float)perlinGen.Function2D(i * zoom + seed, j * zoom + seed);
                        Color color;
                            float val = 1 - (1 + rawVal) / 2;
                        if (Math.Abs(rawVal) < perlinCutoff)
                        {
                            color = new Color(0, val, 0);
                        }
                        else if (rawVal < 0)
                        {
                            color = new Color(val,val,val);
                        }
                        else
                        {
                            color = new Color(0f,0f,val);
                        }


                        _spriteBatch.Draw(tileset, drawLocation, source, color);
                    }
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
