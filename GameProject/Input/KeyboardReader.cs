using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    internal class KeyboardReader : IInputReader
    {
        public bool IsDestinationInput => false;
        public Vector2 ReadInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                direction.X += 1f;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                direction.X -= 1f;
                
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                direction.Y += 1f;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                direction.Y -= 1f;
            }

            return direction;
            
        }
    }
}
