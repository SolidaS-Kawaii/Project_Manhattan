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

            enemies[0] = muscleRat;
            enemies[1] = sensei;
            enemies[2] = muscleRat1;
        }
    }
}
