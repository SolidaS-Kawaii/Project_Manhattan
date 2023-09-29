﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Manhattan.Content;
using Project_Manhattan.CoreCode;

namespace Project_Manhattan
{
    public class Sensei : Enemy_skill
    {
        public Sensei(int hp, int str) : base(hp, str)
        {

        }
        public override void skill1(int RandPos)
        {
            LFC.PAS[0].Hp -= this.Str;
            LFC.PAS[1].Hp -= this.Str;
            LFC.PAS[2].Hp -= this.Str;
        }
        public override void skill2(int RandPos)
        {

        }
        public override void skill1_Info()
        {

        }
        public override void skill2_Info()
        {

        }
    }
    public class MuscleRat : Enemy_skill
    {
        public MuscleRat(int hp, int str) : base(hp, str)
        {

        }
        public override void skill1(int RandPos)
        {
            LFC.PAS[RandPos].Hp -= this.Str;
        }
        public override void skill2(int RandPos)
        {

        }
        public override void skill1_Info()
        {

        }
        public override void skill2_Info()
        {

        }
    }
}
