using GameProject.Animations;
using GameProject.GameObjects;
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

namespace GameProject.GameObjects
{
    internal abstract class GameObject
    {

        protected bool remove;
        protected Rectangle hitbox;
        private Vector2 position;
        private Vector2 center;

        public bool Remove { get { return remove; } set { remove = value; } }
        public Vector2 Center { get { return center; } set { center = value; } }
        public Rectangle Hitbox { get { return hitbox; } set { hitbox = value; } }

        public Vector2 Position { get { return position; } set { position = value; } }
    }
}
