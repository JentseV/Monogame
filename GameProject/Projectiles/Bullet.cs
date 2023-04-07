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
        public Bullet(int ID,string tag,Vector2 position, Vector2 direction, Vector2 speed, Texture2D texture)
        {
            this.ID = ID;
            this.Tag = tag;
            this.Position = position;
            this.Direction = direction;
            this.Speed = speed;
            this.Texture = texture;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 5, 5);
        }


        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, rotation, new Vector2(0f, 0f), 2f, SpriteEffects.None, 0f);
        }

        public new void Move()
        {
            base.Move();
        }

        public new void Update(GameTime gameTime)
        {
            this.hitbox.X = (int)Position.X;
            this.hitbox.Y = (int)Position.Y;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            created += deltaTime;
            if(created >= timeToLive)
            {
                destroy = true;
            }
            Rotate();
            base.Update(gameTime);
        }

        public bool Destroy()
        {
            return destroy;
        }

        private void Rotate()
        {
            
            switch (Direction)
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
    }
}
