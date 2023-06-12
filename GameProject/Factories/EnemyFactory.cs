using GameProject.Enemies;
using GameProject.GameObjects.Characters.Player;
using GameProject.GameObjects.Dynamic.DynamicCollidables.Characters.Enemies.Cactus;
using GameProject.GameObjects.Dynamic.DynamicCollidables.Characters.Enemies.Coffin;
using GameProject.GameObjects.Dynamic.DynamicCollidables.Characters.Enemies.Coyote;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Factories
{
    internal static class EnemyFactory
    {
        private static List<Vector2> spawnLocations = new List<Vector2>();

        public static List<Enemy> SpawnEnemies(Texture2D[] coffinTextures, Texture2D[] cactusTextures, Texture2D[] coyoteTextures, float difficulty,Hero hero)
        {

            List<Enemy> enemies = new List<Enemy>();

            spawnLocations.Add(new Vector2(114F, 280f));
            spawnLocations.Add(new Vector2(1082f, 450f));
            spawnLocations.Add(new Vector2(537f, 288f));
            spawnLocations.Add(new Vector2(188f, 257f));

            float enemiesToSpawn = difficulty * 1f;
            while (enemies.Count < enemiesToSpawn)
            {

                Random r = new Random();
                short spawnerChance = (short)r.Next(0, 100);

                if (spawnerChance > 0 && spawnerChance < 50)
                {
                    enemies.Add(new Coffin(new Vector2(1f, 1f), spawnLocations[r.Next(0, 4)], coffinTextures,hero));
                }
                else if (spawnerChance > 50 && spawnerChance < 80)
                {
                    enemies.Add(new Cactus(new Vector2(1.5f, 1.5f), spawnLocations[r.Next(0, 4)], cactusTextures, hero));
                }
                else
                {
                    enemies.Add(new Coyote(new Vector2(1f, 1f), spawnLocations[r.Next(0, 4)], coyoteTextures, hero));
                }
            }
            return enemies;
        }
    }

}
