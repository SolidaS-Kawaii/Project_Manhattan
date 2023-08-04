using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

////////////////////////////////// เป็นหน้าเอาไว้สร้างสเตตัสพื้นฐานของ ตัวละคร /////////////////////////////////////

namespace Project_Manhattan
{
    public abstract class Player_skill
    {
        public int Hp;
        public int MaxHp;
        public int Str;
        public int MaxStr;
        public int Mood;

        public int Skill1_Cost;
        public int Skill2_Cost;

        public bool IsCharEnd = false;
        public Player_skill(int hp, int str, int skill1_Cost, int skill2_Cost)
        {
            Hp = hp;
            MaxHp = hp;
            Str = str;
            MaxStr = str;
            Skill1_Cost = skill1_Cost;
            Skill2_Cost = skill2_Cost;
        }
        public abstract void skill1();
        public abstract void skill2();
        public abstract void skill1_Info();
        public abstract void skill2_Info();
    }

    public abstract class Enemy_skill
    {
        public int Hp;
        public int MaxHp;
        public int Str;
        public int MaxStr;
        public bool IsCharEnd = false;
        public Enemy_skill(int hp, int str)
        {
            Hp = hp;
            MaxHp = hp;
            Str = str;
            MaxStr = str;
        }
        public abstract void skill1();
        public abstract void skill2();
        public abstract void skill1_Info();
        public abstract void skill2_Info();
    }
}
