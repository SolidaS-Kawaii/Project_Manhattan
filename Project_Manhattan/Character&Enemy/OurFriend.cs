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
    public class Mickey : Player_skill
    {   

        public Mickey() : base() 
        {
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

            // สกิล1 เตะบอลใส่ศัตรูสร้างความเสียหาย100%atk
            // สกิล2 เตะบอลอัดใส่ศัตรูอย่างรุนแรงสร้างความเสียหาย 200 % atk
        }
        public override void skill1(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost ;
            Gameplay_Screen.enemyList[enePos].Hp -= Str;
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost ;
            Gameplay_Screen.enemyList[enePos].Hp -= Str*2;
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "MickeyE (2E)";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "MickeyQ (3E)";
        }
    }

    public class Heng : Player_skill
    {

        public Heng() : base()
        {
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
            Gameplay_Screen.Energy -= Skill1_Cost ;
            LFC.PAS[enePos].Hp += 150;
            if( LFC.PAS[enePos].Hp >= LFC.PAS[enePos].MaxHp)
            {
                LFC.PAS[enePos].Hp = LFC.PAS[enePos].MaxHp;
            }
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost ;
            for(int i = 0; i < LFC.PAS.Length; i++)
            {
                LFC.PAS[i].Str += 25;
            }
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "HengE (2E)";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "HengQ (2E)";
        }
    }

    public class Ohm : Player_skill
    {

        public Ohm() : base()
        {
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
            Gameplay_Screen.Energy -= Skill1_Cost ;
            LFC.PAS[Gameplay_Screen.Caster].Def += 50;
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost ;
            LFC.PAS[Gameplay_Screen.Caster].Def += 100;
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "OhmE (1E)";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "OhmQ (3E)";
        }
    }
    public class Dome : Player_skill
    {

        public Dome() : base()
        {
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

            //สกิล1 ใช้เหม่งสะท้อนใส่ศัตรูสร้างความเสียหาย75%atk
            //สกิล2 ใช้ไสยเวยต้องห้ามโจมตีศัตรู200 % atkพร้อมทั้งได้รับผลกระทบโดยของเข้าตัวลดhp25หน่วย
        }
        public override void skill1(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost;
            Gameplay_Screen.enemyList[enePos].Hp -= Str*75/100;
        }
        public override void skill2(int enePos, int CastPos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost;
            Gameplay_Screen.enemyList[enePos].Hp -= Str*2;
            Hp -= 25;
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "DomeE (1E)";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "DomeQ (3E)";
        }
    }
}