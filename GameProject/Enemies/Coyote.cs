using GameProject.Animations;
using GameProject.Enemies;
using GameProject.Pickups;
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
    internal class Coyote : Enemy,IEnemy
    {
        //SPRITESHEET 70x70
        private SpriteEffects flip = SpriteEffects.None;

        private Texture2D BulletTexture;
        private bool heroInRange = false;

        private float channelTime = 0f;

        private Texture2D hitboxText;

        public List<Bullet> fireBalls = new List<Bullet>();

        public Coyote(Vector2 speed, Vector2 position, Texture2D[] textures)
        {
            Movable = true;
            this.Hitpoints = 3;
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

            BulletTexture = textures[11];

            this.TextureIdling = TextureIdle;
            this.TextureRunning = TextureRunRight;
            this.TextureAttacking = TextureAttackFront;

            AttackCooldown = 0.5f;

            TimeSinceLastAttack = AttackCooldown;

            AnimationIdle = new Animation();
            AnimationRun = new Animation();
            AnimationAttacking = new Animation();
            AnimationHit = new Animation();

            AnimationHit.GetFramesFromTextureProperties(this.TextureHit.Width, this.TextureHit.Height, 1, 1);
            AnimationIdle.GetFramesFromTextureProperties(this.TextureIdle.Width, this.TextureIdle.Height, 7, 1);
            AnimationRun.GetFramesFromTextureProperties(this.TextureRunRight.Width, this.TextureRunRight.Height, 14, 1);
            AnimationAttacking.GetFramesFromTextureProperties(this.TextureAttacking.Width, this.TextureAttacking.Height, 24, 1);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            if (Moving && Movable == true)
            {
                
                spriteBatch.Draw(TextureRunning, Center, AnimationRun.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, Flip, 0f);
            }
            else if (Attacking)
            {

                spriteBatch.Draw(TextureAttacking, Center, AnimationAttacking.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, Flip, 0f);
            }

            else if (Hit)
            {

                spriteBatch.Draw(TextureHit, Center, null, Color.White, 0f, new Vector2(0f, 0f), 1f, Flip, 0f);
            }
            else
            {
                spriteBatch.Draw(TextureIdling, Center, AnimationIdle.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, Flip, 0f);
            }


            //spriteBatch.Draw(hitboxText, Position , Hitbox, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);

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
                Movable = false;
                Attacking = false;
                InvincibleTimer += deltaTime;
                if (InvincibleTimer > 1.5f)
                {
                    Invincible = false;
                    Hit = false;
                    Movable = true;
                    InvincibleTimer = 0f;
                }
            }

 


            HeroPos = hero.Position;

            
            CheckCollision(collidables);
            OnDeath(collidables);
            DecideAction();
            GetFacingDirection();
            DecideAnimation();
            UpdateAnimations(gameTime);
            

        }

        

        public void DecideAction()
        {

            if (Vector2.Distance(HeroPos, this.Position) < 400f)
            {
                heroInRange = true;
            }
            else
            {
                Moving = true;
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
        

        private new  void Attack()
        {

            if (TimeSinceLastAttack <= 0 && Invincible == false)
            {
                Attacking = true;
                //if (Attacking)
                //{
                //    channelTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                //    if (channelTime > 3f)
                //    {
                //        Debug.WriteLine("Spawn meteor at " + hero.Position);

                //        channelTime = 0f;
                //    }
                //}

                TimeSinceLastAttack = AttackCooldown;
                
            }
        }

       
        protected new void CheckCollision(List<ICollidable> collidables)
        {
            if(Dead == false)
            {
                foreach (ICollidable collidable in collidables)
                {

                    if (this.Hitbox.Intersects(collidable.Hitbox) && !(collidable is Coyote) && !(collidable is Pickup))
                    {
                        if (collidable is Bullet)
                        {
                            Bullet b = collidable as Bullet;
                            if (b.Tag == "BulletHero" && Invincible == false)
                            {
                                TakeDamage(b.Damage);
                            }

                        }

                    }

                }
            }
            

        }


    }
}

