using GameProject.Animations;
using GameProject.Characters;
using GameProject.Pickups;
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

        private SpriteEffects flip = SpriteEffects.None;
        private Vector2 heroPos, facing;
        private float range;
        private bool heroInRange = false;
        public Vector2 Facing { get { return facing; } set { facing = value; } }
        public Vector2 HeroPos { get { return heroPos; } set { heroPos = value; } }
        public SpriteEffects Flip { get { return flip; } set { flip = value; } }

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
                if (InvincibleTimer > 1.5f)
                {
                    Invincible = false;
                    Hit = false;
                    InvincibleTimer = 0f;
                }
            }

            HeroPos = hero.Position;

            CheckCollision(collidables);
            OnDeath(collidables);
            
            GetFacingDirection();
            DecideAnimation();
            UpdateAnimations(gameTime);

        }


       
        protected void DecideAnimation()
        {

            switch (Facing)
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
            if (Moving)
            {
                AnimationRun.Update(gameTime);
            }
            else if (Attacking)
            {
                AnimationAttacking.Update(gameTime);
            }
          
        }
        public void OnDeath(List<ICollidable> collidables)
        {
            if (Dead)
            {
                
                Moving = false;
                Attacking = false;
                Random r = new Random();
                Pickup p = Pickup.SpawnPickup(r.Next(),"Pickup",this.Position,5f);
                Debug.WriteLine(p.Hitbox);
                collidables.Add(p);
                Remove = true;
                
            }
        }


    }
}
