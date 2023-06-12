using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Interfaces;

namespace GameProject.GameObjects.Dynamic.DynamicCollidables
{
    internal abstract class DynamicCollidable : DynamicGO, ICollidable
    {
        public virtual void CheckCollision(ICollidable collidables)
        {
            
        }
    }
}
