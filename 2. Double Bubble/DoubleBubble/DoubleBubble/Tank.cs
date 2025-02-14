
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Formats.Asn1.AsnWriter;

namespace DoubleBubble
{
    public class Tank :GameObject
    {
        private KeyboardState _lastState;
        public Vector2 Speed = new Vector2(5, 0);
        public Tank(Texture2D sprite, Vector2 location)
        {
            Sprite = sprite;
            Location = location;
            _lastState = Keyboard.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Location -= Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Location += Speed;
            }
            if (_lastState.IsKeyUp(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Fire();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(Sprite, Location, null, Color.White, 0, new Vector2(Radius, Sprite.Height), 1, SpriteEffects.None, 0);
        }

        public void Fire()
        {

        }
    }
}
