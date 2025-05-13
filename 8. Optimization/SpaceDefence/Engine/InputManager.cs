using System.Data;
using System.Reflection.PortableExecutable;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefence
{
    public class InputManager
    {
        public KeyboardState LastKeyboardState { get; private set; }
        public KeyboardState CurrentKeyboardState { get; private set; }
        public MouseState LastMouseState { get; private set; }
        public MouseState CurrentMouseState { get; private set; }



        /// <summary>
        /// Keeps track of input states and contains methods to work with them.
        /// </summary>
        public InputManager()
        {
            LastKeyboardState = Keyboard.GetState();
            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
            LastMouseState = Mouse.GetState();

        }
        
        /// <summary>
        /// Updates the current and previous keyboard and mouse states
        /// </summary>
        public void Update()
        {
            LastKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        /// <summary>
        /// Gets whether the <paramref name="key"/> is currently down.
        /// </summary>
        /// <param name="key">The key for which you wish to know the state</param>
        /// <returns>true if the key is currently down, otherwise false</returns>
        public bool IsKeyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }


        /// <summary>
        /// Gets whether the <paramref name="key"/> is currently up.
        /// </summary>
        /// <param name="key">The key for which you wish to know the state</param>
        /// <returns>true if the key is currently up, otherwise false</returns>
        public bool IsKeyUp(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key);
        }



        /// <summary>
        /// Gets whether the <paramref name="key"/> was pressed in this frame.
        /// </summary>
        /// <param name="key">The key for which you wish to know the state</param>
        /// <returns>true if the key is currently down and was up in the previous step, otherwise false</returns>
        public bool IsKeyPress(Keys key) 
        {
            return CurrentKeyboardState.IsKeyDown(key) && LastKeyboardState.IsKeyUp(key);
        }


        /// <summary>
        /// Gets whether the left mouse button was pressed in this frame.
        /// </summary>
        /// <returns>true if the button is currently down and was up in the previous step, otherwise false</returns>
        public bool LeftMousePress()
        {
            return CurrentMouseState.LeftButton == ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Released;
        }


        /// <summary>
        /// Gets whether the right mouse button was pressed in this frame.
        /// </summary>
        /// <returns>true if the button is currently down and was up in the previous step, otherwise false</returns>
        public bool RightMousePress()
        {
            return CurrentMouseState.RightButton == ButtonState.Pressed && LastMouseState.RightButton == ButtonState.Released;
        }
    }
}