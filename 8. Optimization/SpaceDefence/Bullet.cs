using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence
{
    internal class Bullet : GameObject
    {
        private Texture2D _texture;
        private CircleCollider _circleCollider;
        private Vector2 _velocity;
        public float bulletSize = 4;
        public float LifeTime = 3;

        public Bullet(Vector2 location, Vector2 direction, float speed, CollisionType collisionType)
        {
            CollisionType = collisionType & ~CollisionType.Solid;
            _circleCollider = new CircleCollider(location, bulletSize);
            SetCollider(_circleCollider);
            _velocity = direction * speed;
        }

        public override void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Bullet");
            base.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _circleCollider.Center += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            LifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (LifeTime < 0)
                GameManager.GetGameManager().RemoveGameObject(this);

        }

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
            if (other is Ship && (other.CollisionType & CollisionType) == 0)
            {
                GameManager.GetGameManager().RemoveGameObject(this);
                ParticleData data = new ParticleData();
                data.maxScale = 0.2f;
                data.minScale = 0.1f;
                new ParticleEmitter(GetPosition().Center.ToVector2(), data).Emit();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _circleCollider.GetBoundingBox(), Color.Red);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
