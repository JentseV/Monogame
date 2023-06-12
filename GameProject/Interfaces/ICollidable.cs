using GameProject.Animations;
using GameProject.Enemies;
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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject.Interfaces
{
    internal interface ICollidable
    {
        Vector2 Position { get; set; }
        bool Remove { get; set; }
        Rectangle Hitbox { get; set; }

        void CheckCollision(ICollidable collidables);
    }
}
