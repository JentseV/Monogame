using GameProject.Content;
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


namespace GameProject
{
    internal abstract class Character : IGameComponent
    {

        private float hitpoints;

        protected Rectangle hitbox;

        
        public float Hitpoints { get { return hitpoints; } set { hitpoints = value; } }

        private Texture2D _texture, _textureRunning, _textureIdling;
        private Texture2D _textureIdleFacingFront, _textureIdleFacingRight, _textureIdleFacingUp, _textureIdle;
        private Texture2D _textureRunRight, _textureUpRun, _textureDownRun;

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Texture2D TextureRunning { get { return _textureRunning; } set { _textureRunning = value;  } }
        public Texture2D TextureIdling { get { return _textureIdling; } set { _textureIdling = value; } }
        public Texture2D TextureUpRun { get { return _textureUpRun; } set { _textureUpRun = value; } }
        public Texture2D TextureIdleFacingFront { get { return _textureIdleFacingFront; } set { _textureIdleFacingFront = value; } }
        public Texture2D TextureIdleFacingRight { get { return _textureIdleFacingRight; } set { _textureIdleFacingRight = value; } }
        public Texture2D TextureIdleFacingUp { get { return _textureIdleFacingUp; } set { _textureIdleFacingUp = value; } }
        public Texture2D TextureRunRight { get { return _textureRunRight; } set { _textureRunRight = value; } }
        public Texture2D TextureIdle { get { return _textureIdle; } set { _textureIdle = value; } }
        public Texture2D TextureDownRun { get { return _textureDownRun; } set { _textureDownRun = value; } }


        private Vector2 position;
        private Vector2 direction;
        private Vector2 speed;
        public Vector2 Position { get { return position;  } set { position = value; } }
        public Vector2 Direction { get { return direction; } set { direction = value; } }
        public Vector2 Speed { get { return speed; } set { speed = value; } }
        public Vector2 Center { get ; set; }
        public Rectangle Hitbox { get ; set ; }


        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
