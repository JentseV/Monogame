using GameProject.Animations;
using GameProject.GameObjects;
using GameProject.GameObjects.Characters.Player;
using GameProject.GameObjects.Dynamic;
using GameProject.Interfaces;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject.GameObjects.Dynamic
{
    internal abstract class DynamicGO : GameObject , Interfaces.IUpdateable
    {

        private Vector2 direction;
        private Vector2 speed;

        public Vector2 Direction { get { return direction; } set { direction = value; } }
        public Vector2 Speed { get { return speed; } set { speed = value; } }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Update(GameTime gameTime, Hero hero, List<ICollidable> collidables)
        {
            
        }
    }
}
