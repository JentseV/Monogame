
using GameProject.GameObjects.Dynamic.DynamicCollidables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.GameObjects.Dynamic.Characters.Enemies
{
    internal static class CollisionManager
    {
        public static void CheckCollisions(List<DynamicCollidable> gameObjects)
        {
            
            foreach (var collidable in gameObjects.OfType<ICollidable>())
            {
                foreach (var otherCollidable in gameObjects.OfType<ICollidable>())
                {
                    if (collidable != otherCollidable)
                    {
                        collidable.CheckCollision(otherCollidable);
                    }
                }
            }
        }
    }
}
