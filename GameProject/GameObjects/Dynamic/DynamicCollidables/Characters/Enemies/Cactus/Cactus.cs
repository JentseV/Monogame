using GameProject.Animations;
using GameProject.Enemies;
using GameProject.GameObjects.Characters.Player;
using GameProject.GameObjects.Dynamic.DynamicCollidables;
using GameProject.Interfaces;
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
    internal class Cactus : Enemy, IEnemy, IRangedAttacker, IAttack
    {

        private Texture2D BulletTexture;

        private List<Bullet> bullets = new List<Bullet>();
        public List<Bullet> Bullets { get { return bullets; } set { bullets = value; } }

        public Cactus(Vector2 speed, Vector2 position, Texture2D[] textures) : base(speed,position,textures)
        {
            Movable = true;
            Damage = 1f;
            Hitpoints = 2;

            Range = 200f;
            Center = new Vector2(50 + Position.X, 55 + Position.Y);
            Hitbox = new Rectangle((int)Center.X + 5, (int)Center.Y, 30, 40);
            
            
            BulletTexture = textures[11];

            AttackCooldown = 0.5f;

            TimeSinceLastAttack = AttackCooldown;
            AnimationHit.GetFramesFromTextureProperties(TextureHit.Width, TextureHit.Height, 1, 1);
            AnimationIdle.GetFramesFromTextureProperties(TextureIdle.Width, TextureIdle.Height, 4, 1);
            AnimationRun.GetFramesFromTextureProperties(TextureRunRight.Width, TextureRunRight.Height, 10, 1);
            AnimationAttacking.GetFramesFromTextureProperties(TextureAttacking.Width, TextureAttacking.Height, 11, 1);
        }
       
        public override void Attack()
        {

            if (TimeSinceLastAttack <= 0 && Invincible == false)
            {
                Attacking = true;
                Bullet b = new Bullet(Bullets.Count, "CactusBullet", new Vector2(Center.X + 2f, Center.Y + 12f), Direction, new Vector2(1.5f, 1.5f), BulletTexture, Damage);
                Bullets.Add(b);

                TimeSinceLastAttack = AttackCooldown;
            }
        }


        public void UpdateBullets(GameTime gameTime, List<ICollidable> collidables)
        {
            if (this.Remove)
            {
                foreach (Bullet b in Bullets)
                {
                    collidables.Remove(b);
                }
                Bullets.Clear();
            }
            else
            {
                for (int i = Bullets.Count - 1; i >= 0; i--)
                {
                    Bullet b = Bullets[i];
                    if (!b.Remove)
                    {
                        b.Update(gameTime);
                        if (!collidables.Contains(b))
                            collidables.Add(b);
                    }
                    else
                    {
                        Bullets.RemoveAt(i);
                        collidables.Remove(b);
                    }
                }
            }

        }

        public void DrawBullets(SpriteBatch spriteBatch)
        {
            foreach (Bullet b in Bullets)
            {
                if (!b.Remove) b.Draw(spriteBatch);
            }
        }

        
    }
}
