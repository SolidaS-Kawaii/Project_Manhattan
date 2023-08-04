using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/////////////////////////////////// เป็น Class เอาไว้ใส่สกิลตัวละครแต่ละตัว ///////////////////////////////////

namespace Project_Manhattan
{
    public class Mickey : Player_skill
    {
        public Mickey(int hp, int str, int skill1_cost, int skill2_cost) : base( hp, str, skill1_cost, skill2_cost) 
        {
            
        }
        public override void skill1()
        {
            MainGame.Energy -= Skill1_Cost ;
        }
        public override void skill2()
        {
            MainGame.Energy -= Skill2_Cost ;
        }
        public override void skill1_Info()
        {
            MainGame.SkillName = "MickeyE";
        }
        public override void skill2_Info()
        {
            MainGame.SkillName = "MickeyQ";
        }
    }

    public class Heng : Player_skill
    {

        public Heng(int hp, int str, int skill1_cost, int skill2_cost) : base(hp, str, skill1_cost, skill2_cost)
        {

        }
        public override void skill1()
        {
            MainGame.Energy -= Skill1_Cost ;
        }
        public override void skill2()
        {
            MainGame.Energy -= Skill2_Cost ;
        }
        public override void skill1_Info()
        {
            MainGame.SkillName = "HengE";
        }
        public override void skill2_Info()
        {
            MainGame.SkillName = "HengQ";
        }
    }

    public class Ohm : Player_skill
    {

        public Ohm(int hp, int str, int skill1_cost, int skill2_cost) : base(hp, str, skill1_cost, skill2_cost)
        {

        }
        public override void skill1()
        {
            MainGame.Energy -= Skill1_Cost ;
        }
        public override void skill2()
        {
            MainGame.Energy -= Skill2_Cost ;
        }
        public override void skill1_Info()
        {
            MainGame.SkillName = "OhmE";
        }
        public override void skill2_Info()
        {
            MainGame.SkillName = "OhmQ";
        }
    }
}
