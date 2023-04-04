using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace GameProject.Content
{
    internal class Hero : IMovable , IGameComponent
    {
        private Texture2D _texture, _textureRun, _textureIdle, _textureUpRun , _textureDownRun;
        private Texture2D _textureIdleFacingFront, _textureIdleFacingRight, _textureIdleFacingUp;
        private Texture2D _textureShootUp, _textureShootFront, _textureShootRight, _textureShootLeft;
        private Texture2D _textureRunRight;
        private Texture2D hitboxText;

        private string facing;
        string facing2 = null;
        private SpriteEffects flip = SpriteEffects.None;
        private Animation _animation;
        KeyboardState state;
        private Keys lastKeyPress;

        private Vector2 position;
        private Vector2 speed;
        private Vector2 direction;
        private Vector2 center;


        private Rectangle hitbox;

        public  Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Vector2 Direction
        {
            get { return direction; }
            set { direction = value; }
        }


        private IInputReader inputReader;
        public IInputReader InputReader
        {
            get { return inputReader; }
            set { inputReader = value; }
        }

        public Vector2 Center
        {
            get
            {
                return center;
            }
            set
            {
                center = value;
            }
        }
        public Rectangle Hitbox {
            get
            {
                return hitbox;
            }
            set
            {
                hitbox = value;
            }
        }

        private MovementManager movementManager;
        public Hero(Texture2D[] textures, IInputReader inputReader)
        {
            this._texture = textures[3];
            this._textureIdle = textures[3];
            this._textureUpRun = textures[0];
            this._textureDownRun = textures[1];
            this._textureShootFront = textures[2];
            this._textureIdleFacingUp = textures[5];
            this._textureIdleFacingRight = textures[4];
            this._textureIdleFacingFront = textures[3];
            this._textureRunRight = textures[6];
            this._textureShootRight = textures[7];
            this._textureShootUp = textures[8];
            hitboxText = textures[10];
            position = new Vector2(2f, 2f);
            
            this.Hitbox = new Rectangle((int)this.position.X, (int)this.position.Y, 48,43);
            this.Center = new Vector2(this.position.X + this._texture.Width / 2f, this.position.Y + this._texture.Height / 2f);
            speed = new Vector2(3f, 3f);
            _animation = new Animation();
            _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 6, 1);
            // (48,43)
            this.movementManager = new MovementManager();
            this.inputReader = inputReader;
        }


        public void Draw(SpriteBatch spriteBatch)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 8, 1);
                spriteBatch.Draw(_texture, position, _animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 2f,flip, 0f);

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 8, 1);
                spriteBatch.Draw(_texture, position, _animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 2f, flip, 0f);

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 8, 1);
                spriteBatch.Draw(_texture, position, _animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 2f, SpriteEffects.None, 0f);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 8, 1);
                spriteBatch.Draw(_texture, position, _animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 2f, SpriteEffects.None, 0f);
            }

            else
            {
                _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 6, 1);
                spriteBatch.Draw(_texture, position, _animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 2f, flip, 0f);
            }

             spriteBatch.Draw(hitboxText, Center, Hitbox, Color.White, 0f , new Vector2(0f,0f), 1f,SpriteEffects.None,0f);
            
        }


        public void Update(GameTime gameTime)
        {
            
            this.Center = new Vector2(this.position.X + 48 / 2f, this.position.Y + 43 / 2f);
            this.hitbox.X = (int)Center.X;
            this.hitbox.Y = (int)Center.Y;
            state = Keyboard.GetState();

            foreach(Keys k in state.GetPressedKeys())
            {
                lastKeyPress = k;
                if(k == Keys.Up)
                {
                    facing = "Up";
                }
                else if ( k == Keys.Down)
                {
                    facing = "Down";
                }
                else if( k == Keys.Right)
                {
                    facing = "Right";
                }
                else if(k == Keys.Left)
                {
                    facing = "Left";
                }
            }
 
            DecideAnimation();
            Move();
            _animation.Update(gameTime);
        }

        private void DecideAnimation()
        {
            direction = inputReader.ReadInput();

            if (direction.X > 0 )
            {
                flip = SpriteEffects.None;
                _texture = _textureRunRight;
            }

            else if(direction.X < 0)
            {
                flip = SpriteEffects.FlipHorizontally;
                _texture = _textureRunRight;
            }

            else if( direction.Y < 0)
            {
                flip = SpriteEffects.None;
                _texture = _textureUpRun;
            }

            else if (direction.Y > 0)
            {

                flip = SpriteEffects.None;
                _texture = _textureDownRun;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 6, 1);
                switch (facing)
                {
                    case "Up":
                        {
                            _texture = _textureShootUp;
                            break;
                        }
                    case "Down":
                        {
                            _texture = _textureShootFront;
                            break;
                        }
                    case "Right":
                        {
                            
                            _texture = _textureShootRight;
                            break;
                        }
                    case "Left":
                        {
                            flip = SpriteEffects.FlipHorizontally;
                            _texture = _textureShootRight;
                            break;
                        }
                }
                
            }

            else
            {
                switch (facing)
                {
                    case "Up":
                        {
                            _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 6, 1);
                            _texture = _textureIdleFacingUp;
                            break;
                        }

                    case "Right":
                        {
                            _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 6, 1);
                            flip = SpriteEffects.None;
                            _texture = _textureIdleFacingRight;
                            break;
                        }

                    case "Left":
                        {
                            _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 6, 1);
                            flip = SpriteEffects.FlipHorizontally;
                            _texture = _textureIdleFacingRight;
                            break;
                        }
                    case "Down":
                        {
                            _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 6, 1);
                            _texture = _textureIdleFacingFront;
                            break;
                        }

                    default:
                        {
                            _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 6, 1);
                            _texture = _textureIdle;
                            break;
                        }
                }

            }

        }
        public void ChangeInput(IInputReader inputReader)
        {
            this.inputReader = inputReader;
        }

        public void Move()
        {
            movementManager.Move(this);
        }

        public void CheckCollision(Rectangle[] rect)
        {
            foreach(Rectangle r in rect)
            {
                if (this.hitbox.Intersects(r))
                {
                    if (facing2 == null) facing2 = facing;
                    if (facing.Contains(facing2))
                    {
                        Speed = Vector2.Zero;
                        break;
                    }
                    else
                    {
                        Speed = new Vector2(3f, 3f);
                        break;
                    }
                }
                else
                {
                    facing2 = null;
                }
            }
        }
    }
}
