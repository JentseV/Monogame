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
namespace GameProject.Pickups
{
    internal abstract class Pickup : ICollidable, IGameComponent
    {

        private Texture2D texture;
        private Texture2D healthTexture, coinTexture;
        private Rectangle hitbox;
        private bool despawn;
        private float timeTillDespawn;
        private Animation animation;
        private int id;
        private string tag;
        private Vector2 position;


        public Animation Animation { get { return animation; } set { animation = value; } }
        public Texture2D HealthTexture { get { return healthTexture; } set { healthTexture = value; } }
        public Texture2D CoinTexture { get { return coinTexture; } set { coinTexture = value; } }
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public float TimeTillDespawn { get { return timeTillDespawn; } set { timeTillDespawn = value; } }

        public bool Despawn { get { return despawn; } set { despawn = value; } }

        public Rectangle Hitbox { get { return hitbox; } set { hitbox = value; } }

        public int ID { get { return id; } set { id = value; } }
        public string Tag { get { return tag; } set { tag = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }

        public Pickup(int idIn, string tagIn, Vector2 postionIn,float timeTillDespawnIn)
        {
            ID = idIn;
            Tag = tagIn;
            Position = postionIn;
            Hitbox = new Rectangle((int)Position.X,(int)Position.Y,25,25);
            Despawn = false;
            TimeTillDespawn = timeTillDespawnIn;
            this.Animation = new Animation();
        }

        public void CheckCollision(List<ICollidable> collidables)
        {
            foreach(ICollidable collidable in collidables)
            {
                if(this.Hitbox.Intersects(collidable.Hitbox) && !(collidable is Pickup) && !(collidable is Bullet))
                {
                    if(collidable is Hero)
                    {
                        
                        Hero h = collidable as Hero;
                        OnPickup(h);
                    }
                }
            }
        }

        public void OnPickup(Hero h)
        {
            if(this is Coin)
            {
                h.GainGold(5f);
            }

            if(this is Health)
            {
                h.GainHealth(1f);
            }
            this.Despawn = true;
        }

        public static Pickup SpawnPickup(int id,string tag, Vector2 position,float timeDespawn)
        {
            
            Random r = new Random();
            int rn = r.Next(0, 11);
            {
                if(rn < 3)
                {
                    return new Health(id,tag,position,timeDespawn);
                }
                else
                {
                    return new Coin(id, tag, position, timeDespawn);
                }
            }
        }

        public void Update(GameTime gameTime,List<ICollidable> collidables)
        {
            this.animation.Update(gameTime);
            CheckCollision(collidables);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0f, 0f), 1f, SpriteEffects.None, 0f);

        }
    }
}
