using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace GameProject
{
    internal class Animation
    {
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames = new List<AnimationFrame>();
        private int counter;
        private double secondCounter = 0;

        public Animation()
        {
            frames = new List<AnimationFrame>();

        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
            
        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];

            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;

            int fps = 15;

            
            if (secondCounter >= 1d / fps)
            {
                counter++;
                secondCounter = 0;
             
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }

        }

        public void GetFramesFromTextureProperties(int width, int height, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            int widthOfFrame = width / numberOfWidthSprites;
            int heightOfFrame = height / numberOfHeightSprites;

            for (int i = 0; i <= height - heightOfFrame; i += heightOfFrame)
            {
                for (int j = 0; j <= width - widthOfFrame; j += widthOfFrame)
                {
                    
                    frames.Add(new AnimationFrame(new Rectangle(j, i, widthOfFrame, heightOfFrame)));
                    
                }
            }
        }
    }
}
