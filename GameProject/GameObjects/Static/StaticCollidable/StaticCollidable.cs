using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Interfaces;

namespace GameProject.GameObjects.Static.StaticCollidable
{
    internal  abstract class StaticCollidable : StaticGO, ICollidable
    {
        public virtual void CheckCollision(ICollidable collidables)
        {
            
        }
    }
}
