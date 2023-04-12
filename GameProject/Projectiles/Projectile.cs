using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Projectiles
{
    internal abstract class Projectile : IMovable , IGameComponent
    {
        private string tag;

        private int id;
        private float damage;

        public float Damage { get { return damage; } set { damage = value; } }
        public int ID { get { return id; } set { id = value; } }
        public string Tag { get { return tag; } set { tag = value; } }

        private Texture2D _texture;

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        private Vector2 direction, position, speed;
        public Vector2 Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public IInputReader InputReader { get; set; }

        protected Rectangle hitbox;
        public Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void Move()
        {
            this.position += Direction * Speed;
        }

        public void Update(GameTime gameTime)
        {
            Move();
        }
    }
}
