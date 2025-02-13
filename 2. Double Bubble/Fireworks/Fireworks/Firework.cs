using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fireworks
{
    
    public class Firework
    {

        public Vector2 Position;
        public Vector2 Velocity;
        public Texture2D RocketSprite;
        public Texture2D ExplosionSprite;
        public SoundEffect Explosion;
        public bool Exploded = false;
        public double ExplosionTimer = 2;
        public Color FireworkColor;
        public Vector2 Scale;

        public Firework(Vector2 position, Vector2 velocity, Texture2D rocketSprite, Texture2D explosionSprite, SoundEffect explosion)
        {
            Position = position;
            Velocity = velocity;
            RocketSprite = rocketSprite;
            ExplosionSprite = explosionSprite;
            Scale = new Vector2(2, 2);
            FireworkColor = Color.White;
            Explosion = explosion;
        }

        public void Update()
        {

            if (!Exploded)
            {
                Position += Velocity;

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Exploded = true;
                    Explosion.Play();
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (!Exploded)
            {
                spriteBatch.Draw(RocketSprite, Position, null, Color.White, 0, RocketSprite.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(ExplosionSprite, Position, null, FireworkColor, 0, ExplosionSprite.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0);
            }
        }

    }
}
