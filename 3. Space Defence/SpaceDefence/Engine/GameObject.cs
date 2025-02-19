using SpaceDefence.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence
{
    public abstract class GameObject
    {
        protected Collider collider;

        /// <summary>
        /// Used to set the collider used for object collision.
        /// </summary>
        /// <param name="collider"> The collider to be used. </param>
        public void SetCollider(Collider collider)
        {
            this.collider = collider;
        }

        /// <summary>
        /// Override this method to load in assets for your class.
        /// </summary>
        /// <param name="content"> The ContentManager that handles file loading. </param>
        /// <example>
        /// To load a texture use: 
        /// content.Load<Texture2D>([texture name]);
        /// </example>
        public virtual void Load(ContentManager content)
        {

        }

        /// <summary>
        /// Override this if you want your GameObject to react to user input.
        /// </summary>
        /// <param name="inputManager"> Keeps track of user input. </param>
        public virtual void HandleInput(InputManager inputManager)
        {

        }

        /// <summary>
        /// Checks if this GameObject collides with the other GameObject
        /// </summary>
        /// <param name="other"> The GameObject to check collision with. </param>
        /// <returns></returns>
        public bool CheckCollision(GameObject other)
        {
            if (collider == null)
                return false;
            return collider.CheckIntersection(other.collider);
        }

        /// <summary>
        /// Override this if you want the GameObject to respond to collision events.
        /// Keep in mind that OnCollision will be called once on both Objects.
        /// </summary>
        /// <param name="other"> The GameObject with which it collided. </param>
        public virtual void OnCollision(GameObject other)
        {

        }

        /// <summary>
        /// Called every game step. Override this to keep your GameObject up to date.
        /// </summary>
        /// <param name="gameTime"> The amount of time that has elapsed since the last update call. </param>
        public virtual void Update(GameTime gameTime) 
        { 
        
        }
        /// <summary>
        /// Called every game step. Override this with any drawing code you wish to implement.
        /// SpriteBatch.Begin() and SpriteBatch.End() are called in the GameManager, so you should not call them for every draw call.
        /// </summary>
        /// <param name="gameTime"> The amount of time that has elapsed since the last draw call. </param>
        /// <param name="spriteBatch"> The Spritebatch to write your textures to. </param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        { 

        }

        /// <summary>
        /// Called when the GameObject is removed from the GameManager.
        /// </summary>
        public virtual void Destroy() 
        { 
        
        }
    }
}
