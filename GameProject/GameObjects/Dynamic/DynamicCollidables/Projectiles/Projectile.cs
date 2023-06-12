using GameProject.GameObjects;
using GameProject.GameObjects.Dynamic;
using GameProject.GameObjects.Dynamic.DynamicCollidables;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGameComponent = GameProject.Interfaces.IGameComponent;

namespace GameProject.Projectiles
{
    internal abstract class Projectile : DynamicCollidable, IMovable , IGameComponent , ICollidable
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

        public IInputReader InputReader { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void Move()
        {
            this.Position += Direction * Speed;
        }

        public override void Update(GameTime gameTime)
        {
            Move();
        }
    }
}
