using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private GamePadState gamePadState;
        //private MouseState anutin;                      //Mouse click
        //private MouseState chanveerakul;                //Mouse Position

        private Random random = new Random();
        private int RandPos = 0;
        private int RandPoss = 0;

        private float Cooldown = 0f;
        private float delay = 0f;

        private float Uptime = 0f;
        private bool trigger = true;

        private bool IsWin = false;
        private bool IsLoss = false;
        private bool IsGameEnd = false;

        public bool IsMyPhase = true;
        public bool StartPhase = true;

        public int SelPos = 1;  //SelectPosition
        public int TargetPos = 1;
        private int turn_e = 0;
        public static int Caster;

        public static int Energy = 0;
        public const int EnergyMax = 7;

        Texture2D BG_hospital;
        Texture2D BG_temple;
        Texture2D BG_tapae;

        private Texture2D WinLoss;
        private Texture2D Select_Enemy;
        private Texture2D Select_Friend;
        private Texture2D Select_Me;
        private Texture2D Select_Pos;
        private Texture2D Healthbar_Fr;
        private Texture2D Healthbar_En;
        private Texture2D Healthbar_Bk;
        private Texture2D UI;
        private Texture2D Arrow1;
        private Texture2D Tutorial;
        private SpriteFont font;
        private SpriteFont font48;

        AnimatedTexture EnterK = new AnimatedTexture(Vector2.Zero, 0, 0.5f, 0);

        private bool IsSkillEReady = false;
        private bool IsSkillQReady = false;
        private bool IsCharge = false;

        public static Vector2[] PlayerPos = new Vector2[3];   //Player Position
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

        Level elevel;
        enum Level
        {
            Hospital,
            Temple,
            Tapae
        }

        MainGame game;

        public Gameplay_Screen(MainGame game, EventHandler theScreenEvent) : base(game,theScreenEvent)
        {
            BG_hospital = game.Content.Load<Texture2D>("2D/BG/Hospital (1)");
            BG_temple = game.Content.Load<Texture2D>("2D/BG/Temple");
            BG_tapae = game.Content.Load<Texture2D>("2D/BG/Tapae");

            WinLoss = game.Content.Load<Texture2D>("2D/UI/Vic_De");
            UI = game.Content.Load<Texture2D>("2D/UI/UI1 (1)");
            Select_Enemy = game.Content.Load<Texture2D>("2D/UI/Selecting");
            Select_Friend = game.Content.Load<Texture2D>("2D/UI/Selecting2");
            Select_Me = game.Content.Load<Texture2D>("2D/UI/Selecting3");
            Select_Pos = game.Content.Load<Texture2D>("2D/UI/Selecting4");
            Arrow1 = game.Content.Load<Texture2D>("2D/UI/arrow-1");
            Healthbar_Bk = game.Content.Load<Texture2D>("2D/UI/Health_Back");
            Healthbar_Fr = game.Content.Load<Texture2D>("2D/UI/Health_Friend");
            Healthbar_En = game.Content.Load<Texture2D>("2D/UI/Health_Enemy");
            Tutorial = game.Content.Load<Texture2D>("2D/UI/Tutorial2");

            EnterK.Load(game.Content, "2D/UI/EnterKey", 2, 1, 2);

            font = game.Content.Load<SpriteFont>("File");
            font48 = game.Content.Load<SpriteFont>("Oth/Font/Arial48");

            PlayerPos[0] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width - 900, MainGame._graphics.GraphicsDevice.Viewport.Height / 2 - 100);
            PlayerPos[1] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width - 700, MainGame._graphics.GraphicsDevice.Viewport.Height / 2 - 150);
            PlayerPos[2] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width - 500, MainGame._graphics.GraphicsDevice.Viewport.Height / 2 - 100);

            EnemyPos[0] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width / 4 - 250, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);
            EnemyPos[1] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width / 4 - 100, MainGame._graphics.GraphicsDevice.Viewport.Height / 2 - 300);
            EnemyPos[2] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width / 4 + 300, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);

            NameShow = new Vector2(UIPos.X + 650, UIPos.Y);
            HpShow = new Vector2(UIPos.X + 875, UIPos.Y);
            StrShow = new Vector2(UIPos.X + 1145, UIPos.Y);
            DefShow = new Vector2(UIPos.X + 1410, UIPos.Y);

            this.game = game;
        }
        public override void Update(GameTime gameTime)
        {
            //Console.Clear();
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Uptime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            keytak = Keyboard.GetState();   //รับคีย์
            var anutin = Mouse.GetState();  //รับเมาส์
            gamePadState = GamePad.GetState(PlayerIndex.One);

            if(!MainGame.IsNightMare)
            {
                switch (elevel)
                {
                    case Level.Hospital:
                        {
                            if (IsWin && (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter)))
                            {
                                elevel = Level.Temple;
                                ScreenEvent.Invoke(game.mstory_Screen, new EventArgs());
                            }
                            break;
                        }
                    case Level.Temple:
                        {
                            if (IsWin && (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter)))
                            {
                                elevel = Level.Tapae;
                                ScreenEvent.Invoke(game.mstory_Screen, new EventArgs());
                            }
                            break;
                        }
                    case Level.Tapae:
                        {
                            if (IsWin && (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter)))
                            {
                                ScreenEvent.Invoke(game.mstory_Screen, new EventArgs());
                            }
                            break;
                        }
                }

                if (IsWin && (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter)))
                {
                    IsWin = false;
                    IsLoss = false;
                    IsGameEnd = false;
                    StartPhase = true;
                    IsMyPhase = true;
                    LEC.enemies[1].Resetto();
                    MediaPlayer.Play(song[1]);
                    Energy = 0;
                    ResetFede();
                    for (int i = 0; i < 3; i++)
                    {
                        LFC.friend[i].Hp = 1;
                        LFC.friend[i].IsAlive = true;
                        LFC.friend[i].Anime = "Idle";
                        LFC.friend[i].IsCharEnd = false;
                    }
                }
                else if (IsLoss && (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter)))
                {
                    IsLoss = false;
                    IsWin = false;
                    IsGameEnd = false;
                    StartPhase = true; 
                    IsMyPhase = true;
                    LEC.enemies[1].Resetto();
                    MediaPlayer.Play(song[2]);
                    Energy = 0;
                    ResetFede();
                    for (int i = 0; i < 3; i++)
                    {
                        LFC.friend[i].Hp = 1;
                        LFC.friend[i].IsAlive = true;
                        LFC.friend[i].Anime = "Idle";
                        LFC.friend[i].IsCharEnd = false;
                    }
                    ScreenEvent.Invoke(game.mTeam_Manage, new EventArgs());
                }
                if (!IsMyPhase)
                {
                    if (trigger)
                    {
                        Delay(3);
                        do
                        {
                            RandPos = random.Next(0, 3);
                        }
                        while (!LFC.friend[RandPos].IsAlive);
                        if (turn_e == 1)
                        {
                            if (LEC.enemies[1].IsAlive)
                            {
                                LEC.enemies[1].skill(RandPos);
                            }
                        }

                        turn_e++;
                    }
                    if (LEC.enemies[1].IsEnd)
                    {
                        IsMyPhase = true;
                        StartPhase = true;
                        LEC.enemies[1].IsEnd = false;
                        turn_e = 0;
                    }
                }

                if (((!LEC.enemies[1].IsAlive) || (Keyboard.GetState().IsKeyDown(Keys.NumPad1))) && !IsWin && !IsGameEnd)
                {
                    IsWin = true;
                    IsLoss = false;
                    IsGameEnd = true;
                    sfx[5].Play();
                }
                else if ((LFC.friend[0].Hp <= 0 && LFC.friend[1].Hp <= 0 && LFC.friend[2].Hp <= 0 || (Keyboard.GetState().IsKeyDown(Keys.NumPad2))) && !IsLoss && !IsGameEnd)
                {
                    IsLoss = true;
                    IsWin = false;
                    IsGameEnd = true;
                    sfx[6].Play();
                }
            }
            else if (MainGame.IsNightMare)
            {
                if (IsWin && (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter)))
                {
                    IsLoss = false;
                    IsWin = false;
                    IsGameEnd = false;
                    StartPhase = true;
                    IsMyPhase = true;
                    MediaPlayer.Play(song[0]);
                    MainGame.IsNightMare = false;
                    Energy = 0;
                    ResetFede();
                    for (int i = 0; i < 3; i++)
                    {
                        LFC.friend[i].Hp = 1;
                        LFC.friend[i].IsAlive = true;
                        LFC.friend[i].Anime = "Idle";
                        LFC.friend[i].IsCharEnd = false;
                    }
                    ScreenEvent.Invoke(game.mTitile_Screen, new EventArgs());
                }
                else if(IsLoss && (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter)))
                {
                    IsLoss = false;
                    IsWin = false;
                    IsGameEnd = false;
                    StartPhase = true;
                    IsMyPhase = true;
                    MediaPlayer.Play(song[0]);
                    MainGame.IsNightMare = false; 
                    Energy = 0;
                    ResetFede();
                    for (int i = 0; i < 3; i++)
                    {
                        LFC.friend[i].Hp = 1;
                        LFC.friend[i].IsAlive = true;
                        LFC.friend[i].Anime = "Idle";
                        LFC.friend[i].IsCharEnd = false;
                    }
                    ScreenEvent.Invoke(game.mTitile_Screen, new EventArgs());
                }
                if (!IsMyPhase)
                {
                    if (trigger)
                    {
                        Delay(3);
                        do
                        {
                            RandPos = random.Next(0, 3);
                        }
                        while (!LEC.enemies[RandPos].IsAlive);
                        if (turn_e == 1)
                        {
                            if (LEC.enemies[RandPos].IsAlive)
                            {
                                do
                                {
                                    RandPoss = random.Next(0, 3);
                                }
                                while (!LFC.friend[RandPoss].IsAlive);
                                LEC.enemies[RandPos].skill(RandPoss);
                            }
                        }

                        turn_e++;
                    }
                    if (LEC.enemies[RandPos].IsEnd)
                    {
                        IsMyPhase = true;
                        StartPhase = true;
                        LEC.enemies[RandPos].IsEnd = false;
                        turn_e = 0;
                    }
                }
                if (((!LEC.enemies[1].IsAlive && !LEC.enemies[0].IsAlive && !LEC.enemies[2].IsAlive) || (Keyboard.GetState().IsKeyDown(Keys.NumPad2))) && !IsGameEnd && !IsWin)
                {
                    IsWin = true;
                    IsLoss = false;
                    IsGameEnd = true;
                    sfx[5].Play();
                }
                else if ((LFC.friend[0].Hp <= 0 && LFC.friend[1].Hp <= 0 && LFC.friend[2].Hp <= 0 || (Keyboard.GetState().IsKeyDown(Keys.NumPad2))) && !IsLoss && !IsGameEnd)
                {
                    IsLoss = true;
                    IsWin = false;
                    IsGameEnd = true;
                    sfx[6].Play();
                }
            }
            

            ///////////////////////////// Energy /////////////////
            if (Energy > EnergyMax)
            {
                Energy = EnergyMax;     //กันไม่ให้ Energy เกิน Max
            }

            ///////////////////// เลือกตำแหน่งSensei by keyboard ///////////////////////////////////////

            if (IsMyPhase && trigger)
            {
                if (keytak.IsKeyDown(Keys.A) && keypiak.IsKeyUp(Keys.A) && !IsCharge)
                {
                    if (SelPos > 0)
                    {
                        SelPos -= 1;
                    }
                    sfx[0].Play();
                }
                else if (keytak.IsKeyDown(Keys.D) && keypiak.IsKeyUp(Keys.D) && !IsCharge)
                {
                    if (SelPos < 2)
                    {
                        SelPos += 1;
                    }
                    sfx[0].Play();
                }

                if (keytak.IsKeyDown(Keys.A) && keypiak.IsKeyUp(Keys.A) && IsCharge)
                {
                    if (TargetPos > 0)
                    {
                        TargetPos -= 1;
                    }
                    sfx[0].Play();
                }
                else if (keytak.IsKeyDown(Keys.D) && keypiak.IsKeyUp(Keys.D) && IsCharge)
                {
                    if (TargetPos < 2)
                    {
                        TargetPos += 1;
                    }
                    sfx[0].Play();
                }

                if (keytak.IsKeyDown(Keys.Back) && keypiak.IsKeyUp(Keys.Back) && IsCharge)
                {
                    IsSkillEReady = false;
                    IsSkillQReady = false;
                    IsCharge = false;
                    sfx[0].Play();
                }
                for (int i = 0; i < LEC.enemies.Length; i++)
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
                        if (i == SelPos && IsSkillEReady == false && LFC.friend[i].IsCharEnd == false && LFC.friend[i].IsAlive)
                        {
                            LFC.friend[i].skill1_Info();
                            IsSkillEReady = true;
                            IsSkillQReady = false;
                            IsCharge = true;
                            Target_task = LFC.friend[i].Target_1;
                            Caster = i;
                            sfx[1].Play();
                        }
                        else if (i == SelPos && LFC.friend[i].IsCharEnd == false && Energy - LFC.friend[i].Skill1_Cost >= 0 && IsSkillEReady == true)
                        {
                            if(Target_task == "Friend" && LFC.friend[TargetPos].IsAlive)
                            {
                                LFC.friend[i].skill1(TargetPos, Caster);
                                IsSkillEReady = false;
                                IsCharge = false;
                                sfx[2].Play();
                                Delay(3);
                            }
                            else if(Target_task == "AllFriend")
                            {
                                LFC.friend[i].skill1(TargetPos, Caster);
                                IsSkillEReady = false;
                                IsCharge = false;
                                sfx[2].Play();
                                Delay(3);
                            }
                            else if(Target_task == "Self")
                            {
                                LFC.friend[i].skill1(TargetPos, Caster);
                                IsSkillEReady = false;
                                IsCharge = false;
                                sfx[2].Play();
                                Delay(3);
                            }
                            else if(Target_task == "Enemy" && LEC.enemies[TargetPos].IsAlive)
                            {
                                LFC.friend[i].skill1(TargetPos, Caster);
                                IsSkillEReady = false;
                                IsCharge = false;
                                sfx[2].Play();
                                Delay(3);
                            }
                            else if(Target_task == "AllEnemy")
                            {
                                LFC.friend[i].skill1(TargetPos, Caster);
                                IsSkillEReady = false;
                                IsCharge = false;
                                sfx[2].Play();
                                Delay(3);
                            }
                        }
                        else if (i == SelPos &&  (LFC.friend[i].IsCharEnd || !LFC.friend[i].IsAlive))
                        {
                            sfx[4].Play();
                        }
                    }
                    if (keytak.IsKeyDown(Keys.Q) && keypiak.IsKeyUp(Keys.Q))        //สกิล 2 Q
                    {
                        if (i == SelPos && IsSkillQReady == false && LFC.friend[i].IsCharEnd == false && LFC.friend[i].IsAlive)
                        {
                            LFC.friend[i].skill2_Info();
                            IsSkillQReady = true;
                            IsSkillEReady = false;
                            IsCharge = true;
                            Target_task = LFC.friend[i].Target_2;
                            Caster = i;
                            sfx[1].Play();
                        }
                        else if (i == SelPos && LFC.friend[i].IsCharEnd == false && Energy - LFC.friend[i].Skill2_Cost >= 0 && IsSkillQReady == true)
                        {
                            if (Target_task == "Friend" && LFC.friend[TargetPos].IsAlive)
                            {
                                LFC.friend[i].skill2(TargetPos, Caster);
                                IsSkillQReady = false;
                                IsCharge = false;
                                sfx[2].Play();
                                Delay(3);
                            }
                            else if (Target_task == "AllFriend")
                            {
                                LFC.friend[i].skill2(TargetPos, Caster);
                                IsSkillQReady = false;
                                IsCharge = false;
                                sfx[2].Play();
                                Delay(3);
                            }
                            else if (Target_task == "Self")
                            {
                                LFC.friend[i].skill2(TargetPos, Caster);
                                IsSkillQReady = false;
                                IsCharge = false;
                                sfx[2].Play();
                                Delay(3);
                            }
                            else if (Target_task == "Enemy" && LEC.enemies[TargetPos].IsAlive)
                            {
                                LFC.friend[i].skill2(TargetPos, Caster);
                                IsSkillQReady = false;
                                IsCharge = false;
                                sfx[2].Play();
                                Delay(3);
                            }
                            else if (Target_task == "AllEnemy")
                            {
                                LFC.friend[i].skill2(TargetPos, Caster);
                                IsSkillQReady = false;
                                IsCharge = false;
                                sfx[2].Play();
                                Delay(3);
                            }
                        }
                        else if (i == SelPos && (LFC.friend[i].IsCharEnd || !LFC.friend[i].IsAlive))
                        {
                            sfx[4].Play();
                        }
                    }
                }

                ///////////////////// จบ Phase ////////////////////

                if (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter) && IsMyPhase && !StartPhase && !IsLoss && !IsWin)
                {
                    IsMyPhase = false;
                    IsSkillEReady = false;
                    IsSkillQReady = false;
                    IsCharge = false;
                    turn_e = 0;
                }
            }

            ///////////////// Phase ศัตรู ///////////////////
            ///
            if (!MainGame.IsNightMare)
            {
                
            }
            else
            {
                
            }
            
            ///////////////           
            if (StartPhase == true)
            {
                Energy += 4;
                for (int i = 0; i < LFC.friend.Length; i++)
                {
                    LFC.friend[i].IsCharEnd = false;
                }
                StartPhase = false;
            }

            ////////////
            if (IsMyPhase)
            {
                Phase_Status = "Our Phase";
            }
            else if(!IsMyPhase)
            {
                Phase_Status = "Sensei Phase";
            }
            
            ////////////
            
            for (int i = 0; i < LEC.enemies.Length; i++)
            {
                if (LEC.enemies[i].Hp <= 0)
                {
                    LEC.enemies[i].Hp = 0;
                    LEC.enemies[i].IsAlive = false;
                }
            }

            for (int i = 0; i < LFC.friend.Length; i++)
            {
                if (LFC.friend[i].Hp <= 0)
                {
                    LFC.friend[i].Hp = 0;
                    LFC.friend[i].IsAlive = false;
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
            EnterK.UpdateFrame(Elapsed);
            //Console.WriteLine(LFC.friend[Caster].This_Ani[2].Frame);
            //Console.WriteLine(IsWin);
            ScreenFadeIn(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            if(!MainGame.IsNightMare)
            {
                switch (elevel)
                {
                    case Level.Hospital:
                        {
                            theBatch.Draw(BG_hospital, Vector2.Zero, Color.White);
                            break;
                        }
                    case Level.Temple:
                        {
                            theBatch.Draw(BG_temple, Vector2.Zero, Color.White);
                            break;
                        }
                    case Level.Tapae:
                        {
                            theBatch.Draw(BG_tapae, Vector2.Zero, Color.White);
                            break;
                        }
                }
            }
            else
            {
                theBatch.Draw(BG_tapae, Vector2.Zero, Color.White);
            }

            ////////////// วาด UI //////////
            if(IsMyPhase)
            {
                theBatch.DrawString(font48, Phase_Status, new Vector2(1300, 25), Color.CornflowerBlue);
            }
            else if(!IsMyPhase)
            {
                theBatch.DrawString(font48, Phase_Status, new Vector2(1200, 25), Color.Red);
            }

            theBatch.Draw(UI, UIPos, new Rectangle(0, 384, 1696, 288), Color.White);
            theBatch.DrawString(font, Ener + Energy + " / " + EnergyMax, UIPos + new Vector2(25, 25), Color.White);
            theBatch.DrawString(font, check2 + SkillName, UIPos + new Vector2(25, 75), Color.White);
            theBatch.DrawString(font, check + SkillInfo, UIPos + new Vector2(25, 125), Color.White);
            theBatch.DrawString(font, _Hp, HpShow + new Vector2(0, 25), Color.White);
            theBatch.DrawString(font, _Str, StrShow + new Vector2(0, 25), Color.White);
            theBatch.DrawString(font, _Def, DefShow + new Vector2(0, 25), Color.White);

            ///////////วาด เพื่อน/////////////

            for (int i = 0; i < LFC.friend.Length; i++)
            {
                LFC.friend[i].UpdateDraw(theBatch, PlayerPos[i] + LFC.friend[i].AbsPos);              

                if (LFC.friend[i].IsAlive)
                {
                    theBatch.DrawString(font, LFC.friend[i].Name , NameShow + new Vector2(0, 25 + (50 * (i + 1))), Color.White);
                    theBatch.DrawString(font, (int)LFC.friend[i].Hp + " / " + (int)LFC.friend[i].MaxHp, HpShow + new Vector2(0, 25 + (50 * (i + 1))), Color.White);
                    theBatch.DrawString(font, (int)LFC.friend[i].Str + "", StrShow + new Vector2(0, 25 + (50 * (i + 1))), Color.White);
                    theBatch.DrawString(font, (int)LFC.friend[i].Def + "", DefShow + new Vector2(0, 25 + (50 * (i + 1))), Color.White);
                    theBatch.Draw(Healthbar_Bk, PlayerPos[i] + new Vector2(50, 30), new Rectangle(0, 0, 200, 50), Color.White, 0, Vector2.Zero, 0.75f, 0, 0);
                    theBatch.Draw(Healthbar_Fr, PlayerPos[i] + new Vector2(50, 30), new Rectangle(0, 0, (int)((2 * (LFC.friend[i].Hp * 100 / LFC.friend[i].MaxHp))), 50), Color.White, 0, Vector2.Zero, 0.75f, 0, 0);
                }
                else if (!LFC.friend[i].IsAlive)
                {
                    theBatch.DrawString(font, LFC.friend[i].Name, UIPos + new Vector2(650, 25 + (50 * (i + 1))), Color.Gray);
                    theBatch.DrawString(font, (int)LFC.friend[i].Hp + " / " + (int)LFC.friend[i].MaxHp, HpShow + new Vector2(0, 25 + (50 * (i + 1))), Color.Gray);
                    theBatch.DrawString(font, (int)LFC.friend[i].Str + "", StrShow + new Vector2(0, 25 + (50 * (i + 1))), Color.Gray);
                    theBatch.DrawString(font, (int)LFC.friend[i].Def + "", DefShow + new Vector2(0, 25 + (50 * (i + 1))), Color.Gray);
                }
            }

            /////////////วาด จารย์////////////////

            if (!MainGame.IsNightMare)
            {
                for (int i = 0; i < LEC.enemies.Length; i++)
                {
                    LEC.enemies[i].UpdateDraw(theBatch, EnemyPos[i]);
                    if (LEC.enemies[i].IsAlive)
                    {
                        theBatch.Draw(Healthbar_Bk, EnemyPos[i] + new Vector2(-10, -80) + LEC.enemies[i].AbsPos, new Rectangle(0, 0, 200, 50), Color.White, 0, Vector2.Zero, 1.5f, 0, 0);
                        theBatch.Draw(Healthbar_En, EnemyPos[i] + new Vector2(-10, -80) + LEC.enemies[i].AbsPos, new Rectangle(0, 0, (200 * (int)(LEC.enemies[i].Hp * 100 / LEC.enemies[i].MaxHp)) / 100, 50), Color.White, 0, Vector2.Zero, 1.5f, 0, 0);

                        theBatch.DrawString(font, Hpcheck + (int)LEC.enemies[i].Hp + " / DEF : " + LEC.enemies[i].DefReal, new Vector2(EnemyPos[i].X + 25, EnemyPos[i].Y - 60) + LEC.enemies[i].AbsPos, Color.White);
                    }
                }
            }
            else
            {
                LEC.enemies[0].UpdateDraw(theBatch, EnemyPos[0] + new Vector2(-100,-225));
                LEC.enemies[1].UpdateDraw(theBatch, EnemyPos[1]);
                LEC.enemies[2].UpdateDraw(theBatch, EnemyPos[2] + new Vector2(-100, -225));

                theBatch.Draw(Healthbar_Bk, EnemyPos[0] + new Vector2(-100, -225) + new Vector2(-10, -80) + LEC.enemies[0].AbsPos, new Rectangle(0, 0, 200, 50), Color.White, 0, Vector2.Zero, 1.5f, 0, 0);
                theBatch.Draw(Healthbar_En, EnemyPos[0] + new Vector2(-100, -225) + new Vector2(-10, -80) + LEC.enemies[0].AbsPos, new Rectangle(0, 0, (200 * (int)(LEC.enemies[0].Hp * 100 / LEC.enemies[0].MaxHp)) / 100, 50), Color.White, 0, Vector2.Zero, 1.5f, 0, 0);
                theBatch.DrawString(font, Hpcheck + (int)LEC.enemies[0].Hp + " / DEF : " + LEC.enemies[0].DefReal, new Vector2(EnemyPos[0].X + 25, EnemyPos[0].Y - 60) + new Vector2(-100, -225) + LEC.enemies[0].AbsPos, Color.White);

                theBatch.Draw(Healthbar_Bk, EnemyPos[1] + new Vector2(-10, -80) + LEC.enemies[1].AbsPos, new Rectangle(0, 0, 200, 50), Color.White, 0, Vector2.Zero, 1.5f, 0, 0);
                theBatch.Draw(Healthbar_En, EnemyPos[1] + new Vector2(-10, -80) + LEC.enemies[1].AbsPos, new Rectangle(0, 0, (200 * (int)(LEC.enemies[1].Hp * 100 / LEC.enemies[1].MaxHp)) / 100, 50), Color.White, 0, Vector2.Zero, 1.5f, 0, 0);
                theBatch.DrawString(font, Hpcheck + (int)LEC.enemies[1].Hp + " / DEF : " + LEC.enemies[1].DefReal, new Vector2(EnemyPos[1].X + 25, EnemyPos[1].Y - 60) + LEC.enemies[1].AbsPos, Color.White);

                theBatch.Draw(Healthbar_Bk, EnemyPos[2] + new Vector2(-100, -225) + new Vector2(-10, -80) + LEC.enemies[2].AbsPos, new Rectangle(0, 0, 200, 50), Color.White, 0, Vector2.Zero, 1.5f, 0, 0);
                theBatch.Draw(Healthbar_En, EnemyPos[2] + new Vector2(-100, -225) + new Vector2(-10, -80) + LEC.enemies[2].AbsPos, new Rectangle(0, 0, (200 * (int)(LEC.enemies[2].Hp * 100 / LEC.enemies[2].MaxHp)) / 100, 50), Color.White, 0, Vector2.Zero, 1.5f, 0, 0);
                theBatch.DrawString(font, Hpcheck + (int)LEC.enemies[2].Hp + " / DEF : " + LEC.enemies[2].DefReal, new Vector2(EnemyPos[2].X + 25, EnemyPos[2].Y - 60) + new Vector2(-100, -225) + LEC.enemies[2].AbsPos, Color.White);
            }
     
            //////////////วาด ลูกศร//////////
            if(IsSkillQReady || IsSkillEReady)
            {
                if (Target_task == "Enemy")
                {
                    theBatch.Draw(Select_Enemy, new Vector2(EnemyPos[TargetPos].X + 100, EnemyPos[TargetPos].Y - 125) + LEC.enemies[TargetPos].AbsPos, Color.White);
                }
                else if (Target_task == "Friend")
                {
                    theBatch.Draw(Select_Friend, new Vector2(PlayerPos[TargetPos].X + 100, PlayerPos[TargetPos].Y - 75) + LFC.friend[TargetPos].AbsPos, Color.White);
                }
                else if (Target_task == "AllFriend")
                {
                    for(int i = 0; i < LFC.friend.Length; i++)
                    {
                        if (LFC.friend[i].IsAlive)
                        {
                            theBatch.Draw(Select_Friend, new Vector2(PlayerPos[i].X + 100, PlayerPos[i].Y - 75) + LFC.friend[i].AbsPos, Color.White);
                        }
                    }
                }
                else if (Target_task == "Self")
                {
                    theBatch.Draw(Select_Me, new Vector2(PlayerPos[Caster].X + 100, PlayerPos[Caster].Y - 75) + LFC.friend[Caster].AbsPos, Color.Blue);
                }
                else if (Target_task == "AllEnemy")
                {
                    for (int i = 0; i < LEC.enemies.Length; i++)
                    {
                        if (LEC.enemies[i].IsAlive)
                        {
                            theBatch.Draw(Select_Enemy, new Vector2(EnemyPos[i].X + 100, EnemyPos[i].Y - 125) + LEC.enemies[i].AbsPos, Color.White);
                        }
                    }
                }
            }

            theBatch.Draw(Select_Pos, new Vector2(PlayerPos[SelPos].X + 100, PlayerPos[SelPos].Y - 25), Color.Yellow);

            //////////จบเกม/////////
            if (IsWin)
            {
                theBatch.Draw(WinLoss, new Vector2(450, 200), new Rectangle(0, 0, 1080, 256), Color.White);
                EnterK.DrawFrame(theBatch, new Vector2(1150,425));
            }
            else if (IsLoss)
            {
                theBatch.Draw(WinLoss, new Vector2(600, 200), new Rectangle(0, 256, 1080, 256), Color.White);
                EnterK.DrawFrame(theBatch, new Vector2(1150, 425));
            }
            theBatch.Draw(Arrow1, new Vector2(NameShow.X - 150, NameShow.Y - 25 + (50 * (SelPos))), Color.White);
            theBatch.Draw(Tutorial, Vector2.Zero, Color.White);
            theBatch.Draw(ScreenHider, Vector2.Zero, Color.White * ScreenOpa);
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
