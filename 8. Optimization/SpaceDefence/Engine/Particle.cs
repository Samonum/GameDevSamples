using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence
{
    public class Particle : GameObject
    {
        public float lifespan;
        public float fade;

        public Vector2 velocity;
        public Vector2 acceleration;
        public Vector2 location;

        public float scale;
        public Texture2D sprite;
        public Color color;

        public override void Load(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Particle");
            base.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            if (lifespan < -fade)
                GameManager.GetGameManager().RemoveGameObject(this);
            if (lifespan < 0)
                color.A = (byte)(255 * (fade + lifespan)/fade);
            lifespan -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity += (float)gameTime.ElapsedGameTime.TotalSeconds * acceleration;
            location += (float)gameTime.ElapsedGameTime.TotalSeconds * velocity;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, location, null, color, 0, sprite.Bounds.Center.ToVector2(), scale, SpriteEffects.None, 0);
        }
    }

}
