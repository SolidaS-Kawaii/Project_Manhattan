using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manhattan
{
    public abstract class Player_skill
    {
        public int Hp;
        public int MaxHp;
        public int Str;
        public int MaxStr;
        public int Mood;

        public Player_skill(int hp, int str)
        {
            Hp = hp;
            MaxHp = hp;
            Str = str;
            MaxStr = str;
        }
        public abstract void skill1();
        public abstract void skill2();

    }
}
