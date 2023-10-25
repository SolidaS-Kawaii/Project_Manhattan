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
            Hp = 1500;
            Str = 200;
            Def = 25;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            phase = 0;
            phaseBegin = phase;

            for (int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);
            }

            This_Ani[0].Load(game.Content, "2D/Enemy/BJJ/BJJ_Idle", 5, 1, 8);
            This_Ani[1].Load(game.Content, "2D/Enemy/BJJ/BJJ_Spawn", 10, 1, frameSp);
            This_Ani[2].Load(game.Content, "2D/Enemy/BJJ/BJJ_Skill1", 10, 1, 4);
            This_Ani[3].Load(game.Content, "2D/Enemy/BJJ/BJJ_Skill2", 10, 1, 4);
            This_Ani[4].Load(game.Content, "2D/Enemy/BJJ/BJJ_Hurt", 4, 1, frameHr);
            This_Ani[5] = null;
        }
        public override void skill(int RandPos)
        {
            rand = RandPos;
            if(Hp <= MaxHp/2 && phase >= 2)
            {
                Anime = "S2";
                phase = 0;
            }
            else
            {
                Anime = "S1";
                phase++;
            }
            this.IsAction = true;
        }
        public override void UpdateAction()
        {
            if (LEC.enemies[0].IsAlive || LEC.enemies[2].IsAlive)
            {
                DefReal = Def + 175;
            }
            else
            {
                DefReal = Def;
            }

            if (IsAction && Anime == "S1")
            {
                if (This_Ani[2].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsEnd = true;
                    LFC.friend[rand].Hp -= (this.Str * LFC.friend[rand].DefRuduce(LFC.friend[rand].Def));
                    LFC.friend[rand].Anime = "Hurt";
                    This_Ani[2].Reset();
                }
            }
            else if (IsAction && Anime == "S2")
            {
                if (This_Ani[3].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    Hp += 300;
                    This_Ani[2].Reset();
                }
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
            if (Def < 0)
            {
                Def = 0;
            }
            if (Hp > MaxHp)
            {
                Hp = MaxHp;
            }

            if (Hp <= 0)
            {
                IsAlive = false;
            }
            else if (Hp > 0)
            {
                IsAlive = true;
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
            Def = 50;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            for(int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);

            }
            This_Ani[0].Load(game.Content, "2D/Enemy/Keke/Rat_Idle", 2, 1, 4);
            This_Ani[1].Load(game.Content, "2D/Enemy/Keke/Rat_Idle", 2, 1, 4);
            This_Ani[2].Load(game.Content, "2D/Enemy/Keke/Rat_Idle", 2, 1, 4);  
            This_Ani[3].Load(game.Content, "2D/Enemy/Keke/Rat_Idle", 2, 1, 4);
            This_Ani[4].Load(game.Content, "2D/Enemy/Keke/Rat_Hurt", 4, 1, frameHr);
            This_Ani[5] = null; 
        }
        public override void skill(int RandPos)
        {
            //LFC.friend[RandPos].Hp -= (this.Str - (LFC.friend[RandPos].Def / 2));

            //LFC.friend[RandPos].Anime = "Hurt";
        }
        public override void UpdateAction()
        {
            if (Anime == "Hurt")
            {
                if (This_Ani[4].IsEnd)
                {
                    Anime = "Idle";
                    This_Ani[4].Reset();
                }
            }
            if (Def < 0)
            {
                Def = 0;
            }
            if (Hp <= 0)
            {
                IsAlive = false;
            }
            else if (Hp > 0)
            {
                IsAlive = true;
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
    public class Shakya : Enemy
    {
        public Shakya(MainGame game) : base(game)
        {
            Hp = 2250;
            Str = 175;
            Def = 105;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            phase = 0;
            phaseBegin = phase;

            for(int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);

            }
            This_Ani[0].Load(game.Content, "2D/Enemy/Shakya/Shakya_Idle", 5, 1, 8);
            This_Ani[1].Load(game.Content, "2D/Enemy/Shakya/Shakya_Spawn", 8, 1, 8);
            This_Ani[2].Load(game.Content, "2D/Enemy/Shakya/Shakya_Skill2", 9, 1, 4);  
            This_Ani[3].Load(game.Content, "2D/Enemy/Shakya/Shakya_Skill1", 10, 1, 4);
            This_Ani[4].Load(game.Content, "2D/Enemy/Shakya/Shakya_Hurt", 4, 1, frameHr);
            This_Ani[5] = null;
        }
        public override void skill(int RandPos)
        {
            rand = RandPos;
            if (phase >= 3)
            {
                Anime = "S2";
                phase = 0;
            }
            else
            {
                Anime = "S1";
                phase++;
            }
            this.IsAction = true;
        }
        public override void UpdateAction()
        {
            DefReal = Def;
            if (LEC.enemies[0].IsAlive || LEC.enemies[2].IsAlive)
            {
                StrReal = Str + 75;
            }
            else
            {
                StrReal = Str;
            }

            if (IsAction && Anime == "S1")
            {
                if (This_Ani[2].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsEnd = true;
                    LFC.friend[rand].Hp -= (this.StrReal * LFC.friend[rand].DefRuduce(LFC.friend[rand].Def));
                    LFC.friend[rand].Anime = "Hurt";
                    This_Ani[2].Reset();
                }
            }
            else if (IsAction && Anime == "S2")
            {
                if (This_Ani[3].IsEnd)
                {
                    Anime = "Idle";
                    for(int i = 0; i < LFC.friend.Length; i++)
                    {
                        if (LFC.friend[i].IsAlive)
                        {
                            LFC.friend[i].Hp -= (this.StrReal * 1.25f *  LFC.friend[rand].DefRuduce(LFC.friend[rand].Def));
                            LFC.friend[i].Def -= 25;
                            LFC.friend[i].Anime = "Hurt";
                        }
                    }
                    Str += 25;
                    this.IsAction = false;
                    IsEnd = true;
                    This_Ani[3].Reset();
                }
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

            if (Def < 0)
            {
                Def = 0;
            }
            if (Hp > MaxHp)
            {
                Hp = MaxHp;
            }

            if (Hp <= 0)
            {
                IsAlive = false;
            }
            else if (Hp > 0)
            {
                IsAlive = true;
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
    public class Sensei : Enemy
    {
        public Sensei(MainGame game) : base(game)
        {
            Hp = 3000;
            Str = 75;
            Def = 50;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            phase = 3;
            phaseBegin = phase;

            This_Ani[0] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);
            This_Ani[1] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);
            This_Ani[2] = new AnimatedTexture(Vector2.Zero, 0, 2f, 0);
            This_Ani[3] = new AnimatedTexture(Vector2.Zero, 0, 2f, 0);
            This_Ani[4] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);
            This_Ani[5] = new AnimatedTexture(Vector2.Zero, 0, 2f, 0);

            This_Ani[0].Load(game.Content, "2D/Enemy/Sensei/Sensei_Idle", 4, 1, 4);
            This_Ani[1].Load(game.Content, "2D/Enemy/Sensei/Sensei_Spawn", 6, 1, 8);
            This_Ani[2].Load(game.Content, "2D/Enemy/Sensei/Sensei_Skill1", 14, 1, 4);
            This_Ani[3].Load(game.Content, "2D/Enemy/Sensei/Sensei_Skill2", 16, 1, 4);
            This_Ani[4].Load(game.Content, "2D/Enemy/Sensei/Sensei_Hurt", 4, 1, frameHr);
            This_Ani[5].Load(game.Content, "2D/Enemy/Sensei/Sensei_Skill3", 12, 1, frameHr);
        }
        public override void skill(int RandPos)
        {
            rand = RandPos;
            if (phase == 3 && Str <= MaxStr + 100)
            {
                Anime = "S1";
                phase++;
            }
            else if(phase >= 8 && !LEC.enemies[0].IsAlive && !LEC.enemies[2].IsAlive)
            {
                Anime = "S3";
                phase = 0;
            }
            else
            {
                Anime = "S2";
                phase++;
            }
            this.IsAction = true;
        }
        public override void UpdateAction()
        {
            DefReal = Def;
            if (LEC.enemies[0].IsAlive || LEC.enemies[2].IsAlive)
            {
                StrReal = Str + 50;
                DefReal = Def + 25;
            }
            else
            {
                StrReal = Str;
                DefReal = Def;
            }

            if (IsAction && Anime == "S1")
            {
                if (This_Ani[2].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsEnd = true;
                    Def += 20;
                    Str += 20;
                    This_Ani[2].Reset();
                }
            }
            else if (IsAction && Anime == "S2")
            {
                if (This_Ani[3].IsEnd)
                {
                    Anime = "Idle";
                    for (int i = 0; i < LFC.friend.Length; i++)
                    {
                        if (LFC.friend[i].IsAlive)
                        {
                            LFC.friend[i].Hp -= (this.StrReal * 1.5f * LFC.friend[rand].DefRuduce(LFC.friend[rand].Def));
                            LFC.friend[i].Anime = "Hurt";
                        }
                    }
                    this.IsAction = false;
                    IsEnd = true;
                    This_Ani[3].Reset();
                }
            }
            else if (IsAction && Anime == "S3")
            {
                if (This_Ani[5].IsEnd)
                {
                    Anime = "Idle";
                    for (int i = 0; i < LFC.friend.Length; i++)
                    {
                        if (LFC.friend[i].IsAlive)
                        {
                            LFC.friend[i].Def -= LFC.friend[i].Def/10;
                        }
                        LEC.enemies[0].Resetto();
                        LEC.enemies[2].Resetto();
                    }
                    this.IsAction = false;
                    IsEnd = true;
                    This_Ani[5].Reset();
                }
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
            if (Def < 0)
            {
                Def = 0;
            }
            if (Hp > MaxHp)
            {
                Hp = MaxHp;
            }

            if (Hp <= 0)
            {
                IsAlive = false;
            }
            else if (Hp > 0)
            {
                IsAlive = true;
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
                else if (Anime == "S3")
                {
                    This_Ani[5].DrawFrame(batch, P, false);
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
    public class Worm : Enemy
    {
        public Worm(MainGame game) : base(game)
        {
            Hp = 725;
            Str = 50;
            Def = 75;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            for (int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);

            }
            This_Ani[0].Load(game.Content, "2D/Enemy/Keke/ke02", 2, 1, 4);
            This_Ani[1].Load(game.Content, "2D/Enemy/Keke/ke02", 2, 1, 4);
            This_Ani[2].Load(game.Content, "2D/Enemy/Keke/ke02", 2, 1, 4);
            This_Ani[3].Load(game.Content, "2D/Enemy/Keke/ke02", 2, 1, 4);
            This_Ani[4].Load(game.Content, "2D/Enemy/Keke/ke02_Hurt", 4, 1, frameHr);
            This_Ani[5] = null;
        }
        public override void skill(int RandPos)
        {
            //LFC.friend[RandPos].Hp -= (this.Str - (LFC.friend[RandPos].Def / 2));

            //LFC.friend[RandPos].Anime = "Hurt";
        }
        public override void UpdateAction()
        {
            if (Anime == "Hurt")
            {
                if (This_Ani[4].IsEnd)
                {
                    Anime = "Idle";
                    This_Ani[4].Reset();
                }
            }
            if (Def < 0)
            {
                Def = 0;
            }
            if (Hp <= 0)
            {
                IsAlive = false;
            }
            else if (Hp > 0)
            {
                IsAlive = true;
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
    public class Bocchi : Enemy
    {
        public Bocchi(MainGame game) : base(game)
        {
            Hp = 375;
            Str = 50;
            Def = 50;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            for (int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);

            }
            This_Ani[0].Load(game.Content, "2D/Enemy/Keke/ke03_P", 4, 1, 4);
            This_Ani[1].Load(game.Content, "2D/Enemy/Keke/ke03_P", 4, 1, 4);
            This_Ani[2].Load(game.Content, "2D/Enemy/Keke/ke03_P", 4, 1, 4);
            This_Ani[3].Load(game.Content, "2D/Enemy/Keke/ke03_P", 4, 1, 4);
            This_Ani[4].Load(game.Content, "2D/Enemy/Keke/ke03_PHurt", 4, 1, frameHr);
            This_Ani[5] = null;
        }
        public override void skill(int RandPos)
        {
            //LFC.friend[RandPos].Hp -= (this.Str - (LFC.friend[RandPos].Def / 2));

            //LFC.friend[RandPos].Anime = "Hurt";
        }
        public override void UpdateAction()
        {
            if (Anime == "Hurt")
            {
                if (This_Ani[4].IsEnd)
                {
                    Anime = "Idle";
                    This_Ani[4].Reset();
                }
            }
            if (Def < 0)
            {
                Def = 0;
            }
            if (Hp <= 0)
            {
                IsAlive = false;
            }
            else if (Hp > 0)
            {
                IsAlive = true;
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
    public class Nijika : Enemy
    {
        public Nijika(MainGame game) : base(game)
        {
            Hp = 375;
            Str = 50;
            Def = 50;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            for (int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);

            }
            This_Ani[0].Load(game.Content, "2D/Enemy/Keke/ke03_Y", 4, 1, 4);
            This_Ani[1].Load(game.Content, "2D/Enemy/Keke/ke03_Y", 4, 1, 4);
            This_Ani[2].Load(game.Content, "2D/Enemy/Keke/ke03_Y", 4, 1, 4);
            This_Ani[3].Load(game.Content, "2D/Enemy/Keke/ke03_Y", 4, 1, 4);
            This_Ani[4].Load(game.Content, "2D/Enemy/Keke/ke03_YHurt", 4, 1, frameHr);
            This_Ani[5] = null;
        }
        public override void skill(int RandPos)
        {
            //LFC.friend[RandPos].Hp -= (this.Str - (LFC.friend[RandPos].Def / 2));

            //LFC.friend[RandPos].Anime = "Hurt";
        }
        public override void UpdateAction()
        {
            if (Anime == "Hurt")
            {
                if (This_Ani[4].IsEnd)
                {
                    Anime = "Idle";
                    This_Ani[4].Reset();
                }
            }
            if (Def < 0)
            {
                Def = 0;
            }
            if (Hp <= 0)
            {
                IsAlive = false;
            }
            else if (Hp > 0)
            {
                IsAlive = true;
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
}
