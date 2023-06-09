using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.GameObjects.Dynamic.DynamicCollidables
{
    internal abstract class DynamicCollidable : DynamicGO, ICollidable
    {
        public virtual void CheckCollision(ICollidable collidables)
        {
            
        }
    }
}
