using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

////////////////////////////////// เป็นหน้าเอาไว้สร้างสเตตัสพื้นฐานของ ตัวละคร /////////////////////////////////////

namespace Project_Manhattan.Content
{
    public abstract class Player_skill
    {
        public int Hp;
        public int MaxHp;
        public int Str;
        public int MaxStr;
        public int Def;
        public int MaxDef;

        public int Skill1_Cost;
        public int Skill2_Cost;

        public string Target_1;   //Self, Friend, Enemy
        public string Target_2;   //Self, Friend, Enemy

        public bool IsCharEnd = false;
        public bool IsAlive = true;
        public Player_skill()
        {

        }
        public abstract void skill1(int enePos, int CastPos);
        public abstract void skill2(int enePos, int CastPos);
        public abstract void skill1_Info();
        public abstract void skill2_Info();
    }

    public abstract class Enemy_skill
    {
        public int Hp;
        public int MaxHp;
        public int Str;
        public int MaxStr;
        public bool Visible { get; set; }
        public Enemy_skill(int hp, int str)
        {
            Hp = hp;
            MaxHp = hp;
            Str = str;
            MaxStr = str;
        }
        public abstract void skill1(int RandPos);
        public abstract void skill2(int RandPos);
        public abstract void skill1_Info();
        public abstract void skill2_Info();
    }
}
