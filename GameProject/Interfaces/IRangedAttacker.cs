using GameProject.GameObjects.Dynamic.DynamicCollidables;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Interfaces
{
    internal interface IRangedAttacker
    {
        public List<Bullet> Bullets { get; set; }
        public void UpdateBullets(GameTime gameTime, List<ICollidable> collidables);
        public void DrawBullets(SpriteBatch spriteBatch);
    }
}
