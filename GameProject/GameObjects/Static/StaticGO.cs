using GameProject.Animations;
using GameProject.GameObjects;
using GameProject.GameObjects.Dynamic;
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

namespace GameProject.GameObjects.Static
{
    internal abstract class StaticGO : GameObject
    {

        private Texture2D texture;
        public Texture2D Texture { get { return texture; } set { texture = value; } }

    }
}
