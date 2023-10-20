using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Manhattan.Content;
using Project_Manhattan.CoreCode;
using Project_Manhattan.Screen_Management;

/////////////////////////////////// เป็น Class เอาไว้ใส่สกิลตัวละครแต่ละตัว ///////////////////////////////////

namespace Project_Manhattan
{
    public class Mickey : Friend
    {
        public Mickey() : base()
        {
            Name = "Miki";

            Hp = 500;
            Str = 300;
            Def = 50;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            Skill1_Cost = 2;
            Skill2_Cost = 3;

            Target_1 = "Enemy";
            Target_2 = "Enemy";

            // สกิล1 เตะบอลใส่ศัตรูสร้างความเสียหาย100%atk
            // สกิล2 เตะบอลอัดใส่ศัตรูอย่างรุนแรงสร้างความเสียหาย 200 % atk
        }
        public override void skill1(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost;
            this.IsAction = true;
            Anime = "S1";
            target = enePos;
            cast = CastPos;
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost;
            this.IsAction = true;
            Anime = "S2";
            target = enePos;
            cast = CastPos;
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
                LFC.PAA[cast] = LFC.Mickuy[2];
                if (LFC.PAA[cast].IsEnd)
                {
                    LFC.PAS[cast].IsAction = false;
                    LFC.PAA[cast] = LFC.Mickuy[1];
                    LEC.enemies[target].Hp -= Str;
                    LEC.enemies[target].IsHurt = true;
                }
            }
            else if(IsAction && Anime == "S2")
            {
                LFC.PAA[cast] = LFC.Mickuy[3];
                if (LFC.PAA[cast].IsEnd)
                {
                    LFC.PAS[cast].IsAction = false;
                    LFC.PAA[cast] = LFC.Mickuy[1];
                    LEC.enemies[target].Hp -= Str * 2;
                    LEC.enemies[target].IsHurt = true;
                }
            }
        }
    }

    public class Heng : Friend
    {
        public Heng() : base()
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

            Target_1 = "Friend";
            Target_2 = "AllFriend";

            //สกิล1 ใช้ยาจากกระเป่าคู่ใจฮีลเพื่อนร่วมทีม150หน่วย
            //สกิล2 ตะโกนปลุกใจเพื่อนร่วมทีมเพิ่มatk25หน่วย
        }
        public override void skill1(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost;
            LFC.PAS[enePos].Hp += 150;
            if (LFC.PAS[enePos].Hp >= LFC.PAS[enePos].MaxHp)
            {
                LFC.PAS[enePos].Hp = LFC.PAS[enePos].MaxHp;
            }
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost;
            for (int i = 0; i < LFC.PAS.Length; i++)
            {
                LFC.PAS[i].Str += 25;
            }
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "HengE (2E)";
            Gameplay_Screen.SkillInfo = "Pick up a medicine form my lover bag \nHeals 150 Hp";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "HengQ (2E)";
            Gameplay_Screen.SkillInfo = "Shouttttttttttttt! \nBuffs all of our friends with 25 Str";
        }
        public override void UpdateAction()
        {

        }
    }

    public class Ohm : Friend
    {
        public Ohm() : base()
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
            Target_2 = "Self";

            //สกิล1 ยกดัมเบลเพื่อสร้างความแข็งแรงให้กล้ามเนื้อDef+50
            //สกิล2 เบ่งกล้ามอวดศัตรูเพื่อยัวยุศัตรูให้มาตีพร้อมทั้งได้รับค่าdefเพิ่มขึ้น100
        }
        public override void skill1(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost;
            LFC.PAS[Gameplay_Screen.Caster].Def += 50;
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost;
            LFC.PAS[Gameplay_Screen.Caster].Def += 100;
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "OhmE (1E)";
            Gameplay_Screen.SkillInfo = "I can lift a dumbbell all day \nDef up 50";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "OhmQ (3E)";
            Gameplay_Screen.SkillInfo = "Taunt all of our sensei! \nTaunt and Def up 100";
        }
        public override void UpdateAction()
        {

        }
    }
    public class Dome : Friend
    {
        public Dome() : base()
        {
            Name = "Domino";
            Hp = 450;
            Str = 275;
            Def = 25;

            MaxHp = Hp;
            MaxStr = Str;
            MaxDef = Def;

            Skill1_Cost = 1;
            Skill2_Cost = 3;

            Target_1 = "Enemy";
            Target_2 = "Enemy";

            //สกิล1 ใช้เหม่งสะท้อนใส่ศัตรูสร้างความเสียหาย75% atk
            //สกิล2 ใช้ไสยเวยต้องห้ามโจมตีศัตรู200 % atkพร้อมทั้งได้รับผลกระทบโดยของเข้าตัวลดhp25หน่วย
        }
        public override void skill1(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost;
            LEC.enemies[enePos].Hp -= Str * 75 / 100;
            LEC.enemies[enePos].IsHurt = true;
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost;
            LEC.enemies[enePos].Hp -= Str * 2;
            Hp -= 25;
            LEC.enemies[enePos].IsHurt = true;
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

        }
    }
}