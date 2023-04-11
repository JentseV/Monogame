using GameProject.Animations;
using GameProject.Enemies;
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
namespace GameProject.Pickups
{
    internal class Coin : Pickup
    {
        public Coin(int idIn, string tagIn, Vector2 postionIn, float timeTillDespawnIn) : base(idIn, tagIn, postionIn, timeTillDespawnIn)
        {
            this.Texture = Game1.coinTexture;
            this.Animation = new Animation();
            this.Animation.GetFramesFromTextureProperties(Texture.Width, Texture.Height, 6, 1);

        }

        
    }
}
