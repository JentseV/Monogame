using GameProject.Animations;
using GameProject.Enemies;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    internal class Cactus : Enemy, IEnemy
    {

        private Texture2D BulletTexture;
        

        public List<Bullet> cactusBullets = new List<Bullet>();

        public Cactus(Vector2 speed, Vector2 position, Texture2D[] textures)
        {
            Movable = true;
            this.Damage = 1f;
            this.Hitpoints = 3;
            this.Speed = speed;
            this.Position = position;
            Range = 200f;
            this.Center = new Vector2(50 + Position.X, 55 + Position.Y);
            this.Hitbox = new Rectangle((int)Center.X+5, (int)Center.Y, 30, 40);
            this.TextureIdle = textures[0];
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
            AnimationIdle.GetFramesFromTextureProperties(this.TextureIdle.Width, this.TextureIdle.Height, 4, 1);
            AnimationRun.GetFramesFromTextureProperties(this.TextureRunRight.Width, this.TextureRunRight.Height, 10, 1);
            AnimationAttacking.GetFramesFromTextureProperties(this.TextureAttacking.Width, this.TextureAttacking.Height, 11, 1);
        }


        public new void Update(GameTime gameTime, Hero hero, List<ICollidable> collidables)
        {
            base.Update(gameTime, hero, collidables);
          
        }
        protected override void Attack()
        {
            
            if (TimeSinceLastAttack <= 0 && Invincible == false )
            {
                Attacking = true;
                Bullet b = new Bullet(cactusBullets.Count, "CactusBullet", new Vector2(Center.X + 2f, Center.Y + 12f), Direction, new Vector2(1.5f, 1.5f), BulletTexture,Damage);
                cactusBullets.Add(b);

                TimeSinceLastAttack = AttackCooldown;
            }
        }

        
    }
}
