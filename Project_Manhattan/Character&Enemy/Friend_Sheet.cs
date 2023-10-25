using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Manhattan.Content;
using Project_Manhattan.CoreCode;
using Project_Manhattan.Screen_Management;

/////////////////////////////////// เป็น Class เอาไว้ใส่สกิลตัวละครแต่ละตัว ///////////////////////////////////

namespace Project_Manhattan
{
    public class Mickey : Friend
    {
        int a = 0;
        public Mickey(MainGame game) : base(game)
        {
            Name = "Miki";

            Hp = 500;
            Str = 150;
            Def = 50;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            Skill1_Cost = 2;
            Skill2_Cost = 3;

            Target_1 = "Enemy";
            Target_2 = "Enemy"; 

            for (int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);
            }

            This_Ani[0].Load(game.Content, "2D/Friend/Mickey/Mig_idlet", 4, 1, frameSk);
            This_Ani[1].Load(game.Content, "2D/Friend/Mickey/Mig_spawn", 12, 1, frameSp);
            This_Ani[2].Load(game.Content, "2D/Friend/Mickey/Mig_skill1tt", 12, 1, frameSk);
            This_Ani[3].Load(game.Content, "2D/Friend/Mickey/Mig_skill2", 12, 1, frameSk);
            This_Ani[4].Load(game.Content, "2D/Friend/Mickey/Mig_hurt", 4, 1, frameHr);
            // สกิล1 เตะบอลใส่ศัตรูสร้างความเสียหาย100%atk
            // สกิล2 เตะบอลอัดใส่ศัตรูอย่างรุนแรงสร้างความเสียหาย 200 % atk
        }
        public override void skill1(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost;
            Anime = "S1";
            Console.WriteLine(Anime);
            Console.WriteLine(This_Ani[2].IsEnd);
            This_Ani[2].Reset();
            target = enePos;
            cast = CastPos;
            this.IsAction = true;
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost;
            Anime = "S2";
            Console.WriteLine(Anime);
            Console.WriteLine(This_Ani[3].IsEnd);
            This_Ani[3].Reset();
            target = enePos;
            cast = CastPos;
            this.IsAction = true;
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "MickeyE (2E)";
            Gameplay_Screen.SkillInfo = "Kick a ball into our sensei \nDeals 100% of Str";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "MickeyQ (3E)";
            Gameplay_Screen.SkillInfo = "Kick a ball into our sensei harshly \nDeals 200% of Str";
        }
        public override void UpdateAction() //for animated
        {
            if (IsAction && Anime == "S1")
            {
                if (This_Ani[2].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsCharEnd = true;
                    LEC.enemies[target].Hp -= Str * LEC.enemies[target].DefRuduce(LEC.enemies[target].DefReal);
                    LEC.enemies[target].Anime = "Hurt";
                }
            }
            else if(IsAction && Anime == "S2")
            {
                if (This_Ani[3].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsCharEnd = true;
                    LEC.enemies[target].Hp -= (Str * 2) * LEC.enemies[target].DefRuduce(LEC.enemies[target].DefReal);
                    LEC.enemies[target].Anime = "Hurt";
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

            if(Def < 0)
            {
                Def = 0;
            }
            if (Hp > MaxHp)
            {
                Hp = MaxHp;
            }
            if(Hp <= 0)
            {
                IsAlive = false;
            }
            else if(Hp > 0)
            {
                IsAlive = true;
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
        public override void UpdateDraw(SpriteBatch batch, Vector2 P)
        {
            if (IsAlive)
            {
                if (Anime == "S1")
                {
                    This_Ani[2].DrawFrame(batch, P, true);
                }
                else if (Anime == "S2")
                {
                    This_Ani[3].DrawFrame(batch, P, true);
                }
                else if (Anime == "Spawn")
                {
                    This_Ani[1].DrawFrame(batch, P, true);
                }
                else if (Anime == "Hurt")
                {
                    This_Ani[4].DrawFrame(batch, P, true);
                }
                else
                {
                    This_Ani[0].DrawFrame(batch, P, true, color);
                }
            }
        }
    }
    public class Heng : Friend
    {
        public Heng(MainGame game) : base(game)
        {
            Name = "Hengoku";

            Hp = 450;
            Str = 75;
            Def = 50;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            Skill1_Cost = 2;
            Skill2_Cost = 2;

            Target_1 = "AllFriend";
            Target_2 = "Friend";

            for (int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);
            }

            This_Ani[0].Load(game.Content, "2D/Friend/Heng/Heng_Idle", 4, 1, frameSk);
            This_Ani[1].Load(game.Content, "2D/Friend/Heng/Heng_Spawn", 12, 1, frameSp);
            This_Ani[2].Load(game.Content, "2D/Friend/Heng/Heng_Skill1", 12, 1, frameSk);
            This_Ani[3].Load(game.Content, "2D/Friend/Heng/Heng_Skill2", 12, 1, frameSk);
            This_Ani[4].Load(game.Content, "2D/Friend/Heng/Heng_Hurt", 4, 1, frameHr);

            //สกิล1 ใช้ยาจากกระเป่าคู่ใจฮีลเพื่อนร่วมทีม150หน่วย
            //สกิล2 ตะโกนปลุกใจเพื่อนร่วมทีมเพิ่มatk25หน่วย
        }
        public override void skill1(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost;
            Anime = "S1";
            target = enePos;
            cast = CastPos;
            this.IsAction = true;
            This_Ani[2].Reset();
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost;
            Anime = "S2";
            target = enePos;
            cast = CastPos;
            this.IsAction = true;
            This_Ani[3].Reset();
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "HengE (2E)";
            Gameplay_Screen.SkillInfo = "Pick up a medicine form my lover bag \nHeals 125 Hp to all friend";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "HengQ (2E)";
            Gameplay_Screen.SkillInfo = "Shouttttttttttttt! \nBuffs 75 Str to a friend";
        }
        public override void UpdateAction()
        {
            if (IsAction && Anime == "S1")
            {
                if (This_Ani[2].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsCharEnd = true;
                    for (int i = 0; i < 3; i++)
                    {
                        if (LFC.friend[i].IsAlive)
                        {
                            LFC.friend[i].Hp += 125;
                        }
                    }
                }
            }
            else if (IsAction && Anime == "S2")
            {
                if (This_Ani[3].IsEnd)
                {
                    Anime = "Idle";
                    IsAction = false;
                    IsCharEnd = true;
                    LFC.friend[target].Str += 75;                   
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
            if (IsCharEnd)
            {
                color = Color.Gray;
            }
            else if (!IsCharEnd)
            {
                color = Color.White;
            }
        }
        public override void UpdateDraw(SpriteBatch batch, Vector2 P)
        {
            if (IsAlive)
            {              
                if (Anime == "S1")
                {
                    This_Ani[2].DrawFrame(batch, P, true);
                }
                else if (Anime == "Spawn")
                {
                    This_Ani[1].DrawFrame(batch, P, true);
                }
                else if (Anime == "Hurt")
                {
                    This_Ani[4].DrawFrame(batch, P, true);
                }
                else if (Anime == "S2")
                {
                    This_Ani[3].DrawFrame(batch, P, true);
                }
                else
                {
                    This_Ani[0].DrawFrame(batch, P, true, color);
                }
            }
        }
    }
    public class Ohm : Friend
    {
        public Ohm(MainGame game) : base(game)
        {
            Name = "Ohmo";

            Hp = 800;
            Str = 75;
            Def = 125;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            Skill1_Cost = 1;
            Skill2_Cost = 3;

            Target_1 = "Self";
            Target_2 = "Enemy";

            for (int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);
            }

            This_Ani[0].Load(game.Content, "2D/Friend/Ohm/Ohm_Idle", 4, 1, frameSk);
            This_Ani[1].Load(game.Content, "2D/Friend/Ohm/Ohm_Spawn", 12, 1, frameSp);
            This_Ani[2].Load(game.Content, "2D/Friend/Ohm/Ohm_Skill1", 16, 1, frameSk);
            This_Ani[3].Load(game.Content, "2D/Friend/Ohm/Ohm_Skill2", 10, 1, frameSk);
            This_Ani[4].Load(game.Content, "2D/Friend/Ohm/Ohm_Hurt", 4, 1, frameHr);
            //สกิล1 ยกดัมเบลเพื่อสร้างความแข็งแรงให้กล้ามเนื้อDef+50
            //สกิล2 เบ่งกล้ามอวดศัตรูเพื่อยัวยุศัตรูให้มาตีพร้อมทั้งได้รับค่าdefเพิ่มขึ้น100
        }
        public override void skill1(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost;
            Anime = "S1";
            This_Ani[2].Reset();
            target = enePos;
            cast = CastPos;
            this.IsAction = true;
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost;
            Anime = "S2";
            This_Ani[3].Reset();
            target = enePos;
            cast = CastPos;
            this.IsAction = true;
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "OhmE (1E)";
            Gameplay_Screen.SkillInfo = "I can lift a dumbbell all day \nDef up 50";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "OhmQ (3E)";
            Gameplay_Screen.SkillInfo = "Want some larb, brother? \nDeal Dmg with 125%";
        }
        public override void UpdateAction()
        {
            if (IsAction && Anime == "S1")
            {
                if (This_Ani[2].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsCharEnd = true;
                    LFC.friend[cast].Def += 50;
                }
            }
            else if (IsAction && Anime == "S2")
            {
                if (This_Ani[3].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsCharEnd = true;
                    LEC.enemies[target].Hp -= Def * 1.25f * LEC.enemies[target].DefRuduce(LEC.enemies[target].DefReal);
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
            if (IsCharEnd)
            {
                color = Color.Gray;
            }
            else if (!IsCharEnd)
            {
                color = Color.White;
            }
        }
        public override void UpdateDraw(SpriteBatch batch, Vector2 P)
        {
            if (IsAlive)
            {
                if (Anime == "S1")
                {
                    This_Ani[2].DrawFrame(batch, P, true);
                }
                else if (Anime == "S2")
                {
                    This_Ani[3].DrawFrame(batch, P, true);
                }
                else if (Anime == "Spawn")
                {
                    This_Ani[1].DrawFrame(batch, P, true);
                }
                else if (Anime == "Hurt")
                {
                    This_Ani[4].DrawFrame(batch, P, true);
                }
                else
                {
                    This_Ani[0].DrawFrame(batch, P, true, color);
                }
            }
        }
    }
    public class Dome : Friend
    {
        public Dome(MainGame game) : base(game)
        {
            Name = "Domino";
            Hp = 450;
            Str = 175;
            Def = 25;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            Skill1_Cost = 1;
            Skill2_Cost = 3;

            Target_1 = "Enemy";
            Target_2 = "Enemy";

            for (int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);
            }

            This_Ani[0].Load(game.Content, "2D/Friend/Dome/Dome_Idle", 4, 1, frameSk);
            This_Ani[1].Load(game.Content, "2D/Friend/Dome/Dome_Spawn", 12, 1, frameSp);
            This_Ani[2].Load(game.Content, "2D/Friend/Dome/Dome_Skill1", 10, 1, frameSk);
            This_Ani[3].Load(game.Content, "2D/Friend/Dome/Dome_Skill2", 12, 1, frameSk);
            This_Ani[4].Load(game.Content, "2D/Friend/Dome/Dome_Hurt", 4, 1, frameHr);
            //สกิล1 ใช้เหม่งสะท้อนใส่ศัตรูสร้างความเสียหาย75% atk
            //สกิล2 ใช้ไสยเวยต้องห้ามโจมตีศัตรู200 % atkพร้อมทั้งได้รับผลกระทบโดยของเข้าตัวลดhp25หน่วย
        }
        public override void skill1(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost;
            Anime = "S1";
            This_Ani[2].Reset();
            target = enePos;
            cast = CastPos;
            this.IsAction = true;
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost;
            Anime = "S2";
            This_Ani[3].Reset();
            target = enePos;
            cast = CastPos;
            this.IsAction = true;
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "DomeE (1E)";
            Gameplay_Screen.SkillInfo = "Reflect DMG into our sensei \nDeals 75% of Str";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "DomeQ (3E)";
            Gameplay_Screen.SkillInfo = "Reverse curse technique \nDeals 200% of Str and Reserve DMG 25";
        }
        public override void UpdateAction()
        {
            if (IsAction && Anime == "S1")
            {
                if (This_Ani[2].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsCharEnd = true;
                    LEC.enemies[target].Hp -= (Str * 75 / 100) * LEC.enemies[target].DefRuduce(LEC.enemies[target].DefReal);
                    LEC.enemies[target].Anime = "Hurt";
                }
            }
            else if (IsAction && Anime == "S2")
            {
                if (This_Ani[3].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsCharEnd = true;
                    LEC.enemies[target].Hp -= (Str * 2) * LEC.enemies[target].DefRuduce(LEC.enemies[target].DefReal);
                    Hp -= 25;
                    LEC.enemies[target].Anime = "Hurt";
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
            if (IsCharEnd)
            {
                color = Color.Gray;
            }
            else if (!IsCharEnd)
            {
                color = Color.White;
            }
        }
        public override void UpdateDraw(SpriteBatch batch, Vector2 P)
        {
            if (IsAlive)
            {
                if (Anime == "S1")
                {
                    This_Ani[2].DrawFrame(batch, P, true);
                }
                else if (Anime == "S2")
                {
                    This_Ani[3].DrawFrame(batch, P, true);
                }
                else if (Anime == "Spawn")
                {
                    This_Ani[1].DrawFrame(batch, P, true);
                }
                else if (Anime == "Hurt")
                {
                    This_Ani[4].DrawFrame(batch, P, true);
                }
                else
                {
                    This_Ani[0].DrawFrame(batch, P, true, color);
                }
            }
        }
    }
    public class JaBing : Friend
    {
        public JaBing(MainGame game) : base(game)
        {
            Name = "JajaBing";

            Hp = 300;
            Str = 200;
            Def = 0;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            Skill1_Cost = 4;
            Skill2_Cost = 4;

            Target_1 = "Enemy";
            Target_2 = "AllEnemy";

            AbsPos = new Vector2(40, 50);

            for (int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);
            }

            This_Ani[0].Load(game.Content, "2D/Friend/JJB/JJB_Idle", 2, 1, frameSk);
            This_Ani[1].Load(game.Content, "2D/Friend/JJB/JJB_Spawn", 12, 1, frameSp);
            This_Ani[2].Load(game.Content, "2D/Friend/JJB/JJB_Skill1", 11, 1, frameSk);
            This_Ani[3].Load(game.Content, "2D/Friend/JJB/JJB_Skill2", 12, 1, frameSk);
            This_Ani[4].Load(game.Content, "2D/Friend/JJB/JJB_Hurt", 4, 1, frameHr);
            //4E ตีปกติก็แรงละ 100ATK def+10ถาวร สูงสุด50
            //4E เรียกยานแม่มาตีอีกฝ่ายทั้งหมด50 % ATK
        }
        public override void skill1(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost;
            Anime = "S1";
            This_Ani[2].Reset();
            target = enePos;
            cast = CastPos;
            this.IsAction = true;
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost;
            Anime = "S2";
            This_Ani[3].Reset();
            target = enePos;
            cast = CastPos;
            this.IsAction = true;
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "JaJaBingE (4E)";
            Gameplay_Screen.SkillInfo = "Sawasdee Manud \nDeals 100% of Str \nDef up 10 but limits 50";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "JaJaBingQ (4E)";
            Gameplay_Screen.SkillInfo = "Attack all of plumbers \nDeals 75% of Str to all of sensei";
        }
        public override void UpdateAction() //for animated
        {
            if (IsAction && Anime == "S1")
            {
                if (This_Ani[2].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsCharEnd = true;
                    LEC.enemies[target].Hp -= Str * LEC.enemies[target].DefRuduce(LEC.enemies[target].Def);
                    if(Def < 50)
                    {
                        Def += 10;
                    }
                    LEC.enemies[target].Anime = "Hurt";
                }
            }
            else if (IsAction && Anime == "S2")
            {
                if (This_Ani[3].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsCharEnd = true;
                    LEC.enemies[0].Hp -= (Str * 3 / 4) * LEC.enemies[target].DefRuduce(LEC.enemies[target].DefReal);
                    LEC.enemies[1].Hp -= (Str * 3 / 4) * LEC.enemies[target].DefRuduce(LEC.enemies[target].DefReal);
                    LEC.enemies[2].Hp -= (Str * 3 / 4) * LEC.enemies[target].DefRuduce(LEC.enemies[target].DefReal);
                    LEC.enemies[0].Anime = "Hurt";
                    LEC.enemies[1].Anime = "Hurt";
                    LEC.enemies[2].Anime = "Hurt";
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
            if (IsCharEnd)
            {
                color = Color.Gray;
            }
            else if (!IsCharEnd)
            {
                color = Color.White;
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
                    This_Ani[4].DrawFrame(batch, P, true);
                }
                else
                {
                    This_Ani[0].DrawFrame(batch, P, false, color);
                }
            }
        }
    }
    public class Tata : Friend
    {
        public Tata(MainGame game) : base(game)
        {
            Name = "Tata";

            Hp = 700;
            Str = 0;
            Def = 50;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            Skill1_Cost = 2;
            Skill2_Cost = 3;

            Target_1 = "Enemy";
            Target_2 = "Friend";

            for (int i = 0; i < This_Ani.Length; i++)
            {
                This_Ani[i] = new AnimatedTexture(Vector2.Zero, 0, 1f, 0);
            }

            This_Ani[0].Load(game.Content, "2D/Friend/Tata/Tata_Idle", 4, 1, frameSk);
            This_Ani[1].Load(game.Content, "2D/Friend/Tata/Tata-Spawn", 6, 1, frameSp);
            This_Ani[2].Load(game.Content, "2D/Friend/Tata/Tata_Skill1", 8, 1, frameSk);
            This_Ani[3].Load(game.Content, "2D/Friend/Tata/Tata_Skill2", 9, 1, frameSk);
            This_Ani[4].Load(game.Content, "2D/Friend/Tata/Tata_Hurt", 4, 1, frameHr);
            //4E ตีปกติก็แรงละ 100ATK def+10ถาวร สูงสุด50
            //4E เรียกยานแม่มาตีอีกฝ่ายทั้งหมด50 % ATK
        }
        public override void skill1(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost;
            Anime = "S1";
            This_Ani[2].Reset();
            target = enePos;
            cast = CastPos;
            this.IsAction = true;
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost;
            Anime = "S2";
            This_Ani[3].Reset();
            target = enePos;
            cast = CastPos;
            this.IsAction = true;
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "TataE (2E)";
            Gameplay_Screen.SkillInfo = "Decrease a sensei's Def \nDef down 30";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "TataQ (3E)";
            Gameplay_Screen.SkillInfo = "Take a rest \nHeals 50% of a friend's MaxHp";
        }
        public override void UpdateAction() //for animated
        {
            if (IsAction && Anime == "S1")
            {
                if (This_Ani[2].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsCharEnd = true;
                    LEC.enemies[target].Def -= 30;
                }
            }
            else if (IsAction && Anime == "S2")
            {
                if (This_Ani[3].IsEnd)
                {
                    Anime = "Idle";
                    this.IsAction = false;
                    IsCharEnd = true;
                    LFC.friend[target].Hp += LFC.friend[target].MaxHp / 2;
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
            if(IsCharEnd)
            {
                color = Color.Gray;
            }
            else if (!IsCharEnd)
            {
                color = Color.White;
            }
        }
        public override void UpdateDraw(SpriteBatch batch, Vector2 P)
        {
            if (IsAlive)
            {
                if (Anime == "S1")
                {
                    This_Ani[2].DrawFrame(batch, P, true);
                }
                else if (Anime == "S2")
                {
                    This_Ani[3].DrawFrame(batch, P, true);
                }
                else if (Anime == "Spawn")
                {
                    This_Ani[1].DrawFrame(batch, P, true);
                }
                else if (Anime == "Hurt")
                {
                    This_Ani[4].DrawFrame(batch, P, true);
                }
                else
                {
                    This_Ani[0].DrawFrame(batch, P, true, color);
                }
            }
        }
    }
}