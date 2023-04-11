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


namespace GameProject.Enemies
{
    internal class Coffin : Enemy , IEnemy
    {
        private SpriteEffects flip = SpriteEffects.None;

        private bool heroInRange = false;
        private Texture2D hitboxText;

        
        private Vector2 heroPos,facing;
        private Animation _animationIdle;
        private Animation _animationRun;
        private Animation _animationAttacking;
        private Animation _animationHit;
        public Coffin(Vector2 speed, Vector2 position, Texture2D[] textures)
        {
            this.Hitpoints = 3;
            
            
            //SPRITESHEET 74x55
            this.Speed = speed;
            this.Position = position;

            this.Center = new Vector2(50 + Position.X, 55 + Position.Y);
            this.Hitbox = new Rectangle((int)Center.X, (int)Center.Y, 45, 45);
            this.TextureIdle = textures[0];
            this.TextureRunRight = textures[1];
            this.TextureUpRun = textures[2];
            this.TextureIdleFacingUp = textures[3];
            this.hitboxText = textures[9];
            this.TextureIdleFacingRight = textures[4];
            this.TextureIdleFacingFront = textures[0];
            this.TextureDownRun = textures[5];

            this.TextureAttackRight = textures[7];
            this.TextureAttackUp = textures[8];
            this.TextureAttackFront = textures[6];
            this.TextureHit = textures[10];

            this.TextureIdling = TextureIdle;
            this.TextureRunning = TextureRunRight;
            this.TextureAttacking = TextureAttackFront;

            TimeSinceLastAttack = AttackCooldown;

            _animationIdle = new Animation();
            _animationRun = new Animation();
            _animationAttacking = new Animation();
            _animationHit = new Animation();

            _animationHit.GetFramesFromTextureProperties(this.TextureHit.Width, this.TextureHit.Height, 1, 1);
            _animationIdle.GetFramesFromTextureProperties(this.TextureIdle.Width, this.TextureIdle.Height, 6, 1);
            _animationRun.GetFramesFromTextureProperties(this.TextureRunRight.Width, this.TextureRunRight.Height, 14, 1);
            _animationAttacking.GetFramesFromTextureProperties(this.TextureAttacking.Width, this.TextureAttacking.Height, 18, 1);
        }


        public new void Draw(SpriteBatch spriteBatch)
        {
            if (Moving && Invincible == false)
            {
                spriteBatch.Draw(TextureRunning, Center, AnimationRun.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, flip, 0f);
            }
            else if(Attacking && Invincible == false)
            {

                spriteBatch.Draw(TextureAttacking, Center, AnimationAttacking.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, flip, 0f);
            }

            else if (Hit && Invincible == true)
            {
                spriteBatch.Draw(TextureHit, Center, null, Color.White, 0f, new Vector2(0f, 0f), 1f, flip, 0f);
            }
            else
            {
                spriteBatch.Draw(TextureIdling, Center, AnimationIdle.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, flip, 0f);
            }

             //spriteBatch.Draw(hitboxText, Center , Hitbox, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);
            
        }

        public void Update(GameTime gameTime, Hero hero, List<ICollidable> collidables)
        {
            this.Center = new Vector2((int)Position.X, (int)Position.Y);
            this.hitbox.X = (int)Center.X;
            this.hitbox.Y = (int)Center.Y;
            

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TimeSinceLastAttack -= deltaTime;

            if (Hit)
            {
                Invincible = true;
                Moving = false;
                Attacking = false;
                InvincibleTimer += deltaTime;
                if(InvincibleTimer > 1.5f)
                {
                    Invincible = false;
                    Hit = false;
                    InvincibleTimer = 0f;
                }
            }

            if(Attacking == false)
            {
                this.hitbox.Width = 45;
            }

            heroPos = hero.Position;
           

            CheckCollision(collidables);
            DecideAction();
            GetFacingDirection();
            DecideAnimation();
            UpdateAnimations(gameTime);
            
           
        }

        public void GetFacingDirection()
        {
            Direction = Vector2.Normalize(heroPos - Position);
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

            if (Vector2.Distance(heroPos, this.Position) < 32f)
            {
                heroInRange = true;
            }
            else
            {
                Attacking = false;
                heroInRange = false;
            }

            if (!Attacking && Invincible == false && heroInRange == false)
            {
                Moving = true;
                Attacking = false;
                Move();
            }
            else
            {
                Moving = false;
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
                            TextureAttacking = TextureAttackRight;
                        
                            break;
                        }

                    case Vector2(-1f, 0f):
                        {
                            flip = SpriteEffects.FlipHorizontally;
                            TextureRunning = TextureRunRight;
                            TextureIdling = TextureIdleFacingRight;
                            TextureAttacking = TextureAttackRight;
                            break;
                        }
                    case Vector2(0f, -1f):
                        {
                            flip = SpriteEffects.None;
                            TextureIdling = TextureIdleFacingUp;
                            TextureRunning = TextureUpRun;
                            TextureAttacking = TextureAttackUp;
                            break;
                        }
                    case Vector2(0f, 1f):
                        {
                            flip = SpriteEffects.None;
                            TextureIdling = TextureIdleFacingFront;
                            TextureRunning = TextureDownRun;
                            TextureAttacking = TextureAttackFront;
                            break;
                        }
                }
            
        }

        private void Attack()
        {
            
            if (TimeSinceLastAttack <= 0 && Invincible == false)
            {
                Attacking = true;
                
                TimeSinceLastAttack = AttackCooldown;
                this.hitbox.Width = 55;
            }
        }

        private void UpdateAnimations(GameTime gameTime)
        {
            if (Moving)
            {
                AnimationRun.Update(gameTime);
            }
            else if (Attacking)
            {
                AnimationAttacking.Update(gameTime);
            }
            else
            {
                AnimationIdle.Update(gameTime);
            }
        }

        protected new void CheckCollision(List<ICollidable> collidables)
        {
            
            foreach (ICollidable collidable in collidables)
            {
                
                if (this.Hitbox.Intersects(collidable.Hitbox) && !(collidable is Coffin) )
                {
                    if (collidable is Bullet)
                    {
                        Bullet b = collidable as Bullet;
                        if(b.Tag == "BulletHero" && Invincible == false)
                        {
                                TakeDamage(1f);
                        }   
                        
                    }
                    
                    
                }
                
            }
            
        }

        
    }
}
