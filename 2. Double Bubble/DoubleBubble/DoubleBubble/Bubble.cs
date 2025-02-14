using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DoubleBubble
{
    public class Bubble : GameObject
    {
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public Rectangle Bounds;
        public float Scale;
        public override float Radius { get { return Sprite.Width/2 * Scale; } }

        public Bubble(Vector2 location, Texture2D sprite, Vector2 velocity, Rectangle bounds, float scale = 1)
        {
            Location = location;
            Velocity = velocity;
            Sprite = sprite;
            Acceleration = new Vector2(0, 400);
            Scale = scale;
            Bounds = bounds;
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Velocity += (float)gameTime.ElapsedGameTime.TotalSeconds * Acceleration;
            Location += (float)gameTime.ElapsedGameTime.TotalSeconds * Velocity;

            // Reflect the ball on X and Y axis and push them out of the wall.

            if (Location.Y + Radius > Bounds.Bottom)
            {
                Velocity.Y = -Velocity.Y;
                Location.Y = Bounds.Bottom - Radius;
            }
            if (Location.X - Radius < Bounds.Left)
            {
                Velocity.X = -Velocity.X;
                Location.X = Bounds.Left + Radius;
            }
            if(Location.X + Radius > Bounds.Right)
            {
                Velocity.X = -Velocity.X;
                Location.X = Bounds.Right - Radius;
            }
        }

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
            if (other is Bubble)
            {
                Vector2 collisionNormal = Location - other.Location;
                //collisionNormal.Y = 0;
                collisionNormal.Normalize();

                Velocity = Vector2.Reflect(Velocity, collisionNormal);


                /*
                if(Math.Sign(Velocity.X) != Math.Sign(collisionNormal.X))
                {
                    Velocity.X = -Velocity.X;
                }
                */
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(Sprite, Location, null, Color.Red, 0, new Vector2(Radius, Radius), Scale, SpriteEffects.None, 0);
        }

    }
}
