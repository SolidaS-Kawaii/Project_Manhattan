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
            Doktah sensei = new Doktah(game);
            MuscleRat muscleRat = new MuscleRat(game);
            MuscleRat muscleRat1 = new MuscleRat(game);

            enemies[0] =  muscleRat;
            enemies[1] = sensei;
            enemies[2] = muscleRat1;
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
                else if (enemies[i].Anime == "Spawn")
                {
                    enemies[i].This_Ani[1].UpdateFrame(elapsed);
                }
                else if (enemies[i].Anime == "Hurt")
                {
                    enemies[i].This_Ani[4].UpdateFrame(elapsed);
                }
                enemies[i].This_Ani[0].UpdateFrame(elapsed);
            }
        }
    }
}
