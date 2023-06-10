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
    internal class UpgradeUI : UI
    {
        private List<Button> buttons = new List<Button>();
        public UpgradeUI(SpriteFont fontIn, Texture2D textures, Hero h) : base(fontIn)
        {
            buttons.Add(new Button(textures, new Vector2(200f, 200f),  () => h.IncreaseSpeed(new Vector2(0.1f,0.1f))));
            buttons.Add(new Button(textures, new Vector2(500f, 200f),  () => h.IncreaseDamage(1f)));
            
        }

        public void Update(MouseState mouse)
        {
            foreach(Button button in buttons)
            {
                button.Update(mouse);
            }
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            foreach(Button b in buttons)
            {
                b.Draw(spriteBatch);
            }
        }

        public void ResetButtons()
        {
            foreach (Button b in buttons)
            {
                b.Remove = false;
            }
        }
    }
}
