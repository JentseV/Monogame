using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameProject
{
    internal class MovementManager
    {
        public void Move(IMovable movable)
        {
            Vector2 direction = movable.InputReader.ReadInput();

            if (movable.InputReader.IsDestinationInput)
            {
                direction -= movable.Position;
                direction.Normalize();
            }

            Vector2 distance = direction * movable.Speed;
            movable.Position += distance;
        }
    }
}
