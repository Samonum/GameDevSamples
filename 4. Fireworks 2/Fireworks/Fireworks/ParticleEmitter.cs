using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fireworks
{
    public class ParticleEmitter
    {
        Random random = new Random();
        public bool active;
        public float lifespan = 5;
        private Particle[] particles;

        public float minScale = 0.5f;
        public float maxScale = 2f;

        public float minDirection = 0;
        public float maxDirection = 2 * (float)Math.PI;

        public float minSpeed = 5;
        public float maxSpeed = 40;

        public Vector2 acceleration = new Vector2(0, 20f);
        public Vector2 location;

        public Texture2D sprite;
        public ParticleEmitter(int particleCount, Vector2 location, Texture2D sprite)
        {
            this.sprite = sprite;
            this.location = location;
            particles = new Particle[particleCount];
            ResetParticles();
            
        }

        public void ResetParticles()
        {
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i] = new Particle();

                float direction = MathHelper.Lerp(minDirection, maxDirection, (float)random.NextDouble());
                particles[i].velocity = new Vector2((float)Math.Cos(direction), (float)Math.Sin(direction));
                particles[i].velocity *= MathHelper.Lerp(minSpeed, maxSpeed, (float)random.NextDouble());
                particles[i].scale = MathHelper.Lerp(minScale, maxScale, (float)random.NextDouble());

                particles[i].location = location;
                particles[i].acceleration = acceleration;
                particles[i].lifespan = lifespan;
                particles[i].sprite = sprite;
                particles[i].color = new Color(1f, 0f,0f, 0.4f);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (active)
            {
                bool alive = false;
                foreach (Particle particle in particles)
                {
                    particle.Update(gameTime);
                    alive |= particle.lifespan > 0;
                }
                active = alive;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                foreach (Particle particle in particles)
                    particle.Draw(spriteBatch);
            }
        }

    }
}
