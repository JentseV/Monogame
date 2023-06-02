﻿using GameProject.Enemies;
using GameProject.GameObjects.Characters.Player;
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

namespace GameProject.Projectiles
{
    internal class Bullet : Projectile , IGameComponent, ICollidable
    {

        private float rotation = 0;
        private float timeToLive = 5f, created = 0f;
        public bool destroy = false;

      


        public Bullet(int ID,string tag,Vector2 position, Vector2 direction, Vector2 speed, Texture2D texture,float damage) 
        {
            this.destroy = false;
            this.ID = ID;
            this.Tag = tag;
            this.Position = position;
            this.Direction = direction;
            this.Speed = speed;
            this.Texture = texture;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 5, 5);
            this.Damage = damage;
        }


        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, rotation, new Vector2(0f, 0f), 2.5f, SpriteEffects.None, 0f);
        }


        public void Update(GameTime gameTime, List<ICollidable> collidables)
        {
            
            
            if (destroy == false)
            {
                Move();
                this.hitbox.X = (int)Position.X;
                this.hitbox.Y = (int)Position.Y;
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                created += deltaTime;
                if (created >= timeToLive)
                {
                    destroy = true;
                }
                Rotate();
                CheckCollision(collidables);
            }
            
            base.Update(gameTime);
        }

        public bool Destroy()
        {
            return destroy;
        }

        private void Rotate()
        {
            Point point = new Point((int)Math.Round(Direction.X), (int)Math.Round(Direction.Y));
            Vector2 direction2 = new Vector2(point.X, point.Y);
            switch (direction2)
            {
                case Vector2(0f, -1f):
                    {
                        rotation = MathHelper.ToRadians(90f);
                        break;
                    }
                case Vector2(0f, 1f):
                    {
                        rotation = MathHelper.ToRadians(270f);
                        break;
                    }
                case Vector2(1f, -1f):
                    {
                        rotation = MathHelper.ToRadians(135f);
                        break;
                    }
                case Vector2(1f, 1f):
                    {
                        rotation = MathHelper.ToRadians(225f);
                        break;
                    }
                case Vector2(-1f, 1f):
                    {
                        rotation = MathHelper.ToRadians(315f);
                        break;
                    }
                case Vector2(-1f, -1f):
                    {
                        rotation = MathHelper.ToRadians(-315f);
                        break;
                    }
                case Vector2(1f, 0f):
                    {
                        rotation = MathHelper.ToRadians(180f);
                        break;
                    }

                default:
                    {
                        rotation = 0f;
                        break;
                    }
            }
        }

        public void CheckCollision(List<ICollidable> collidables)
        {
            foreach (ICollidable collidable in collidables)
            {
                if (hitbox.Intersects(collidable.Hitbox) && !(collidable is Bullet) && !(collidable is Pickup))
                {
                    if (this.Tag == "BulletHero")
                    {
                        if (collidable is Coffin)
                        {
                            Coffin c = collidable as Coffin;
                            if (!c.Invincible) destroy = true;
                        }

                        if (collidable is Cactus)
                        {
                            Cactus c = collidable as Cactus;
                            if (!c.Invincible) destroy = true;
                        }

                        if (collidable is Coyote)
                        {
                            Coyote c = collidable as Coyote;
                            if(!c.Invincible) destroy = true;
                        }
                    }
                    if(this.Tag == "CactusBullet")
                    {
                        if(collidable is Hero)
                        {
                            
                            Hero h = collidable as Hero;
                            if (!h.Invincible)
                            {
                                
                                destroy = true;
                            }
                        }
                    }
                    
                }

            }
        }
    }
}