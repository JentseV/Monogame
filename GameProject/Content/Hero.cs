﻿using GameProject.Projectiles;
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
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace GameProject.Content
{
    internal class Hero : Character, IMovable , IGameComponent
    {
        private Texture2D _texture, _textureRunning, _textureIdling , _textureShooting , _textureUpRun , _textureDownRun;
        private Texture2D _textureIdleFacingFront, _textureIdleFacingRight, _textureIdleFacingUp, _textureIdleFacingUpRight, _textureIdleFacingDownRight, _textureIdle;
        private Texture2D _textureShootUp, _textureShootFront, _textureShootRight, _textureShootUpRight, _textureShootDownRight;
        private Texture2D _textureRunRight, _textureRunUpRight, _textureRunDownRight;
        private Texture2D hitboxText;

        private List<Bullet> bullets = new List<Bullet>();

        

        private float shotCooldown;
        private float timeSinceLastShot;

        private Texture2D bulletTexture;
        private bool moving = false, movable= true, shooting=false, canShoot= false;
        private Vector2 facing;
        private SpriteEffects flip = SpriteEffects.None;
        private Animation _animation;
        private Animation _animationRun;
        private Animation _animationShooting;

        private Vector2 position;
        private Vector2 speed;
        private Vector2 direction;
       
        private Rectangle hitbox;

        public new Vector2 Position
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

        private float timerToShoot = 0;
        public new Rectangle Hitbox {
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
            this._textureRunDownRight = textures[11];
            this._textureRunUpRight = textures[12];
            this._textureShootDownRight = textures[13];
            this._textureShootUpRight = textures[14];
            this._textureIdleFacingUpRight = textures[15];
            this._textureIdleFacingDownRight = textures[16];
            this.bulletTexture = textures[17];
            _textureIdling = _textureIdle;
            _textureRunning = _textureRunRight;
            _textureShooting = _textureShootUp;

            hitboxText = textures[10];
            position = new Vector2(2f, 2f);
            
            this.Hitbox = new Rectangle((int)this.position.X, (int)this.position.Y, 48,43);


            shotCooldown = 0.5f;
            timeSinceLastShot = shotCooldown;
            speed = new Vector2(3f, 3f);

            _animation = new Animation();
            _animationRun = new Animation();
            _animationShooting = new Animation();

            _animation.GetFramesFromTextureProperties(_texture.Width, _texture.Height, 6, 1);
            _animationRun.GetFramesFromTextureProperties(_textureRunning.Width, _textureRunning.Height, 8, 1);
            _animationShooting.GetFramesFromTextureProperties(_textureShooting.Width, _textureShooting.Height, 6, 1);
            
            this.movementManager = new MovementManager();
            this.inputReader = inputReader;
        }


        public new void Draw(SpriteBatch spriteBatch)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && canShoot)
            {
                
                spriteBatch.Draw(_textureShooting, position, _animationShooting.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, flip, 0f);
            }
            else if( moving)
            {
                
                spriteBatch.Draw(_textureRunning, position, _animationRun.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, flip, 0f);
            }
            else
            {
                
                spriteBatch.Draw(_textureIdling, position, _animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, flip, 0f);
            }

            foreach(Bullet b in bullets)
            {
                b.Draw(spriteBatch);
            }

            //spriteBatch.Draw(hitboxText, Center, Hitbox, Color.White, 0f , new Vector2(0f,0f), 1f,SpriteEffects.None,0f);
            
        }


        public new void Update(GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastShot -= deltaTime;

            

            this.Center = new Vector2(this.position.X + 48 / 2f, this.position.Y + 43 / 2f);
            this.hitbox.X = (int)Center.X;
            this.hitbox.Y = (int)Center.Y;

            direction = inputReader.ReadInput();
            if (direction.X > 0 || direction.X < 0 || direction.Y > 0 || direction.Y < 0)
            {
                moving = true;
                shooting = false;
                canShoot = false;
            }
            else
            {
                moving = false;
                canShoot = true;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Space) && canShoot){
                Shoot();
            }

            Move();
            GetFacingDirection();
            DecideAnimation();
            UpdateAnimations(gameTime);

            Bullet toDelete = null;
            foreach (Bullet b in bullets)
            {
                b.Update(gameTime);
                if (b.Destroy()) {
                    toDelete = bullets.Find(o => o.id == b.id);
                };
            }

            if (toDelete != null)
            {
                bullets.Remove(toDelete);
            }

            
        }

        private void GetFacingDirection()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                facing = new Vector2(1f, 0f); 
            }
             if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                facing = new Vector2(-1f, 0f);
            }
             if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                facing = new Vector2(0f, -1f);
            }
             if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                facing = new Vector2(0f, 1f);
            }
             if(Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                facing = new Vector2(1f, -1f);
            }
             if(Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                facing = new Vector2(-1f, -1f);
            }
             if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                facing = new Vector2(-1f, 1f);
                
            }
             if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                facing = new Vector2(1f, 1f);
            }

        }

        private void DecideAnimation()
        {
            
            if (moving)
            {
                switch (facing)
                {
                    case Vector2(1f, 0f):
                        {
                            flip = SpriteEffects.None;
                            _textureIdling = _textureIdleFacingRight;
                            _textureShooting = _textureShootRight;
                            _textureRunning = _textureRunRight;
                            break;
                        }

                    case Vector2(-1f, 0f):
                        {
                            flip = SpriteEffects.FlipHorizontally;
                            _textureRunning = _textureRunRight;
                            _textureIdling = _textureIdleFacingRight;
                            _textureShooting = _textureShootRight;
                            break;
                        }
                    case Vector2(0f, -1f):
                        {
                            flip = SpriteEffects.None;
                            _textureIdling = _textureIdleFacingUp;
                            _textureShooting = _textureShootUp;
                            _textureRunning = _textureUpRun;
                            break;
                        }
                    case Vector2(0f, 1f):
                        {
                            flip = SpriteEffects.None;
                            _textureIdling = _textureIdleFacingFront;
                            _textureShooting = _textureShootFront;
                            _textureRunning = _textureDownRun;
                            break;
                        }

                    case Vector2(1f, -1f):
                        {
                            flip = SpriteEffects.None;
                            _textureRunning = _textureRunUpRight;
                            _textureIdling = _textureIdleFacingUpRight;
                            _textureShooting = _textureShootUpRight;
                            break;
                        }

                    case Vector2(-1f, -1f):
                        {
                            flip = SpriteEffects.FlipHorizontally;
                            _textureRunning = _textureRunUpRight;
                            _textureIdling = _textureIdleFacingUpRight;
                            _textureShooting = _textureShootUpRight;
                            break;
                        }

                    case Vector2(1f, 1f):
                        {
                            flip = SpriteEffects.None;
                            _textureRunning = _textureRunDownRight;
                            _textureIdling = _textureIdleFacingDownRight;
                            _textureShooting = _textureShootDownRight;
                            break;
                        }

                    case Vector2(-1f, 1f):
                        {
                            flip = SpriteEffects.FlipHorizontally;
                            _textureRunning = _textureRunDownRight;
                            _textureIdling = _textureIdleFacingDownRight;
                            _textureShooting = _textureShootDownRight;
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
          
             if(movable) movementManager.Move(this);
            
        }

        public void UpdateAnimations(GameTime gameTime)
        {
            if (moving)
            {
                _animationRun.Update(gameTime);
            }else if (shooting)
            {
                _animationShooting.Update(gameTime);
            }
            else
            {
                _animation.Update(gameTime);
            }
            
            
        }

        public void CheckCollision(Rectangle[] rect)
        {

            foreach (Rectangle r in rect)
            {
                if (this.hitbox.Intersects(r))
                {
                    
                }
                else
                {

                }
            }
        }

        public void Shoot()
        {
            if (timeSinceLastShot <= 0)
            {
                shooting = true;
                Bullet bullet = new Bullet(bullets.Count,Position, facing, new Vector2(2f, 2f), bulletTexture);
                bullets.Add(bullet);
                timeSinceLastShot = shotCooldown;
            }
            
            
        }
    }
}
