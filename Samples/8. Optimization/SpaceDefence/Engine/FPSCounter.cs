using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence.Engine
{
    /// <summary>
    /// A simple FPS counter that smooths out some peaks and keeps track of the worst FPS
    /// </summary>
    public class FPSCounter : GameObject
    {
        private const int historySize = 15;
        private double[] timeHistory;
        private int counter = 0;
        private double worstFPS = 60;
        private double currentFPS;
        private SpriteFont font;

        public FPSCounter(SpriteFont font)
        {
            this.font = font;
            timeHistory = new double[historySize];
            for (int i = 0; i < historySize; i++)
            {
                timeHistory[i] = 60;
            }
        }

        public override void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            if (time.TotalGameTime.TotalSeconds < 2)
                return;
            counter = (counter + 1) % historySize;
            timeHistory[counter] = 1.0 / time.ElapsedGameTime.TotalSeconds;
            currentFPS = timeHistory.Average();
            if (currentFPS < worstFPS)
            {
                worstFPS = currentFPS;
            }
            string fpsCounter = $"FPS: {currentFPS.ToString("N1")} \n Worst: {worstFPS.ToString("N1")}";
            spriteBatch.DrawString(font, fpsCounter, new Vector2(25, 25), Color.White);
        }
    }
}
