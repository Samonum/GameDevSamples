using System;
using SpaceDefence.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence
{
    public class Ship : GameObject
    {
        public Vector2 Velocity { get; private set; }
        public float speed = 100;
        public float Range = 500;

        public float AvoidanceRange = 100;
        public float cooldown = 1;
        public float health = 100;
        private Texture2D ship_body;
        private Color[] bodyData;
        private Texture2D fadedBody;
        private Texture2D base_turret;
        private Color[] turretData;
        private Texture2D fadedTurret;
        private RectangleCollider _rectangleCollider;
        private Point target;
        private Color teamColor;

        /// <summary>
        /// The player character
        /// </summary>
        /// <param name="Position">The ship's starting position</param>
        public Ship(Point Position, CollisionType collisionType, Color teamColor)
        {
            _rectangleCollider = new RectangleCollider(new Rectangle(Position, Point.Zero));
            SetCollider(_rectangleCollider);
            CollisionType = collisionType | CollisionType.Solid;
            this.teamColor = teamColor;
        }

        public override void Load(ContentManager content)
        {
            // Original ship sprites from: https://zintoki.itch.io/space-breaker

            // Setting up the texture data so we can apply our colouring later
            ship_body = content.Load<Texture2D>("ship_body");
            fadedBody = new Texture2D(ship_body.GraphicsDevice, ship_body.Width, ship_body.Height);
            bodyData = new Color[ship_body.Width * ship_body.Height];
            ship_body.GetData<Color>(bodyData);

            base_turret = content.Load<Texture2D>("base_turret");
            turretData = new Color[base_turret.Width * base_turret.Height];
            base_turret.GetData<Color>(turretData);
            fadedTurret = new Texture2D(base_turret.GraphicsDevice, base_turret.Width, base_turret.Height);
            
            _rectangleCollider.shape.Size = ship_body.Bounds.Size;
            _rectangleCollider.shape.Location -= new Point(ship_body.Width/2, ship_body.Height/2);
            base.Load(content);
        }

        public override void HandleInput(InputManager inputManager)
        {
            base.HandleInput(inputManager);
            if(inputManager.LeftMousePress())
            {
                Shoot();
            }
        }

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
            
            if (other is Bullet && (other.CollisionType & CollisionType) == 0)
            {
                health -= 1;
                if (health < 0)
                {
                    GameManager.GetGameManager().RemoveGameObject(this);
                    ParticleData data = new ParticleData();
                    data.lifespan = 5;
                    data.particleCount = 40;
                    data.maxScale = .6f;
                    data.minScale = .2f;
                    new ParticleEmitter(GetPosition().Center.ToVector2(), data).Emit();
                }
            }
        }

        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            Ship nearest = FindNearestEnemy();
            target = nearest == null ? Point.Zero : nearest.GetPosition().Center;

            if( (target -GetPosition().Center).ToVector2().Length() < Range)
            {
                if(cooldown < 0)
                {
                    _rectangleCollider.shape.Location += Shoot();
                }
            }
            else
            {
                _rectangleCollider.shape.Location += (Vector2.Normalize((target -GetPosition().Center).ToVector2()) * speed  * (float)gameTime.ElapsedGameTime.TotalSeconds).ToPoint();
            }
            _rectangleCollider.shape.Location += (AvoidObstacles()* (float)gameTime.ElapsedGameTime.TotalSeconds).ToPoint();
        }

        public Point Shoot()
        {
            cooldown = 0.5f;
            Vector2 aimDirection = LinePieceCollider.GetDirection(GetPosition().Center, target);
            Vector2 turretExit = _rectangleCollider.shape.Center.ToVector2() + aimDirection * base_turret.Height / 2f;
            GameManager.GetGameManager().AddGameObject(new Bullet(turretExit, aimDirection, 150, CollisionType));

            return (-aimDirection * 20).ToPoint();
        }

        public Vector2 AvoidObstacles()
        {
            Vector2 avoidance = Vector2.Zero;
            foreach(GameObject other in GameManager.GetGameManager().GetGameObjects())
            {
                if(other == this || !other.CollisionType.HasFlag(CollisionType.Solid))
                    continue;
                Vector2 difference = (GetPosition().Center - other.GetPosition().Center).ToVector2();
                float distance = difference.Length();
                if(distance < AvoidanceRange)
                {
                    avoidance += (float)Math.Sqrt(AvoidanceRange)*speed * Vector2.Normalize(difference)/(float)Math.Sqrt(distance);
                }
            }
            return avoidance;
        }

        public Ship FindNearestEnemy()
        {
            Ship nearest = null;
            foreach(GameObject candidate in GameManager.GetGameManager().GetGameObjects())
            {
                if(candidate is Ship)
                {
                    Ship othership = (Ship)candidate;
                    if((othership.CollisionType & CollisionType.Teams) == (CollisionType & CollisionType.Teams))
                        continue;
                    if(nearest == null )
                    {
                        nearest = othership;
                        continue;
                    }
                    Vector2 pos = GetPosition().Center.ToVector2();
                    Vector2 nearPos = nearest.GetPosition().Center.ToVector2();
                    Vector2 newPos = othership.GetPosition().Center.ToVector2();
                    if( (pos - nearPos).Length() > (pos - newPos).Length() )
                    {
                        nearest = othership;
                    }
                }
            }
            return nearest;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            ReplaceAndFadeTexture(bodyData, fadedBody, teamColor, health / 100);
            ReplaceAndFadeTexture(turretData, fadedTurret, teamColor, health / 100);

            spriteBatch.Draw(fadedBody, _rectangleCollider.shape, Color.White);
            float aimAngle = LinePieceCollider.GetAngle(LinePieceCollider.GetDirection(GetPosition().Center, target));
            Rectangle turretLocation = base_turret.Bounds;
            turretLocation.Location = _rectangleCollider.shape.Center;
            spriteBatch.Draw(fadedTurret, turretLocation, null, Color.White, aimAngle, turretLocation.Size.ToVector2() / 2f, SpriteEffects.None, 0);

            base.Draw(gameTime, spriteBatch);
        }

        public static void ReplaceAndFadeTexture(Color[] textureData, Texture2D target, Color color, float percentage)
        {

            Color[] targetData = new Color[textureData.Length];

            for (int i = 0; i < targetData.Length; i++)
            {
                if (textureData[i].R == textureData[i].B && textureData[i].G == 0 && textureData[i].R != 0)
                {
                    // Read the Red chanel out as a float instead of a byte
                    float originalShade = textureData[i].ToVector4().X;

                    // Fade the pixel to black based on health percentage and shading
                    targetData[i].R = (byte)(color.R * percentage * originalShade);
                    targetData[i].G = (byte)(color.G * percentage * originalShade);
                    targetData[i].B = (byte)(color.B * percentage * originalShade);
                    targetData[i].A = textureData[i].A;
                }
                else
                {
                    targetData[i] = textureData[i];
                }

            }
            target.SetData(targetData);
        }

    }
}
