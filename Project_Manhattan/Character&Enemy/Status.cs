using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public float Hp;
        public float MaxHp;
        public float Str;
        public float MaxStr;
        public float Def;
        public float MaxDef;

        public int frameSk = 4;
        public int frameHr = 8;
        public int frameSp = 8;

        public string Name;

        public int Skill1_Cost;
        public int Skill2_Cost;

        public int target;
        public int cast;
        public string Target_1;   //Self, Friend, Enemy, AllEnemy, AllFriend
        public string Target_2;   //Self, Friend, Enemy, AllEnemy, AllFriend

        public string Anime = "Idle";    //Idle, S1, S2

        public bool IsCharEnd = false;
        public bool IsAlive = true;
        public bool IsHurt = false;
        public bool IsAction = false;

        public AnimatedTexture[] This_Ani = new AnimatedTexture[5];

        public Friend(MainGame game)
        {

        }
        public abstract void skill1(int enePos, int CastPos);
        public abstract void skill2(int enePos, int CastPos);
        public abstract void skill1_Info();
        public abstract void skill2_Info();
        public abstract void UpdateAction();
        public abstract void UpdateDraw(SpriteBatch batch, Vector2 P);
        public void Resetto()
        {
            Hp = MaxHp;
            Def = MaxDef;
            Str = MaxStr;

            Anime = "Spawn";
        }
        public float DefRuduce(float Def)
        {
            float DefRe;
            DefRe = 1 - (Def/(Def+100));
            return DefRe;
        }

    }

    public abstract class Enemy
    {
        public float Hp;
        public float MaxHp;
        public float Str;
        public float MaxStr;
        public float Def;
        public float MaxDef;
        public float rand;

        public int frameHr = 8;
        public int frameSp = 8;

        public string Anime = "Idle";    //Idle, S1, S2

        public bool IsAlive = true;
        public bool IsHurt = false;
        public bool IsAction = false;

        public AnimatedTexture[] This_Ani = new AnimatedTexture[5];
        public Enemy(MainGame game)
        {

        }
        public abstract void skill1(int RandPos);
        public abstract void skill2(int RandPos);
        public abstract void UpdateAction();
        public abstract void UpdateDraw(SpriteBatch batch, Vector2 P);
        public void Resetto()
        {
            Hp = MaxHp;
            Def = MaxDef;
            Str = MaxStr;

            Anime = "Spawn";
        }

        public float DefRuduce(float Def)
        {
            float DefRe;
            DefRe = 1 - (Def / (Def + 200));
            return DefRe;
        }
    }
}
