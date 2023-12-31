﻿using Microsoft.Xna.Framework;
using Project_Manhattan.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manhattan.CoreCode
{
    public class LFC        //เป็น World Class ใช้ list และ array ร่วมกับ class อื่น; ย่อจาก List Friend Center
    {
        AnimatedTexture Mickuy = new AnimatedTexture(Vector2.Zero, 0, 0.55f, 0);
        AnimatedTexture Dan_Heng = new AnimatedTexture(Vector2.Zero, 0, 0.55f, 0);
        AnimatedTexture YoungOhm = new AnimatedTexture(Vector2.Zero, 0, 0.55f, 0);
        AnimatedTexture Tornadome = new AnimatedTexture(Vector2.Zero, 0, 0.55f, 0);

        public static AnimatedTexture[] PAA = new AnimatedTexture[3];   //Player Array Animate
        public static List<AnimatedTexture> PLA = new List<AnimatedTexture>();  //play List Animate

        public static Friend[] PAS = new Friend[3];         //Player Array Skill
        public static List<Friend> PLS = new List<Friend>();//Player List Skill

        public static int[] select = new int[3];

        MainGame game1;
        public LFC(MainGame game)
        {
            //////// Animated Zone/////////
            Mickuy.Load(game.Content, "mig-stand-animation 128x32", 4, 1, 4);
            Dan_Heng.Load(game.Content, "heng-stand-animation 128x32", 4, 1, 4);
            YoungOhm.Load(game.Content, "om-stand-animation 132x33", 4, 1, 4);
            Tornadome.Load(game.Content, "dome-stand-animation 128x32", 4, 1, 4);

            PLA.Add(Dan_Heng); //0
            PLA.Add(Mickuy);   //1
            PLA.Add(YoungOhm); //2
            PLA.Add(Tornadome);//3

            //////////Object + Status Zone//////////////////
            Mickey mickey = new Mickey();
            Heng heng = new Heng();
            Ohm ohm = new Ohm();
            Dome dome = new Dome();

            PLS.Add(heng);
            PLS.Add(mickey);
            PLS.Add(ohm);
            PLS.Add(dome);

            //////////////
            select[0] = 0;
            select[1] = 1;
            select[2] = 2;

            this.game1 = game;
        }

        public void UpdateList(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            PAA[0] = PLA[select[0]];
            PAA[1] = PLA[select[1]];
            PAA[2] = PLA[select[2]];

            PAS[0] = PLS[select[0]];
            PAS[1] = PLS[select[1]];
            PAS[2] = PLS[select[2]];

            PAA[0].UpdateFrame(elapsed);
            PAA[1].UpdateFrame(elapsed);
            PAA[2].UpdateFrame(elapsed);
        }
    }
}
