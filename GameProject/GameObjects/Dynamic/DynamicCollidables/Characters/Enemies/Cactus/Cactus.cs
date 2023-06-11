using GameProject.Animations;
using GameProject.Enemies;
using GameProject.GameObjects.Characters.Player;
using GameProject.GameObjects.Dynamic.Characters.Enemies;
using GameProject.GameObjects.Dynamic.DynamicCollidables;
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

namespace GameProject.GameObjects.Dynamic.DynamicCollidables.Characters.Enemies.Cactus
{
    internal class Cactus : Enemy, IEnemy, IRangedAttacker
    {

        private Texture2D BulletTexture;


        public List<Bullet> cactusBullets = new List<Bullet>();

        public Cactus(Vector2 speed, Vector2 position, Texture2D[] textures)
        {
            Movable = true;
            Damage = 1f;
            Hitpoints = 3;
            Speed = speed;
            Position = position;
            Range = 200f;
            Center = new Vector2(50 + Position.X, 55 + Position.Y);
            Hitbox = new Rectangle((int)Center.X + 5, (int)Center.Y, 30, 40);
            TextureIdle = textures[0];
            TextureRunRight = textures[1];
            TextureUpRun = textures[2];
            TextureIdleFacingUp = textures[3];
            HitboxText = textures[9];
            TextureIdleFacingRight = textures[4];
            TextureIdleFacingFront = textures[0];
            TextureDownRun = textures[5];
            TextureAttackRight = textures[7];
            TextureAttackUp = textures[8];
            TextureAttackFront = textures[6];
            TextureHit = textures[10];

            BulletTexture = textures[11];

            TextureIdling = TextureIdle;
            TextureRunning = TextureRunRight;
            TextureAttacking = TextureAttackFront;

            AttackCooldown = 0.5f;

            TimeSinceLastAttack = AttackCooldown;

            AnimationIdle = new Animation();
            AnimationRun = new Animation();
            AnimationAttacking = new Animation();
            AnimationHit = new Animation();

            AnimationHit.GetFramesFromTextureProperties(TextureHit.Width, TextureHit.Height, 1, 1);
            AnimationIdle.GetFramesFromTextureProperties(TextureIdle.Width, TextureIdle.Height, 4, 1);
            AnimationRun.GetFramesFromTextureProperties(TextureRunRight.Width, TextureRunRight.Height, 10, 1);
            AnimationAttacking.GetFramesFromTextureProperties(TextureAttacking.Width, TextureAttacking.Height, 11, 1);
        }


       
        protected override void Attack()
        {

            if (TimeSinceLastAttack <= 0 && Invincible == false)
            {
                Attacking = true;
                Bullet b = new Bullet(cactusBullets.Count, "CactusBullet", new Vector2(Center.X + 2f, Center.Y + 12f), Direction, new Vector2(1.5f, 1.5f), BulletTexture, Damage);
                cactusBullets.Add(b);

                TimeSinceLastAttack = AttackCooldown;
            }
        }


        public void UpdateBullets(GameTime gameTime, List<DynamicCollidable> collidables)
        {
            if (this.Remove)
            {
                foreach (Bullet b in cactusBullets)
                {
                    collidables.Remove(b);
                }
                cactusBullets.Clear();
            }
            else
            {
                for (int i = cactusBullets.Count - 1; i >= 0; i--)
                {
                    Bullet b = cactusBullets[i];
                    if (!b.Remove)
                    {
                        b.Update(gameTime);
                        if (!collidables.Contains(b))
                            collidables.Add(b);
                    }
                    else
                    {
                        cactusBullets.RemoveAt(i);
                        collidables.Remove(b);
                    }
                }
            }

        }

        public void DrawBullets(SpriteBatch spriteBatch)
        {
            foreach (Bullet b in cactusBullets)
            {
                if (!b.Remove) b.Draw(spriteBatch);
            }
        }

        
    }
}
