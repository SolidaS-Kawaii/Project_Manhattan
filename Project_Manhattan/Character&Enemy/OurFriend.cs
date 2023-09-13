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
        public Mickey(int hp, int str, int skill1_cost, int skill2_cost) : base( hp, str, skill1_cost, skill2_cost) 
        {
            
        }
        public override void skill1(int enePos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost ;
            Gameplay_Screen.enemyList[enePos].Hp -= Str;
        }
        public override void skill2(int enePos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost ;
            Gameplay_Screen.enemyList[enePos].Hp -= Str*3;
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "MickeyE (0E)";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "MickeyQ (2E)";
        }
    }

    public class Heng : Player_skill
    {

        public Heng(int hp, int str, int skill1_cost, int skill2_cost) : base(hp, str, skill1_cost, skill2_cost)
        {

        }
        public override void skill1(int enePos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost ;
            Gameplay_Screen.enemyList[enePos].Hp -= Str;
        }
        public override void skill2(int enePos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost ;
            Gameplay_Screen.enemyList[enePos].Hp -= Str*2;
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "HengE (1E)";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "HengQ (3E)";
        }
    }

    public class Ohm : Player_skill
    {

        public Ohm(int hp, int str, int skill1_cost, int skill2_cost) : base(hp, str, skill1_cost, skill2_cost)
        {

        }
        public override void skill1(int enePos)
        {
            Gameplay_Screen.Energy -= Skill1_Cost ;
            Gameplay_Screen.enemyList[enePos].Hp -= Str;
        }
        public override void skill2(int enePos)
        {
            Gameplay_Screen.Energy -= Skill2_Cost ;
            Gameplay_Screen.enemyList[enePos].Hp -= Str/2;
        }
        public override void skill1_Info()
        {
            Gameplay_Screen.SkillName = "OhmE (3E)";
        }
        public override void skill2_Info()
        {
            Gameplay_Screen.SkillName = "OhmQ (2E)";
        }
    }
}
