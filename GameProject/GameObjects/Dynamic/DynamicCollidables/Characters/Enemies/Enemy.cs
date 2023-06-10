using GameProject.Animations;
using GameProject.Characters;
using GameProject.GameObjects;
using GameProject.GameObjects.Characters.Player;
using GameProject.GameObjects.Dynamic.DynamicCollidables.Characters.Enemies.Cactus;
using GameProject.Pickups;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GameProject.Enemies
{
    internal abstract class Enemy : Character, ICollidable
    {
       
        private Vector2 heroPos, facing;
        private float range;
        private bool heroInRange = false;
        public Vector2 Facing { get { return facing; } set { facing = value; } }
        public Vector2 HeroPos { get { return heroPos; } set { heroPos = value; } }

        public float Range { get { return range; } set { range = value; } }

   

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
                if (InvincibleTimer > 1.5f)
                {
                    Invincible = false;
                    Hit = false;
                    InvincibleTimer = 0f;
                }
            }

            HeroPos = hero.Position;

            OnDeath();
            GetFacingDirection();
            DecideAction();
            DecideAnimation();
            UpdateAnimations(gameTime);

        }


        public void DecideAction()
        {

            if (Vector2.Distance(HeroPos, this.Position) < Range)
            {
                heroInRange = true;
            }
            else
            {
                Attacking = false;
                heroInRange = false;
            }

            if (!Attacking && Invincible == false && heroInRange == false )
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

        protected virtual void Attack()
        {
            
        }


        protected void DecideAnimation()
        {

            switch (Facing)
            {
                case Vector2(1f, 0f):
                    {
                        Flip = SpriteEffects.None;
                        TextureIdling = TextureIdleFacingRight;
                        TextureRunning = TextureRunRight;
                        TextureAttacking = TextureAttackRight;
                        break;
                    }

                case Vector2(-1f, 0f):
                    {
                        Flip = SpriteEffects.FlipHorizontally;
                        TextureRunning = TextureRunRight;
                        TextureIdling = TextureIdleFacingRight;
                        TextureAttacking = TextureAttackRight;
                        break;
                    }
                case Vector2(0f, -1f):
                    {
                        Flip = SpriteEffects.None;
                        TextureIdling = TextureIdleFacingUp;
                        TextureRunning = TextureUpRun;
                        TextureAttacking = TextureAttackUp;
                        break;
                    }
                case Vector2(0f, 1f):
                    {
                        Flip = SpriteEffects.None;
                        TextureIdling = TextureIdleFacingFront;
                        TextureRunning = TextureDownRun;
                        TextureAttacking = TextureAttackFront;
                        break;
                    }
            }

        }

        protected void GetFacingDirection()
        {
            Direction = Vector2.Normalize(HeroPos - Position);
            if (Direction.X > 0f && Direction.X > Direction.Y)
            {

                Facing = new Vector2(1f, 0f);
            }
            else if (Direction.X < 0 && Direction.X < Direction.Y)
            {
                Facing = new Vector2(-1f, 0f);
            }

            if (Direction.Y > 0 && Direction.Y > Direction.X)
            {
                Facing = new Vector2(0f, 1f);
            }
            else if (Direction.Y < 0 && Direction.Y < Direction.X)
            {
                Facing = new Vector2(0f, -1f);
            }

        }

        protected void UpdateAnimations(GameTime gameTime)
        {
            //AnimationRun.Update(gameTime);
            //AnimationAttacking.Update(gameTime);
            if (Moving)
            {
                
                AnimationRun.Update(gameTime);
            }
            else if (Attacking)
            {
                
                AnimationAttacking.Update(gameTime);
            }
            else if (Hit)
            {
                AnimationHit.Update(gameTime);
            }

        }
        public void OnDeath()
        {
            if (Dead)
            {

                Moving = false;
                Attacking = false;
                Random r = new Random();
                //Pickup p = Pickup.SpawnPickup(r.Next(),"Pickup",this.Position,5f); // move login to PickupManager
                //collidables.Add(p);
                Remove = true;
            }
        }


        public override void CheckCollision(ICollidable collidables)
        {
                if (this.Hitbox.Intersects(collidables.Hitbox) && !(collidables is Cactus))
                {
                    if (collidables is Bullet)
                    {
                        Bullet b = collidables as Bullet;
                        if (b.Tag == "BulletHero" && Invincible == false)
                        {
                            TakeDamage(b.Damage);
                        }
                    }

                }
        }

    }
}
