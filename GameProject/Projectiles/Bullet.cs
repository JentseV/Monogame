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
    internal class Bullet : Projectile , IGameComponent
    {
        public  int id;
        private float timeToLive = 5f, created = 0f;
        private bool destroy = false;
        public Bullet(int ID,Vector2 position, Vector2 direction, Vector2 speed, Texture2D texture)
        {
            this.id = ID;
            this.Position = position;
            this.Direction = direction;
            this.Speed = speed;
            this.Texture = texture;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 25, 25);
        }


        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(0f, 0f), 2f, SpriteEffects.None, 0f);
        }

        public new void Move()
        {
            base.Move();
        }

        public new void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            created += deltaTime;
            if(created >= timeToLive)
            {
                destroy = true;
            }
            base.Update(gameTime);
        }

        public bool Destroy()
        {
            return destroy;
        }
    }
}
