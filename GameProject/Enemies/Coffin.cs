using GameProject.Content;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using SharpDX.WIC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace GameProject.Enemies
{
    internal class Coffin : Enemy, IMovable,IGameComponent
    {
        private SpriteEffects flip = SpriteEffects.None;

        private float attackCooldown = 2f;
        private float timeSinceLastAttack;
        private Texture2D hitboxText;

        private Texture2D _textureAttackRight, _textureAttackUp, _textureAttackFront;
        private Texture2D TextureAttacking;
        private bool moving,attacking;
        private Vector2 heroPos,facing;
        private Animation _animationIdle;
        private Animation _animationRun;
        private Animation _animationAttacking;
        public Coffin(Vector2 speed, Vector2 position, Texture2D[] textures)
        {
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 40, 40);
            this.Center = new Vector2(50 + position.X, 55 + position.Y);
            //SPRITESHEET 74x55
            this.Speed = speed;
            this.Position = position;
            this.TextureIdle = textures[0];
            this.TextureRunRight = textures[1];
            this.TextureUpRun = textures[2];
            this.TextureIdleFacingUp = textures[3];
            this.hitboxText = textures[9];
            this.TextureIdleFacingRight = textures[4];
            this.TextureIdleFacingFront = textures[0];
            this.TextureDownRun = textures[5];

            this._textureAttackRight = textures[7];
            this._textureAttackUp = textures[8];
            this._textureAttackFront = textures[6];

            this.TextureIdling = TextureIdle;
            this.TextureRunning = TextureRunRight;
            this.TextureAttacking = _textureAttackFront;

            timeSinceLastAttack = attackCooldown;

            _animationIdle = new Animation();
            _animationRun = new Animation();
            _animationAttacking = new Animation();
            _animationIdle.GetFramesFromTextureProperties(this.TextureIdle.Width, this.TextureIdle.Height, 6, 1);
            _animationRun.GetFramesFromTextureProperties(this.TextureRunRight.Width, this.TextureRunRight.Height, 14, 1);
            _animationAttacking.GetFramesFromTextureProperties(this.TextureAttacking.Width, this.TextureAttacking.Height, 18, 1);
        }

        public IInputReader InputReader { get ; set ; }

        public void Move()
        {
            this.Position += Direction * Speed;
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            if (moving)
            {
                spriteBatch.Draw(TextureRunning, Position, _animationRun.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, flip, 0f);
            }
            else if(attacking)
            {

                spriteBatch.Draw(TextureAttacking, Position, _animationAttacking.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, flip, 0f);
            }
            else
            {
                spriteBatch.Draw(TextureIdling, Position, _animationIdle.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, flip, 0f);
            }

            spriteBatch.Draw(hitboxText, Center , Hitbox, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
        }

        public new void Update(GameTime gameTime, Hero hero)
        {
            this.Center = new Vector2(this.Position.X - 32, this.Position.Y - 32);
            this.Hitbox = new Rectangle((int)Center.X, (int)Center.Y, 32, 32);
            

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastAttack -= deltaTime;

           
            heroPos = hero.Position;
            Direction = Vector2.Normalize(heroPos - Position);

            DecideAction();
            GetFacingDirection();
            DecideAnimation();
            UpdateAnimations(gameTime);
            
           
        }

        public void GetFacingDirection()
        {
            if (Direction.X > 0f && Direction.X > Direction.Y)
            {
               
                facing = new Vector2(1f, 0f);
            }
            else if (Direction.X < 0 && Direction.X < Direction.Y)
            {
                facing = new Vector2(-1f, 0f);
            }
            
            if (Direction.Y > 0 && Direction.Y > Direction.X)
            {
                facing = new Vector2(0f, 1f);
            }
            else if (Direction.Y < 0 && Direction.Y < Direction.X)
            {
                facing = new Vector2(0f, -1f);
            }
            
        }

        public void DecideAction()
        {
            if (Vector2.Distance(Position, heroPos) >= 32f)
            {
                moving = true;
                attacking = false;
                Move();
            }
            else
            {
                moving = false;
                Attack();
            }
        }
        private void DecideAnimation()
        {
            
                switch (facing)
                {
                    case Vector2(1f, 0f):
                        {
                            flip = SpriteEffects.None;
                            TextureIdling = TextureIdleFacingRight;
                            TextureRunning = TextureRunRight;
                            TextureAttacking = _textureAttackRight;
                            break;
                        }

                    case Vector2(-1f, 0f):
                        {
                            flip = SpriteEffects.FlipHorizontally;
                            TextureRunning = TextureRunRight;
                            TextureIdling = TextureIdleFacingRight;
                            TextureAttacking = _textureAttackRight;
                            break;
                        }
                    case Vector2(0f, -1f):
                        {
                            flip = SpriteEffects.None;
                            TextureIdling = TextureIdleFacingUp;
                            TextureRunning = TextureUpRun;
                            TextureAttacking = _textureAttackUp;
                            break;
                        }
                    case Vector2(0f, 1f):
                        {
                            flip = SpriteEffects.None;
                            TextureIdling = TextureIdleFacingFront;
                            TextureRunning = TextureDownRun;
                            TextureAttacking = _textureAttackFront;
                            break;
                        }
                }
            
        }

        private void Attack()
        {
            
            if (timeSinceLastAttack <= 0)
            {
                attacking = true;
                
                timeSinceLastAttack = attackCooldown;
                
            }
        }

        private void UpdateAnimations(GameTime gameTime)
        {
            if (moving)
            {
                _animationRun.Update(gameTime);
            }
            else if (attacking)
            {
                _animationAttacking.Update(gameTime);
            }
            else
            {
                _animationIdle.Update(gameTime);
            }
        }
    }
}
