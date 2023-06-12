using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Interfaces
{
    internal interface IAnimated
    {
        public void InitializeTextures(Texture2D[] textures);
        public void InitializeAnimations();
    }
}
