using Microsoft.Xna.Framework;
using Project_Manhattan.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manhattan.CoreCode
{
    public class LEC
    {
        public static Enemy[] enemies = new Enemy[3];

        public LEC(MainGame game)
        {
            enemies[0] = new MuscleRat(game);
            enemies[1] = new Doktah(game);
            enemies[2] = new MuscleRat(game);
        }
        public void UpdateList(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].UpdateAction();
                if (enemies[i].Anime == "S1")
                {
                    enemies[i].This_Ani[2].UpdateFrame(elapsed);
                }
                else if (enemies[i].Anime == "S2")
                {
                    enemies[i].This_Ani[3].UpdateFrame(elapsed);
                }
                else if (enemies[i].Anime == "S3")
                {
                    enemies[i].This_Ani[5].UpdateFrame(elapsed);
                }
                else if (enemies[i].Anime == "Spawn")
                {
                    enemies[i].This_Ani[1].UpdateFrame(elapsed);
                }
                else if (enemies[i].Anime == "Hurt")
                {
                    enemies[i].This_Ani[4].UpdateFrame(elapsed);
                }

                if (enemies[i].Particle == "Buff")
                {
                    enemies[i].This_Part[0].UpdateFrame(elapsed);
                }
                else if(enemies[i].Particle == "Heal")
                {
                    enemies[i].This_Part[1].UpdateFrame(elapsed);
                }
                else if (enemies[i].Particle == "Debuff")
                {
                    enemies[i].This_Part[2].UpdateFrame(elapsed);
                }
                else if (enemies[i].Particle == "Def")
                {
                    enemies[i].This_Part[3].UpdateFrame(elapsed);
                }
                enemies[i].This_Ani[0].UpdateFrame(elapsed);
            }
        }
    }
}
