
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fireworks
{
    public class Particle
    {
        public float lifespan;

        public Vector2 velocity;
        public Vector2 acceleration;
        public Vector2 location;

        public float scale;
        public Texture2D sprite;
        public Color color;

        public void Update(GameTime gameTime)
        {
            if (lifespan < 0)
                return;
            lifespan -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity += (float)gameTime.ElapsedGameTime.TotalSeconds * acceleration;
            location += (float)gameTime.ElapsedGameTime.TotalSeconds * velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (lifespan < 0)
                return;
            spriteBatch.Draw(sprite, location, null, color, 0,sprite.Bounds.Center.ToVector2(), scale, SpriteEffects.None,0);
        }
    }

}
