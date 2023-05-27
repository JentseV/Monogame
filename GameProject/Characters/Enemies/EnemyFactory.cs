using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Enemies
{
    internal class EnemyFactory
    {
        List<Enemy> enemies = new List<Enemy>();
        List<Vector2> spawnLocations = new List<Vector2>();
        private Texture2D[] coffinTextures, cactusTextures, coyoteTextures;
        public EnemyFactory(Texture2D[] coffinTextures, Texture2D[] cactusTextures, Texture2D[] coyoteTextures)
        {
            this.coffinTextures = coffinTextures;
            this.cactusTextures = cactusTextures;
            this.coyoteTextures = coyoteTextures;

            spawnLocations.Add(new Vector2(200f, 200f));
            spawnLocations.Add(new Vector2(100f, 100f));
            spawnLocations.Add(new Vector2(600f, 600f));
            spawnLocations.Add(new Vector2(800f, 800f));
        }

        public void Update(GameTime gameTime, Hero hero, List<ICollidable> collidables)
        {
            foreach(Enemy enemy in enemies)
            {


                if (!enemy.Remove)
                {
                    if (enemy is Coffin)
                    {
                        Coffin e = enemy as Coffin;
                        e.Update(gameTime, hero, collidables);
                        if (!collidables.Contains(e)) collidables.Add(e);
                    }

                    if (enemy is Cactus)
                    {
                        Cactus e = enemy as Cactus;
                        e.Update(gameTime, hero, collidables);
                        if (!collidables.Contains(e)) collidables.Add(e);

                        foreach (Bullet b in e.cactusBullets)
                        {
                            if (b.destroy == false)
                            {
                                b.Update(gameTime, collidables);
                                if (collidables.Contains(b) == false) collidables.Add(b);
                            }
                        }
                    }

                    if (enemy is Coyote)
                    {
                        Coyote e = enemy as Coyote;
                        e.Update(gameTime, hero, collidables);
                        if (!collidables.Contains(e)) collidables.Add(e);
                    }
                }

            }

            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Enemy enemy in enemies)
            {
                if (!enemy.Remove)
                {
                    enemy.Draw(spriteBatch);

                    if (enemy is Cactus)
                    {
                        Cactus c = enemy as Cactus;
               
                        foreach (Bullet b in c.cactusBullets)
                        {
                            if(!b.destroy) b.Draw(spriteBatch);
                        }
                    }
                }
            }
        }

        public void SpawnEnemies(float amount)
        {
            if(enemies.Count < amount)
            {
                Random r = new Random();
                short spawnerChance = (short)r.Next(0, 100);

                if(spawnerChance > 0 && spawnerChance < 50)
                {
                    enemies.Add(new Coffin(new Vector2(1f,1f),spawnLocations[r.Next(0,4)],coffinTextures));
                }else if(spawnerChance > 50 && spawnerChance < 80)
                {
                    enemies.Add(new Cactus(new Vector2(1.5f, 1.5f),spawnLocations[r.Next(0, 4)],cactusTextures));
                }
                else
                {
                    enemies.Add(new Coyote(new Vector2(1f, 1f),spawnLocations[r.Next(0, 4)], coyoteTextures));
                }
            }

        }

        
    }
}
