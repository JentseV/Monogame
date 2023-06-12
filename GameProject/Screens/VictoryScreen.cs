using GameProject.GameObjects.Characters.Player;
using GameProject.Screens.UI;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace GameProject.Screens
{
    internal class VictoryScreen : GameScreen
    {
        private Button exitButton;
        public VictoryScreen(Texture2D[] buttonText, SpriteFont font, Hero hero) : base(buttonText, font, hero)
        {
            exitButton = new Button(buttonText[1], new Microsoft.Xna.Framework.Vector2(425f, 368f), () =>
            {
                Environment.Exit(0);
            });
            Buttons.Add(exitButton);

        }
    }
}
