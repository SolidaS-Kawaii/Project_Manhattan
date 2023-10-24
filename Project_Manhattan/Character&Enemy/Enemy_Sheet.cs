using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Manhattan.Content;
using Project_Manhattan.CoreCode;
using SharpDX.Direct3D9;

namespace Project_Manhattan
{
    public class Doktah : Enemy
    {
        public Doktah(MainGame game) : base(game)
        {
            Hp = 1000;
            Str = 115;
            Def = 100;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            for (int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);
            }

            This_Ani[0].Load(game.Content, "2D/Enemy/BJJ/BJJ_Idle", 5, 1, 8);
            This_Ani[1].Load(game.Content, "2D/Enemy/BJJ/BJJ_Spawn", 10, 1, frameSp);
            This_Ani[2].Load(game.Content, "2D/Enemy/BJJ/BJJ_Skill1", 10, 1, 4);
            This_Ani[3].Load(game.Content, "2D/Enemy/BJJ/BJJ_Skill2", 10, 1, 4);
            This_Ani[4].Load(game.Content, "2D/Enemy/BJJ/BJJ_Hurt", 4, 1, frameHr);
        }
        public override void skill1(int RandPos)
        {
            rand = RandPos;
            Anime = "S1";
            this.IsAction = true;
        }
        public override void skill2(int RandPos)
        {
            rand = RandPos;
            Anime = "S2";
            this.IsAction = true;
        }
        public override void UpdateAction()
        {
            if (IsAction && Anime == "S1")
            {
                if (This_Ani[2].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    LFC.friend[0].Hp -= (this.Str * LFC.friend[0].DefRuduce(LFC.friend[0].Def));
                    LFC.friend[1].Hp -= (this.Str * LFC.friend[1].DefRuduce(LFC.friend[1].Def));
                    LFC.friend[2].Hp -= (this.Str * LFC.friend[2].DefRuduce(LFC.friend[2].Def));

                    LFC.friend[0].Anime = "Hurt";
                    LFC.friend[1].Anime = "Hurt";
                    LFC.friend[2].Anime = "Hurt";
                    This_Ani[2].Reset();
                }
            }
            else if (IsAction && Anime == "S2")
            {
                
            }
            else if (Anime == "Hurt")
            {
                if (This_Ani[4].IsEnd)
                {
                    Anime = "Idle";
                    This_Ani[4].Reset();
                }
            }
            else if (Anime == "Spawn")
            {
                if (This_Ani[1].IsEnd)
                {
                    Anime = "Idle";
                    This_Ani[1].Reset();
                }
            }

            if (Hp > MaxHp)
            {
                Hp = MaxHp;
            }
        }
        public override void UpdateDraw(SpriteBatch batch, Vector2 P)
        {
            if (IsAlive)
            {
                if (Anime == "S1")
                {
                    This_Ani[2].DrawFrame(batch, P, false);
                }
                else if (Anime == "S2")
                {
                    This_Ani[3].DrawFrame(batch, P, false);
                }
                else if (Anime == "Spawn")
                {
                    This_Ani[1].DrawFrame(batch, P, false);
                }
                else if (Anime == "Hurt")
                {
                    This_Ani[4].DrawFrame(batch, P, false);
                }
                else
                {
                    This_Ani[0].DrawFrame(batch, P, false);
                }
            }
        }
    }
    public class MuscleRat : Enemy
    {
        public MuscleRat(MainGame game) : base(game)
        {
            Hp = 500;
            Str = 50;
            Def = 0;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            for(int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 0.5f, 0);

            }
            This_Ani[0].Load(game.Content, "2D/Enemy/Keke_idle", 2, 1, 4);
            This_Ani[1].Load(game.Content, "2D/Enemy/Keke_idle", 2, 1, 4);
            This_Ani[2].Load(game.Content, "2D/Enemy/Keke_idle", 2, 1, 4);  
            This_Ani[3].Load(game.Content, "2D/Enemy/Keke_idle", 2, 1, 4);
            This_Ani[4].Load(game.Content, "2D/Enemy/Keke_idle", 2, 1, 4);
        }
        public override void skill1(int RandPos)
        {
            LFC.friend[RandPos].Hp -= (this.Str - (LFC.friend[RandPos].Def / 2));

            LFC.friend[RandPos].Anime = "Hurt";
        }
        public override void skill2(int RandPos)
        {

        }
        public override void UpdateAction()
        {

        }
        public override void UpdateDraw(SpriteBatch batch, Vector2 P)
        {
            if (IsAlive)
            {
                if (Anime == "S1")
                {
                    This_Ani[2].DrawFrame(batch, P, false);
                }
                else if (Anime == "S2")
                {
                    This_Ani[3].DrawFrame(batch, P, false);
                }
                else if (Anime == "Spawn")
                {
                    This_Ani[1].DrawFrame(batch, P, false);
                }
                else if (Anime == "Hurt")
                {
                    This_Ani[4].DrawFrame(batch, P, false);
                }
                else
                {
                    This_Ani[0].DrawFrame(batch, P, false);
                }
            }
        }
    }
}
