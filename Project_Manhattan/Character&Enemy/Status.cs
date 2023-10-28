using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Project_Manhattan.CoreCode;
using System;
using System.Collections.Generic;
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
        public string Particle = "Non";  //Non, Heal, Buff, Debuff, Def

        public bool IsCharEnd = false;
        public bool IsAlive = true;
        public bool IsAction = false;

        public string SkillInfo1 = "";
        public string SkillInfo2 = "";
        public string role = "";
        public string ability = "";

        public Vector2 AbsPos = new Vector2(0,0);
        public Color color = new Color();

        public SoundEffect sound_attack, sound_up;

        public AnimatedTexture[] This_Part = new AnimatedTexture[4];
        public AnimatedTexture[] This_Ani = new AnimatedTexture[5];

        public Friend(MainGame game)
        {
            for(int i = 0; i < This_Part.Length; i++)
            {
                This_Part[i] = new AnimatedTexture(Vector2.Zero, 0f, 1f, 0f);
            }

            This_Part[0].Load(game.Content, "2D/EFF/buff", 5, 1, 4);
            This_Part[1].Load(game.Content, "2D/EFF/heal", 5, 1, 4);
            This_Part[3].Load(game.Content, "2D/EFF/def", 5, 1, 4);
            This_Part[2].Load(game.Content, "2D/EFF/debuff", 5, 1, 4);

            sound_attack = game.Content.Load<SoundEffect>("Oth/SFX/Attack2");
            sound_up = game.Content.Load<SoundEffect>("Oth/SFX/Powerup");
            color = Color.White;
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

            IsAlive = true;
            Anime = "Spawn";
        }
        public float DefRuduce(float Def)
        {
            float DefRe;
            DefRe = 1 - (Def/(Def + 300));
            return DefRe;
        }
        public void CheckAction()
        {
            if (Def < 0)
            {
                Def = 0;
            }
            if (Hp > MaxHp)
            {
                Hp = MaxHp;
            }
            if (Hp < 0)
            {
                Hp = 0;
            }
            if (IsCharEnd)
            {
                color = Color.Gray;
            }
            else if (!IsCharEnd)
            {
                color = Color.White;
            }
        }
        public void ParticleEff()
        {
            if (Particle == "Buff")
            {
                if (This_Part[0].IsEnd)
                {
                    Particle = "Non";
                    This_Part[0].Reset();
                }
            }
            else if (Particle == "Heal")
            {
                if (This_Part[1].IsEnd)
                {
                    Particle = "Non";
                    This_Part[1].Reset();
                }
            }
            else if (Particle == "Debuff")
            {
                if (This_Part[2].IsEnd)
                {
                    Particle = "Non";
                    This_Part[2].Reset();
                }
            }
            else if (Particle == "Def")
            {
                if (This_Part[3].IsEnd)
                {
                    Particle = "Non";
                    This_Part[3].Reset();
                }
            }
        }
        public void ParticleDraw(SpriteBatch batch, Vector2 P)
        {
            if (Particle == "Buff")
            {
                This_Part[0].DrawFrame(batch, P);
            }
            else if (Particle == "Heal")
            {
                This_Part[1].DrawFrame(batch, P);
            }
            else if (Particle == "Debuff")
            {
                This_Part[2].DrawFrame(batch, P);
            }
            else if (Particle == "Def")
            {
                This_Part[3].DrawFrame(batch, P);
            }
        }
    }

    public abstract class Enemy
    {
        public float Hp;
        public float MaxHp;
        public float Str;
        public float MaxStr;
        public float StrReal;
        public float Def;
        public float MaxDef;
        public float DefReal;
        public float DefEx;
        public int rand;

        public int frameHr = 8;
        public int frameSp = 8;
        public int phase;
        public int phase2;
        public int phaseBegin;

        public string Anime = "Idle";    //Idle, S1, S2
        public string Particle = "Non";  //Non, Heal, Buff, Debuff, Def

        public bool IsAlive = true;
        public bool IsHurt = false;
        public bool IsAction = false;
        public bool IsEnd = false;

        public Vector2 AbsPos = new Vector2(0, 0);

        public SoundEffect sound_attack, sound_up;

        public AnimatedTexture[] This_Part = new AnimatedTexture[4];
        public AnimatedTexture[] This_Ani = new AnimatedTexture[6];
        public Enemy(MainGame game)
        {
            for (int i = 0; i < This_Part.Length; i++)
            {
                This_Part[i] = new AnimatedTexture(Vector2.Zero, 0f, 1.5f, 0f);
            }

            This_Part[0].Load(game.Content, "2D/EFF/buff", 5, 1, 4);
            This_Part[1].Load(game.Content, "2D/EFF/heal", 5, 1, 4);
            This_Part[3].Load(game.Content, "2D/EFF/def", 5, 1, 4);
            This_Part[2].Load(game.Content, "2D/EFF/debuff", 5, 1, 4);

            sound_attack = game.Content.Load<SoundEffect>("Oth/SFX/Attack2");
            sound_up = game.Content.Load<SoundEffect>("Oth/SFX/Powerup");
        }
        public abstract void skill(int RandPos);
        public abstract void UpdateAction();
        public abstract void UpdateDraw(SpriteBatch batch, Vector2 P);
        public void Resetto()
        {
            Hp = MaxHp;
            Def = MaxDef;
            Str = MaxStr;
            phase = phaseBegin;
            phase2 = phaseBegin;

            IsAlive = true;
            Anime = "Spawn";
        }
        public float DefRuduce(float Def)
        {
            float DefRe;
            DefRe = 1 - (Def / (Def + 300));
            return DefRe;
        }
        public void ParticleEff()
        {
            if (Particle == "Buff")
            {
                if (This_Part[0].IsEnd)
                {
                    Particle = "Non";
                    This_Part[0].Reset();
                }
            }
            else if (Particle == "Heal")
            {
                if (This_Part[1].IsEnd)
                {
                    Particle = "Non";
                    This_Part[1].Reset();
                }
            }
            else if (Particle == "Debuff")
            {
                if (This_Part[2].IsEnd)
                {
                    Particle = "Non";
                    This_Part[2].Reset();
                }
            }
            else if (Particle == "Def")
            {
                if (This_Part[3].IsEnd)
                {
                    Particle = "Non";
                    This_Part[3].Reset();
                }
            }
        }
        public void ParticleDraw(SpriteBatch batch, Vector2 P)
        {
            if (Particle == "Buff")
            {
                This_Part[0].DrawFrame(batch, P);
            }
            else if (Particle == "Heal")
            {
                This_Part[1].DrawFrame(batch, P);
            }
            else if (Particle == "Debuff")
            {
                This_Part[2].DrawFrame(batch, P);
            }
            else if (Particle == "Def")
            {
                This_Part[3].DrawFrame(batch, P);
            }
        }
    }
}
