using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    internal class MouseReader : IInputReader
    {
        public bool IsDestinationInput => true;

        public Vector2 ReadInput()
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 directionMouse = new Vector2(mouseState.X, mouseState.Y);

           
            return directionMouse;
        }
    }
}
