using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project_Manhattan.Content;
using Project_Manhattan.CoreCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Project_Manhattan.Screen_Management
{
    public class Gameplay_Screen : Screen
    {
        private KeyboardState keytak;                   //New Key
        private KeyboardState keypiak;                  //Old key
        //private MouseState anutin;                      //Mouse click
        //private MouseState chanveerakul;                //Mouse Position

        private Random random = new Random();

        private float Cooldown = 0f;
        private float delay = 0f;

        private float Uptime = 0f;
        private bool trigger = true;

        public static bool IsMyPhase = true;
        public static bool StartPhase = true;

        public int SelPos = 1;  //SelectPosition
        public int TargetPos = 1;
        private int turn_e = 0;
        public static int Caster;

        public static int Energy = 0;
        public const int EnergyMax = 6;

        private AnimatedTexture Eniem;
        private AnimatedTexture GeeGee;
        private AnimatedTexture BG_Hotel_UI;

        private const float Rotation = 0.0f;
        private const float Scale = 1.0f;   //ใช้ขยายขนาด obj
        private const float Depth = 1.0f;

        private Texture2D Select_Enemy;
        private Texture2D Select_Friend;
        private Texture2D Select_Me;
        private Texture2D Select_Pos;
        private Texture2D Healthbar_Fr;
        private Texture2D Healthbar_En;
        private Texture2D Healthbar_Bk;
        private Texture2D UI;
        private Texture2D Arrow1;
        private SpriteFont font;
        private SpriteFont font48;

        public static List<Enemy_skill> enemyList = new List<Enemy_skill>();

        private bool IsSkillEReady = false;
        private bool IsSkillQReady = false;

        public Vector2[] PlayerPos = new Vector2[3];   //Player Position
        public Vector2[] EnemyPos = new Vector2[3];      //Enemy Position
        private Vector2 UIPos = new Vector2(112, 724);
        private Vector2 NameShow;
        private Vector2 HpShow;
        private Vector2 StrShow;
        private Vector2 DefShow;

        private string Target_task;
        private string _Hp = "HP";
        private string _Str = "STR";
        private string _Def = "DEF";
        private string Phase_Status = "Our Phase";

        public static string Ener = "Energy : ";
        public static string check = "";
        public static string check2 = "Skill : ";
        public static string Hpcheck = "Hp : ";
        public static string SkillInfo;
        public static string SkillName;

        MainGame game;

        public Gameplay_Screen(MainGame game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            Eniem = new AnimatedTexture(Vector2.Zero, Rotation, 0.6f, Depth);
            BG_Hotel_UI = new AnimatedTexture(Vector2.Zero, Rotation, 1.0f, Depth);
            GeeGee = new AnimatedTexture(Vector2.Zero, Rotation, 0.5f, Depth);

            Eniem.Load(game.Content, "2D/Enemy/bjj idle-400x80", 5, 1, 8);
            GeeGee.Load(game.Content, "2D/Enemy/Keke_idle", 2, 1, 4);
            BG_Hotel_UI.Load(game.Content, "2D/BG/Hospital", 1, 1, 0);
            UI = game.Content.Load<Texture2D>("2D/UI/UI1 (1)");
            Select_Enemy = game.Content.Load<Texture2D>("2D/UI/Selecting");
            Select_Friend = game.Content.Load<Texture2D>("2D/UI/Selecting2");
            Select_Me = game.Content.Load<Texture2D>("2D/UI/Selecting3");
            Select_Pos = game.Content.Load<Texture2D>("2D/UI/Selecting4");
            Arrow1 = game.Content.Load<Texture2D>("2D/UI/arrow-1");
            Healthbar_Bk = game.Content.Load<Texture2D>("2D/UI/Health_Back");
            Healthbar_Fr = game.Content.Load<Texture2D>("2D/UI/Health_Friend");
            Healthbar_En = game.Content.Load<Texture2D>("2D/UI/Health_Enemy");

            font = game.Content.Load<SpriteFont>("File");
            font48 = game.Content.Load<SpriteFont>("Oth/Font/Arial48");

            PlayerPos[0] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width - 900, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);
            PlayerPos[1] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width - 700, MainGame._graphics.GraphicsDevice.Viewport.Height / 2 - 100);
            PlayerPos[2] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width - 500, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);

            EnemyPos[0] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width / 4 - 200, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);
            EnemyPos[1] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width / 4 , MainGame._graphics.GraphicsDevice.Viewport.Height / 2 - 250);
            EnemyPos[2] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width / 4 + 200, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);

            NameShow = new Vector2(UIPos.X + 650, UIPos.Y);
            HpShow = new Vector2(UIPos.X + 875, UIPos.Y);
            StrShow = new Vector2(UIPos.X + 1145, UIPos.Y);
            DefShow = new Vector2(UIPos.X + 1410, UIPos.Y);

            Sensei sensei = new Sensei(1000, 115);
            MuscleRat muscleRat = new MuscleRat(500, 50);
            MuscleRat muscleRat1 = new MuscleRat(500, 50);

            enemyList.Add(muscleRat);
            enemyList.Add(sensei);
            enemyList.Add(muscleRat1);

            this.game = game;
        }
        public override void Update(GameTime gameTime)
        {
            Console.Clear();
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Uptime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            ///////////////////////////// Energy /////////////////
            if (Energy > EnergyMax)
            {
                Energy = EnergyMax;     //กันไม่ให้ Energy เกิน Max
            }

            keytak = Keyboard.GetState();   //รับคีย์
            var anutin = Mouse.GetState();  //รับเมาส์

            ///////////////////// เลือกตำแหน่งSensei by keyboard ///////////////////////////////////////

            if (IsMyPhase && trigger)
            {
                if (keytak.IsKeyDown(Keys.A) && keypiak.IsKeyUp(Keys.A))
                {
                    if (SelPos > 0)
                    {
                        SelPos -= 1;
                    }
                    IsSkillEReady = false;
                    IsSkillQReady = false;
                }
                else if (keytak.IsKeyDown(Keys.D) && keypiak.IsKeyUp(Keys.D))
                {
                    if (SelPos < 2)
                    {
                        SelPos += 1;
                    }
                    IsSkillEReady = false;
                    IsSkillQReady = false;
                }

                if (keytak.IsKeyDown(Keys.J) && keypiak.IsKeyUp(Keys.J))
                {
                    if (TargetPos > 0)
                    {
                        TargetPos -= 1;
                    }
                    IsSkillEReady = false;
                    IsSkillQReady = false;
                }
                else if (keytak.IsKeyDown(Keys.L) && keypiak.IsKeyUp(Keys.L))
                {
                    if (TargetPos < 2)
                    {
                        TargetPos += 1;
                    }
                    IsSkillEReady = false;
                    IsSkillQReady = false;
                }
                for (int i = 0; i < enemyList.Count; i++)
                {
                    /*
                    Rectangle EnemyScale = new Rectangle((int)EnemyPos[i].X, (int)EnemyPos[i].Y, 160, 160);
                    if (EnemyScale.Contains(anutin.X, anutin.Y) && anutin.LeftButton == ButtonState.Pressed)
                    {
                        IsSkillEReady = false;
                        IsSkillQReady = false;
                        EnePos = i;
                    }*/

                    //////////////////////////////// ใช้สกิลตัวละคร //////////////////////////

                    if (keytak.IsKeyDown(Keys.E) && keypiak.IsKeyUp(Keys.E))        //สกิล 1 E                 
                    {
                        if (i == SelPos && IsSkillEReady == false && LFC.PAS[i].IsCharEnd == false && LFC.PAS[i].IsAlive)
                        {
                            LFC.PAS[i].skill1_Info();
                            IsSkillEReady = true;
                            IsSkillQReady = false;
                            Target_task = LFC.PAS[i].Target_1;
                            Caster = i;
                        }
                        else if (i == SelPos && LFC.PAS[i].IsCharEnd == false && Energy - LFC.PAS[i].Skill1_Cost >= 0 && IsSkillEReady == true)
                        {
                            LFC.PAS[i].skill1(TargetPos, Caster);
                            LFC.PAS[i].IsCharEnd = true;
                            IsSkillEReady = false;
                            Delay(3);
                        }
                    }
                    if (keytak.IsKeyDown(Keys.Q) && keypiak.IsKeyUp(Keys.Q))        //สกิล 2 Q
                    {
                        if (i == SelPos && IsSkillQReady == false && LFC.PAS[i].IsCharEnd == false && LFC.PAS[i].IsAlive)
                        {
                            LFC.PAS[i].skill2_Info();
                            IsSkillQReady = true;
                            IsSkillEReady = false;
                            Target_task = LFC.PAS[i].Target_2;
                            Caster = i;
                        }
                        else if (i == SelPos && LFC.PAS[i].IsCharEnd == false && Energy - LFC.PAS[i].Skill2_Cost >= 0 && IsSkillQReady == true)
                        {
                            LFC.PAS[i].skill2(TargetPos, Caster);
                            LFC.PAS[i].IsCharEnd = true;
                            IsSkillQReady = false;
                            Delay(3);
                        }
                    }

                }

                ///////////////////// จบ Phase ////////////////////

                if (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter) && IsMyPhase)
                {
                    IsMyPhase = false;
                    turn_e = 0;
                }
            }

            ///////////////// Phase ศัตรู ///////////////////
            if (!IsMyPhase)
            {
                if (trigger)
                {
                    Delay(5);
                    int RandPos = random.Next(0, 3);
                    if (turn_e == 1)
                    {
                        if (enemyList[turn_e - 1].IsAlive)
                        {
                            enemyList[turn_e - 1].skill1(RandPos);
                        }
                        else if (!enemyList[turn_e - 1].IsAlive)
                        {
                            turn_e++;
                        }
                        RandPos = random.Next(0, 3);
                    }
                    else if (turn_e == 2)
                    {
                        if (enemyList[turn_e - 1].IsAlive)
                        {
                            enemyList[turn_e - 1].skill1(RandPos);
                        }
                        else if (!enemyList[turn_e - 1].IsAlive)
                        {
                            turn_e++;
                        }
                        RandPos = random.Next(0, 3);
                    }
                    else if (turn_e == 3)
                    {
                        if (enemyList[turn_e - 1].IsAlive)
                        {
                            enemyList[turn_e - 1].skill1(RandPos);
                        }
                        else if (!enemyList[turn_e - 1].IsAlive)
                        {
                            turn_e++;
                        }
                        RandPos = random.Next(0, 3);
                    }
                    turn_e++;
                }
                if (turn_e >= enemyList.Count + 1)
                {
                    IsMyPhase = true;
                    StartPhase = true;
                }
            }

            ///////////////           
            if (StartPhase == true)
            {
                Energy += 4;
                for (int i = 0; i < LFC.PAS.Length; i++)
                {
                    LFC.PAS[i].IsCharEnd = false;
                }
                StartPhase = false;
            }

            if (IsMyPhase)
            {
                Phase_Status = "Our Phase";
            }
            else if(!IsMyPhase)
            {
                Phase_Status = "Sensei Phase";
            }
            ////////////////////

            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].Hp <= 0)
                {
                    enemyList[i].Hp = 0;
                    enemyList[i].IsAlive = false;
                }
            }

            for (int i = 0; i < LFC.PAS.Length; i++)
            {
                if (LFC.PAS[i].Hp <= 0)
                {
                    LFC.PAS[i].Hp = 0;
                    LFC.PAS[i].IsAlive = false;
                }
            }

            //////////// เวลา //////////////
            if (Cooldown < delay && !trigger)
                Cooldown += Uptime;
            else if (Cooldown >= delay && !trigger)
            {
                trigger = true;
            }
            ///////////////////////////

            keypiak = keytak;
            Eniem.UpdateFrame(Elapsed);
            GeeGee.UpdateFrame(Elapsed);
            Console.WriteLine(Cooldown);
            Console.WriteLine(turn_e);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            BG_Hotel_UI.DrawFrame(theBatch, Vector2.Zero, false);
            ////////////// วาด UI //////////
            if(IsMyPhase)
            {
                theBatch.DrawString(font48, Phase_Status, new Vector2(1500, 25), Color.CornflowerBlue);
            }
            else if(!IsMyPhase)
            {
                theBatch.DrawString(font48, Phase_Status, new Vector2(1400, 25), Color.Red);
            }

            theBatch.Draw(UI, UIPos, new Rectangle(0, 384, 1696, 288), Color.White);
            theBatch.DrawString(font, Ener + Energy + " / 6", UIPos + new Vector2(25, 25), Color.White);
            theBatch.DrawString(font, check2 + SkillName, UIPos + new Vector2(25, 75), Color.White);
            theBatch.DrawString(font, check + SkillInfo, UIPos + new Vector2(25, 125), Color.White);
            theBatch.DrawString(font, _Hp, HpShow + new Vector2(0, 25), Color.White);
            theBatch.DrawString(font, _Str, StrShow + new Vector2(0, 25), Color.White);
            theBatch.DrawString(font, _Def, DefShow + new Vector2(0, 25), Color.White);

            ///////////วาด เพื่อน/////////////

            for (int i = 0; i < LFC.PAS.Length; i++)
            {

                if (LFC.PAS[i].IsAlive)
                {
                    if (!LFC.PAS[i].IsHurt)
                    {
                        LFC.PAA[i].DrawFrame(theBatch, PlayerPos[i], true);
                    }
                    else
                    {
                        LFC.PAS[i].IsHurt = false;
                    }
                }

                if (LFC.PAS[i].IsAlive)
                {
                    theBatch.DrawString(font, LFC.PAS[i].Name , NameShow + new Vector2(0, 25 + (50 * (i + 1))), Color.White);
                    theBatch.DrawString(font, LFC.PAS[i].Hp + " / " + LFC.PAS[i].MaxHp, HpShow + new Vector2(0, 25 + (50 * (i + 1))), Color.White);
                    theBatch.DrawString(font, LFC.PAS[i].Str + "", StrShow + new Vector2(0, 25 + (50 * (i + 1))), Color.White);
                    theBatch.DrawString(font, LFC.PAS[i].Def + "", DefShow + new Vector2(0, 25 + (50 * (i + 1))), Color.White);
                    theBatch.Draw(Healthbar_Bk, PlayerPos[i] + new Vector2(25, -120), new Rectangle(0, 0, 200, 50), Color.White, 0, Vector2.Zero, 0.75f, 0, 0);
                    theBatch.Draw(Healthbar_Fr, PlayerPos[i] + new Vector2(25, -120), new Rectangle(0, 0, (200 * (LFC.PAS[0].Hp * 100 / LFC.PAS[0].MaxHp)) / 100, 50), Color.White, 0, Vector2.Zero, 0.75f, 0, 0);
                }
                else if (!LFC.PAS[i].IsAlive)
                {
                    theBatch.DrawString(font, LFC.PAS[i].Name, UIPos + new Vector2(650, 25 + (50 * (i + 1))), Color.Gray);
                    theBatch.DrawString(font, LFC.PAS[i].Hp + " / " + LFC.PAS[i].MaxHp, HpShow + new Vector2(0, 25 + (50 * (i + 1))), Color.Gray);
                    theBatch.DrawString(font, LFC.PAS[i].Str + "", StrShow + new Vector2(0, 25 + (50 * (i + 1))), Color.Gray);
                    theBatch.DrawString(font, LFC.PAS[i].Def + "", DefShow + new Vector2(0, 25 + (50 * (i + 1))), Color.Gray);
                    theBatch.Draw(Healthbar_Bk, new Vector2(400, 0), new Rectangle(0, 0, 200, 50), Color.White, 0, Vector2.Zero, 0.75f, 0, 0);
                }
            }

            /////////////วาด จารย์////////////////
            for(int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].IsAlive)
                {
                    theBatch.Draw(Healthbar_Bk, EnemyPos[i] + new Vector2(-10, -80), new Rectangle(0, 0, 200, 50), Color.White, 0, Vector2.Zero, 1f, 0, 0);
                    theBatch.Draw(Healthbar_En, EnemyPos[i] + new Vector2(-10, -80), new Rectangle(0, 0, (200 * (enemyList[i].Hp * 100 / enemyList[i].MaxHp)) / 100, 50), Color.White, 0, Vector2.Zero, 1f, 0, 0);

                    theBatch.DrawString(font, Hpcheck + enemyList[i].Hp, new Vector2(EnemyPos[i].X + 25, EnemyPos[i].Y - 70), Color.White);
                }
            }

            if (enemyList[0].Hp > 0)
            {
                if (!enemyList[0].IsHurt)
                {
                    GeeGee.DrawFrame(theBatch, EnemyPos[0], false);
                }
                else
                {
                    enemyList[0].IsHurt = false;
                }
            }
            if (enemyList[1].Hp > 0)
            {
                if (!enemyList[1].IsHurt)
                {
                    Eniem.DrawFrame(theBatch, new Vector2(330, 140), false);
                }
                else
                {
                    enemyList[1].IsHurt = false;
                }
            }
            if (enemyList[2].Hp > 0)
            {
                if (!enemyList[2].IsHurt)
                {
                    GeeGee.DrawFrame(theBatch, EnemyPos[2], false);
                }
                else
                {
                    enemyList[2].IsHurt = false;
                }
            }

            //////////////วาด ลูกศร//////////
            if (Target_task == "Enemy")
            {
                theBatch.Draw(Select_Enemy, new Vector2(EnemyPos[TargetPos].X + 50, EnemyPos[TargetPos].Y - 50), Color.White);
            }
            else if (Target_task == "Friend")
            {
                theBatch.Draw(Select_Friend, new Vector2(PlayerPos[TargetPos].X + 50, PlayerPos[TargetPos].Y - 100), Color.White);
            }
            else if (Target_task == "AllFriend")
            {
                theBatch.Draw(Select_Friend, new Vector2(PlayerPos[0].X + 50, PlayerPos[0].Y - 100), Color.White);
                theBatch.Draw(Select_Friend, new Vector2(PlayerPos[1].X + 50, PlayerPos[1].Y - 100), Color.White);
                theBatch.Draw(Select_Friend, new Vector2(PlayerPos[2].X + 50, PlayerPos[2].Y - 100), Color.White);
            }
            else if (Target_task == "Self")
            {
                theBatch.Draw(Select_Me, new Vector2(PlayerPos[Caster].X + 50, PlayerPos[Caster].Y - 100), Color.Blue);
            }
            theBatch.Draw(Select_Pos, new Vector2(PlayerPos[SelPos].X + 50, PlayerPos[SelPos].Y - 50), Color.Yellow);

            theBatch.Draw(Arrow1, new Vector2(NameShow.X - 150, NameShow.Y - 25 + (50 * (SelPos))), Color.White);

        }

        void Delay(float amountoftime)
        {
            delay = amountoftime;
            Cooldown = 0;
            Uptime = 0;
            trigger = false;
        }
    }
}
