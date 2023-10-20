using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Project_Manhattan.Content;
using Project_Manhattan.CoreCode;

namespace Project_Manhattan
{
    public class Doktah : Enemy
    {
        public Doktah(MainGame game) : base(game)
        {
            Hp = 1000;
            Str = 115;

            MaxHp = Hp;
            MaxStr = Str;

            This_Ani = new AnimatedTexture(Vector2.Zero, 0, 0.6f, 0);
            This_Ani.Load(game.Content, "2D/Enemy/bjj idle-400x80", 5, 1, 8);
        }
        public override void skill1(int RandPos)
        {
            LFC.PAS[0].Hp -= (this.Str - (LFC.PAS[0].Def/2));
            LFC.PAS[1].Hp -= (this.Str - (LFC.PAS[1].Def / 2));
            LFC.PAS[2].Hp -= (this.Str - (LFC.PAS[2].Def / 2));

            LFC.PAS[0].IsHurt = true;
            LFC.PAS[1].IsHurt = true;
            LFC.PAS[2].IsHurt = true;
        }
        public override void skill2(int RandPos)
        {

        }
    }
    public class MuscleRat : Enemy
    {
        public MuscleRat(MainGame game) : base(game)
        {
            Hp = 500;
            Str = 50;

            MaxHp = Hp;
            MaxStr = Str;

            This_Ani = new AnimatedTexture(Vector2.Zero, 0, 0.5f, 0);
            This_Ani.Load(game.Content, "2D/Enemy/Keke_idle", 2, 1, 4);
        }
        public override void skill1(int RandPos)
        {
            LFC.PAS[RandPos].Hp -= (this.Str - (LFC.PAS[RandPos].Def / 2));

            LFC.PAS[RandPos].IsHurt = true;
        }
        public override void skill2(int RandPos)
        {

        }
    }
}
