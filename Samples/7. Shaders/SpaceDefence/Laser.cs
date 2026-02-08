using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence
{
    internal class Laser : GameObject
    {
        private LinePieceCollider linePiece;
        private Texture2D sprite;
        private double lifespan = .25f;

        public Laser(LinePieceCollider linePiece)
        {
            this.linePiece = linePiece;
            SetCollider(linePiece);
        }
        public Laser(LinePieceCollider linePiece, float length) : this(linePiece)
        {
            // Sets the length of the laser to be equal to the width of the screen, so it will always cover the full screen.
            this.linePiece.Length = length;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            sprite = content.Load<Texture2D>("Laser");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (lifespan < 0)
                GameManager.GetGameManager().RemoveGameObject(this);
            lifespan -= gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle target = new Rectangle((int)linePiece.Start.X, (int)linePiece.Start.Y, sprite.Width, (int)linePiece.Length);
            spriteBatch.Draw(sprite, target, null,Color.White, linePiece.GetAngle(), new Vector2(sprite.Width/2f,sprite.Height),SpriteEffects.None,1 );
            base.Draw(gameTime, spriteBatch);
        }
    }
}
