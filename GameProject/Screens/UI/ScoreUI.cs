using GameProject.Animations;
using GameProject.Enemies;
using GameProject.GameObjects.Characters.Player;
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


namespace GameProject.Screens.UI
{
    internal class ScoreUI : UI
    {
        public ScoreUI(SpriteFont fontIn) : base(fontIn)
        {
            
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            string temp = "\n\nCOINS : " + Hero.gold + "\n\nHEALTH : " + Hero.hitPoints2;
            Vector2 dimension = font.MeasureString(temp);
            Vector2 drawPos = new Vector2(10, 0);
            spriteBatch.DrawString(font, temp, drawPos, Color.SaddleBrown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
