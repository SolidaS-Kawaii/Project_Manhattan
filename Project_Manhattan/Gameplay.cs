using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Project_Manhattan
{
    internal class Gameplay : MainGame
    {
        public static int SelPos = 1;  //SelectPosition
        public static int EnePos = 1;

        private static List<Player_skill> playerList = new List<Player_skill>();
        private static List<Enemy_skill> enemyList = new List<Enemy_skill>();

        public static bool IsSkillEReady = false;
        public static bool IsSkillQReady = false;

        public static void LoadPlayer()
        {
            //////////////////// สร้างตัวละคร parametre เรียงตามนี้ Hp, Str, ค่า Energy Skill1, ค่า Energy Skill2 //////////////////////////

            Mickey mickey = new Mickey(10, 20, 0, 2);
            Heng heng = new Heng(5, 10, 1, 3);
            Ohm ohm = new Ohm(20, 15, 3, 2);

            playerList.Add(mickey);
            playerList.Add(heng);
            playerList.Add(ohm);

            Sensei sensei = new Sensei(100, 50);
            enemyList.Add(sensei);
            enemyList.Add(sensei);
            enemyList.Add(sensei);
        }
        public static void UpdateGameplay(GameTime gameTime)
        {

            ///////////////////////////// Energy /////////////////
            if(Energy > EnergyMax)
            {
                Energy = EnergyMax;     //กันไม่ให้ Energy เกิน Max
            }

            keytak = Keyboard.GetState();   //รับคีย์
            var anutin = Mouse.GetState();  //รับเมาส์

            ///////////////////// เลือกตำแหน่งSensei by keyboard ///////////////////////////////////////

            if (keytak.IsKeyDown(Keys.A) && keypiak.IsKeyUp(Keys.A))
            {
                if (EnePos > 0)
                {
                    EnePos -= 1;
                }
            }
            if (keytak.IsKeyDown(Keys.D) && keypiak.IsKeyUp(Keys.D))
            {
                if (EnePos < 2)
                {
                    EnePos += 1;
                }
            }
          
            for (int i = 0; i < playerList.Count; i++)
            {
                Rectangle PlayerScale = new Rectangle((int)PlayerPos[i].X, (int)PlayerPos[i].Y, 160, 160);
                if (PlayerScale.Contains(anutin.X, anutin.Y) && anutin.LeftButton == ButtonState.Pressed)
                {
                    IsSkillEReady = false;
                    IsSkillQReady = false;
                    SelPos = i;
                }

                //////////////////////////////// ใช้สกิลตัวละคร //////////////////////////
               
                if (keytak.IsKeyDown(Keys.E) && keypiak.IsKeyUp(Keys.E))        //สกิล 1 E
                {
                    if (i == SelPos && IsSkillEReady == false && playerList[i].IsCharEnd == false)
                    {
                        playerList[i].skill1_Info();
                        IsSkillEReady = true;
                        IsSkillQReady = false;
                    }
                    else if (i == SelPos && playerList[i].IsCharEnd == false && MainGame.Energy - playerList[i].Skill1_Cost >= 0 && IsSkillEReady == true)
                    {
                        playerList[i].skill1();
                        playerList[i].IsCharEnd = true;
                        IsSkillEReady = false;
                    }
                }
                if (keytak.IsKeyDown(Keys.Q) && keypiak.IsKeyUp(Keys.Q))        //สกิล 2 Q
                {
                    if (i == SelPos && IsSkillQReady == false && playerList[i].IsCharEnd == false)
                    {
                        playerList[i].skill2_Info();
                        IsSkillQReady = true;
                        IsSkillEReady = false;
                    }
                    else if (i == SelPos && playerList[i].IsCharEnd == false && MainGame.Energy - playerList[i].Skill2_Cost >= 0 && IsSkillQReady == true)
                    {
                        playerList[i].skill2();
                        playerList[i].IsCharEnd = true;
                        IsSkillQReady = false;
                    }
                }
            }

            ///////////////////// จบ Phase ////////////////////
            
            if (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter))
            {
                   IsMyPhase = false;
            }

        }
        public static void GetEnergy()
        {
            if (StartPhase == true)
            {
                Energy += 4;
                for (int i = 0; i < playerList.Count; i++)
                {
                    playerList[i].IsCharEnd = false;
                }
                StartPhase = false;
            }
        }
    }
}
