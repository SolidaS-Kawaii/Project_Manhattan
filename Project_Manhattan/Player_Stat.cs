using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manhattan
{
    public class Player_Stat 
    {

    }

    public class Mickey : Player_skill
    {

        public Mickey(int hp, int str) : base( hp, str) 
        {
        }

        public override void skill1()
        {
            Console.WriteLine("212");
        }

        public override void skill2()
        {
            throw new NotImplementedException();
        }
    }
}
