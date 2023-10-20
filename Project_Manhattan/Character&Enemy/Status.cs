using Project_Manhattan.CoreCode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

////////////////////////////////// เป็นหน้าเอาไว้สร้างสเตตัสพื้นฐานของ ตัวละคร /////////////////////////////////////

namespace Project_Manhattan.Content
{
    public abstract class Friend
    {
        public int Hp;
        public int MaxHp;
        public int Str;
        public int MaxStr;
        public int Def;
        public int MaxDef;

        public string Name;

        public int Skill1_Cost;
        public int Skill2_Cost;

        public int target;
        public int cast;
        public string Target_1;   //Self, Friend, Enemy
        public string Target_2;   //Self, Friend, Enemy

        public string Anime = "Idle";    //Idle, S1, S2

        public bool IsCharEnd = false;
        public bool IsAlive = true;
        public bool IsHurt = false;
        public bool IsAction = false;

        public Friend()
        {

        }
        public abstract void skill1(int enePos, int CastPos);
        public abstract void skill2(int enePos, int CastPos);
        public abstract void skill1_Info();
        public abstract void skill2_Info();
        public abstract void UpdateAction();
    }

    public abstract class Enemy
    {
        public int Hp;
        public int MaxHp;
        public int Str;
        public int MaxStr;
        public bool IsAlive = true;
        public bool IsHurt = false;

        public AnimatedTexture This_Ani;
        public Enemy(MainGame game)
        {

        }
        public abstract void skill1(int RandPos);
        public abstract void skill2(int RandPos);
    }
}
