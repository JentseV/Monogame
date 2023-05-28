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

        public Coffin(Vector2 speed, Vector2 position, Texture2D[] textures)
        {
            this.Hitpoints = 3;
            Range = 32f;
            Movable = true;
            //SPRITESHEET 74x55
            this.Speed = speed;
            this.Position = position;

            this.Center = new Vector2(50 + Position.X, 55 + Position.Y);
            this.Hitbox = new Rectangle((int)Center.X, (int)Center.Y, 45, 45);
            this.TextureIdle = textures[1];
            this.TextureRunRight = textures[1];
            this.TextureUpRun = textures[2];
            this.TextureIdleFacingUp = textures[3];
            this.HitboxText = textures[9];
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

            AnimationIdle = new Animation();
            AnimationRun = new Animation();
            AnimationAttacking = new Animation();
            AnimationHit = new Animation();

            AnimationHit.GetFramesFromTextureProperties(this.TextureHit.Width, this.TextureHit.Height, 1, 1);
            AnimationIdle.GetFramesFromTextureProperties(this.TextureIdle.Width, this.TextureIdle.Height, 6, 1);
            AnimationRun.GetFramesFromTextureProperties(this.TextureRunRight.Width, this.TextureRunRight.Height, 14, 1);
            AnimationAttacking.GetFramesFromTextureProperties(this.TextureAttacking.Width, this.TextureAttacking.Height, 18, 1);
        }


        public new  void Update(GameTime gameTime,Hero hero, List<ICollidable> collidables)
        {
            base.Update(gameTime,hero,collidables);
            if (Attacking == true)
            {
                Attack();
            }
        }

        private void Attack()
        {
            
            if (TimeSinceLastAttack <= 0 && Invincible == false && Attacking == true)
            {
                
                TimeSinceLastAttack = AttackCooldown;
                this.hitbox.Width = 55;
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
                                TakeDamage(b.Damage);
                        }   
                        
                    }
                    
                    
                }
                
            }
            
        }

        
    }
}
