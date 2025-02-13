using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;

namespace DoubleBubble
{
    public abstract class GameObject
    {
        public Texture2D Sprite;
        public Vector2 Location;
        public virtual float Radius { get { return Sprite.Width / 2; } }
        public virtual void Update() { }
        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual void OnCollision(GameObject other) { }

        public bool CheckCollision(GameObject other)
        {
            Vector2 distance = other.Location - Location;
            return distance.Length() < other.Radius + Radius;

        }

    }
}
