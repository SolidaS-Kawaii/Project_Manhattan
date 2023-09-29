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
        private MouseState anutin;                      //Mouse click
        private MouseState chanveerakul;                //Mouse Position

        private Random random = new Random();

        private float Cooldown = 0f;
        float delay = 0;
        bool trigger = true;

        public static bool IsMyPhase = true;
        public static bool StartPhase = true;

        public int SelPos = 1;  //SelectPosition
        public int TargetPos = 1;
        public static int Caster;

        public static int Energy = 0;
        public const int EnergyMax = 6;

        private AnimatedTexture Eniem;
        private AnimatedTexture GeeGee;
        private AnimatedTexture BG_Hotel_UI;

        private const float Rotation = 0.0f;
        private const float Scale = 1.0f;   //ใช้ขยายขนาด obj
        private const float Depth = 1.0f;

        private Texture2D Select;
        private Texture2D Select2;
        private SpriteFont font;

        public static List<Enemy_skill> enemyList = new List<Enemy_skill>();

        private bool IsSkillEReady = false;
        private bool IsSkillQReady = false;

        public Vector2[] PlayerPos = new Vector2[3];   //Player Position
        public Vector2[] EnemyPos = new Vector2[3];      //Enemy Position

        private string Target_task;
        public static string ftest = "Energy : ";         //เอาไว้dubug เฉยๆ
        public static string check = "Position : ";    //เอาไว้dubug เฉยๆ
        public static string check2 = "Skill : ";
        public static string Hpcheck = "Hp : ";
        public static string SkillName;     //เอาไว้dubug เฉยๆ

        MainGame game;

        public Gameplay_Screen(MainGame game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            Eniem = new AnimatedTexture(Vector2.Zero, Rotation, 0.3f, Depth);
            BG_Hotel_UI = new AnimatedTexture(Vector2.Zero, Rotation, 1.0f, Depth);
            GeeGee = new AnimatedTexture(Vector2.Zero, Rotation, 0.5f, Depth);

            Eniem.Load(game.Content, "blackjojo", 1, 1, 0);
            GeeGee.Load(game.Content, "geegee", 1, 1, 0);
            BG_Hotel_UI.Load(game.Content, "BG_Hotel+Ui", 1, 1, 0);
            Select = game.Content.Load<Texture2D>("Selecting");
            Select2 = game.Content.Load<Texture2D>("Selecting2");
            font = game.Content.Load<SpriteFont>("File");

            PlayerPos[0] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width - 800, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);
            PlayerPos[1] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width - 600, MainGame._graphics.GraphicsDevice.Viewport.Height / 2 - 100);
            PlayerPos[2] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width - 400, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);

            EnemyPos[0] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width / 4 - 200, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);
            EnemyPos[1] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width /4, MainGame._graphics.GraphicsDevice.Viewport.Height / 2 - 100);
            EnemyPos[2] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width / 4 + 200, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);

            Sensei sensei = new Sensei(1000, 115);
            MuscleRat muscleRat = new MuscleRat(500, 50);
            MuscleRat muscleRat1 = new MuscleRat(500, 50);

            enemyList.Add(muscleRat);
            enemyList.Add(sensei);
            enemyList.Add(muscleRat1);

            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Visible = true;
            }
            this.game = game;
        }
        public override async void Update(GameTime gameTime)
        {
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float Uptime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            ///////////////////////////// Energy /////////////////
            if (Energy > EnergyMax)
            {
                Energy = EnergyMax;     //กันไม่ให้ Energy เกิน Max
            }

            //////////// เวลา //////////////
            if (Cooldown < delay && !trigger)
                Cooldown += Uptime;
            else if (Cooldown >= delay && !trigger)
            {
                trigger = true;
            }
            ///////////////////////////
            ///
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
                    }
                    else if (keytak.IsKeyDown(Keys.D) && keypiak.IsKeyUp(Keys.D))
                    {
                        if (SelPos < 2)
                        {
                            SelPos += 1;
                        }
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
                            if (i == SelPos && IsSkillEReady == false && LFC.PAS[i].IsCharEnd == false)
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
                                Delay(5);
                            }
                        }
                        if (keytak.IsKeyDown(Keys.Q) && keypiak.IsKeyUp(Keys.Q))        //สกิล 2 Q
                        {
                            if (i == SelPos && IsSkillQReady == false && LFC.PAS[i].IsCharEnd == false)
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
                                Delay(5);
                            }
                        }

                        if (enemyList[TargetPos].Hp <= 0)
                        {
                            enemyList[TargetPos].Visible = false;
                        }
                    }

                    ///////////////////// จบ Phase ////////////////////

                    if (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter) && IsMyPhase)
                    {
                        IsMyPhase = false;
                    }
                }

                ///////////////// Phase ศัตรู ///////////////////
                if (!IsMyPhase && trigger)
                {     
                    for(int i = 0; i < enemyList.Count; i++)
                    {
                        int RandPos = random.Next(0, 3);
                        enemyList[i].skill1(RandPos);
                        trigger = false;
                        await Task.Delay(20);
                        trigger = true;
                    }
                    IsMyPhase = true;
                    StartPhase = true;
                }           

            if (StartPhase == true)
            {
                Energy += 4;
                for (int i = 0; i < LFC.PAS.Length; i++)
                {
                    LFC.PAS[i].IsCharEnd = false;
                }
                StartPhase = false;
            }

            keypiak = keytak; 
            Eniem.UpdateFrame(Elapsed);           
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            BG_Hotel_UI.DrawFrame(_spriteBatch, Vector2.Zero, false);

            LFC.PAA[1].DrawFrame(_spriteBatch, PlayerPos[1], true);
            LFC.PAA[0].DrawFrame(_spriteBatch, PlayerPos[0], true);
            LFC.PAA[2].DrawFrame(_spriteBatch, PlayerPos[2], true);

            Eniem.DrawFrame(_spriteBatch, EnemyPos[1], false);
            GeeGee.DrawFrame(_spriteBatch, EnemyPos[0], false);
            GeeGee.DrawFrame(_spriteBatch, EnemyPos[2], false);

            _spriteBatch.Draw(Select, new Vector2(PlayerPos[SelPos].X + 25, PlayerPos[SelPos].Y - 50), Color.White);
            if (Target_task == "Enemy")
            {
                _spriteBatch.Draw(Select, new Vector2(EnemyPos[TargetPos].X + 25, EnemyPos[TargetPos].Y - 50), Color.White);
            }
            else if (Target_task == "Friend")
            {
                _spriteBatch.Draw(Select2, new Vector2(PlayerPos[TargetPos].X + 25, PlayerPos[TargetPos].Y - 100), Color.White);
            }
            else if (Target_task == "AllFriend")
            {
                _spriteBatch.Draw(Select2, new Vector2(PlayerPos[0].X + 25, PlayerPos[0].Y - 100), Color.White);
                _spriteBatch.Draw(Select2, new Vector2(PlayerPos[1].X + 25, PlayerPos[1].Y - 100), Color.White);
                _spriteBatch.Draw(Select2, new Vector2(PlayerPos[2].X + 25, PlayerPos[2].Y - 100), Color.White);
            }
            else if(Target_task == "Self")
            {
                _spriteBatch.Draw(Select2, new Vector2(PlayerPos[Caster].X + 25, PlayerPos[Caster].Y - 100), Color.White);
            }
            _spriteBatch.DrawString(font, ftest + Energy, new Vector2(50, 800), Color.Cornsilk);
            _spriteBatch.DrawString(font, check + SelPos, new Vector2(50, 900), Color.Cornsilk);
            _spriteBatch.DrawString(font, check2 + SkillName, new Vector2(50, 1000), Color.Cornsilk);
            _spriteBatch.DrawString(font, Hpcheck + enemyList[0].Hp, new Vector2(EnemyPos[0].X + 25, EnemyPos[0].Y - 100), Color.Cornsilk);
            _spriteBatch.DrawString(font, Hpcheck + enemyList[1].Hp, new Vector2(EnemyPos[1].X + 25, EnemyPos[1].Y - 100), Color.Cornsilk);
            _spriteBatch.DrawString(font, Hpcheck + enemyList[2].Hp, new Vector2(EnemyPos[2].X + 25, EnemyPos[2].Y - 100), Color.Cornsilk);

            _spriteBatch.DrawString(font, Hpcheck + LFC.PAS[0].Hp, new Vector2(PlayerPos[0].X + 25, PlayerPos[0].Y - 100), Color.Cornsilk);
            _spriteBatch.DrawString(font, Hpcheck + LFC.PAS[1].Hp, new Vector2(PlayerPos[1].X + 25, PlayerPos[1].Y - 100), Color.Cornsilk);
            _spriteBatch.DrawString(font, Hpcheck + LFC.PAS[2].Hp, new Vector2(PlayerPos[2].X + 25, PlayerPos[2].Y - 100), Color.Cornsilk);
        }

        void Delay(float amountoftime)
        {
            delay = amountoftime;
            Cooldown = 0;
            trigger = false;
        }
    }
}
