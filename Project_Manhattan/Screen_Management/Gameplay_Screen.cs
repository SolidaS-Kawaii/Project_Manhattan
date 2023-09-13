using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project_Manhattan.Content;
using Project_Manhattan.CoreCode;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Project_Manhattan.Screen_Management
{
    public class Gameplay_Screen : Screen
    {
        public static KeyboardState keytak;                   //New Key
        public static KeyboardState keypiak;                  //Old key
        public static MouseState anutin;                      //Mouse click
        public static MouseState chanveerakul;                //Mouse Position

        public static bool IsMyPhase = true;
        public static bool StartPhase = true;

        public int SelPos = 1;  //SelectPosition
        public int EnePos = 1;

        public static int Energy = 0;
        public const int EnergyMax = 6;

        public static AnimatedTexture Mickarey;
        public static AnimatedTexture Dommie;
        public static AnimatedTexture Ohmmer;
        public static AnimatedTexture Eniem;
        public static AnimatedTexture GeeGee;
        public static AnimatedTexture BG_Hotel_UI;

        private const float Rotation = 0.0f;
        private const float Scale = 1.0f;   //ใช้ขยายขนาด obj
        private const float Depth = 1.0f;

        public static Texture2D Select;
        public static SpriteFont font;

        public static List<Player_skill> playerList = new List<Player_skill>();
        public static List<Enemy_skill> enemyList = new List<Enemy_skill>();

        private static bool IsSkillEReady = false;
        private static bool IsSkillQReady = false;

        public Vector2[] PlayerPos = new Vector2[3];   //Player Position
        public Vector2[] EnemyPos = new Vector2[3];      //Enemy Position

        public static string ftest = "Energy : ";         //เอาไว้dubug เฉยๆ
        public static string check = "Position : ";    //เอาไว้dubug เฉยๆ
        public static string check2 = "Skill : ";
        public static string Hpcheck = "Hp : ";
        public static string SkillName;     //เอาไว้dubug เฉยๆ

        MainGame game;

        public Gameplay_Screen(MainGame game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            Mickarey = new AnimatedTexture(Vector2.Zero, Rotation, 1f, Depth);
            Dommie = new AnimatedTexture(Vector2.Zero, Rotation, 0.55f, Depth);
            Ohmmer = new AnimatedTexture(Vector2.Zero, Rotation, 0.55f, Depth);
            Eniem = new AnimatedTexture(Vector2.Zero, Rotation, 0.3f, Depth);
            BG_Hotel_UI = new AnimatedTexture(Vector2.Zero, Rotation, 1.0f, Depth);
            GeeGee = new AnimatedTexture(Vector2.Zero, Rotation, 0.5f, Depth);

            Mickarey.Load(game.Content, "Mickey_idle", 9, 1, 9);
            Dommie.Load(game.Content, "heng", 1, 1, 0);
            Ohmmer.Load(game.Content, "om", 1, 1, 0);
            Eniem.Load(game.Content, "blackjojo", 1, 1, 0);
            GeeGee.Load(game.Content, "geegee", 1, 1, 0);
            BG_Hotel_UI.Load(game.Content, "BG_Hotel+Ui", 1, 1, 0);
            Select = game.Content.Load<Texture2D>("Selecting");
            font = game.Content.Load<SpriteFont>("File");
            Mickey mickey = new Mickey(10, 200, 0, 2);
            Heng heng = new Heng(5, 100, 1, 3);
            Ohm ohm = new Ohm(20, 150, 3, 2);

            PlayerPos[0] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width - 800, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);
            PlayerPos[1] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width - 600, MainGame._graphics.GraphicsDevice.Viewport.Height / 2 - 100);
            PlayerPos[2] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width - 400, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);

            EnemyPos[0] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width / 4 - 200, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);
            EnemyPos[1] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width /4, MainGame._graphics.GraphicsDevice.Viewport.Height / 2 - 100);
            EnemyPos[2] = new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width / 4 + 200, MainGame._graphics.GraphicsDevice.Viewport.Height / 2);
            //////////////////// สร้างตัวละคร parametre เรียงตามนี้ Hp, Str, ค่า Energy Skill1, ค่า Energy Skill2 //////////////////////////
            playerList.Add(mickey);
            playerList.Add(heng);
            playerList.Add(ohm);

            Sensei sensei = new Sensei(1000, 50);
            MuscleRat muscleRat = new MuscleRat(500, 50);

            enemyList.Add(muscleRat);
            enemyList.Add(sensei);
            enemyList.Add(muscleRat);

            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Visible = true;
            }
            this.game = game;
        }
        public override void Update(GameTime gameTime)
        {
            ///////////////////////////// Energy /////////////////
            if (Energy > EnergyMax)
            {
                Energy = EnergyMax;     //กันไม่ให้ Energy เกิน Max
            }

            keytak = Keyboard.GetState();   //รับคีย์
            var anutin = Mouse.GetState();  //รับเมาส์

            ///////////////////// เลือกตำแหน่งSensei by keyboard ///////////////////////////////////////

            if (keytak.IsKeyDown(Keys.A) && keypiak.IsKeyUp(Keys.A))
            {
                if (SelPos > 0)
                {
                    SelPos -= 1;
                }
            }
            if (keytak.IsKeyDown(Keys.D) && keypiak.IsKeyUp(Keys.D))
            {
                if (SelPos < 2)
                {
                    SelPos += 1;
                }
            }

            for (int i = 0; i < enemyList.Count; i++)
            {
                Rectangle EnemyScale = new Rectangle((int)EnemyPos[i].X, (int)EnemyPos[i].Y, 160, 160);
                if (EnemyScale.Contains(anutin.X, anutin.Y) && anutin.LeftButton == ButtonState.Pressed)
                {
                    IsSkillEReady = false;
                    IsSkillQReady = false;
                    EnePos = i;
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
                    else if (i == SelPos && playerList[i].IsCharEnd == false && Energy - playerList[i].Skill1_Cost >= 0 && IsSkillEReady == true)
                    {
                        playerList[i].skill1(EnePos);
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
                    else if (i == SelPos && playerList[i].IsCharEnd == false && Energy - playerList[i].Skill2_Cost >= 0 && IsSkillQReady == true)
                    {
                        playerList[i].skill2(EnePos);
                        playerList[i].IsCharEnd = true;
                        IsSkillQReady = false;
                    }
                }

                if (enemyList[EnePos].Hp <= 0)
                {
                    enemyList[EnePos].Visible = false;
                }
            }

            ///////////////////// จบ Phase ////////////////////

            if (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter))
            {
                IsMyPhase = false;
            }

            if (StartPhase == true)
            {
                Energy += 4;
                for (int i = 0; i < playerList.Count; i++)
                {
                    playerList[i].IsCharEnd = false;
                }
                StartPhase = false;
            }

            keypiak = keytak; 
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Mickarey.UpdateFrame(Elapsed);
            Eniem.UpdateFrame(Elapsed);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            BG_Hotel_UI.DrawFrame(_spriteBatch, Vector2.Zero, false);

            Dommie.DrawFrame(_spriteBatch, PlayerPos[1], true);
            Mickarey.DrawFrame(_spriteBatch, PlayerPos[0], true);
            Ohmmer.DrawFrame(_spriteBatch, PlayerPos[2], true);

            Eniem.DrawFrame(_spriteBatch, EnemyPos[1], false);
            GeeGee.DrawFrame(_spriteBatch, EnemyPos[0], false);
            GeeGee.DrawFrame(_spriteBatch, EnemyPos[2], false);

            _spriteBatch.Draw(Select, new Vector2(PlayerPos[SelPos].X + 25, PlayerPos[SelPos].Y - 50), Color.White);
            _spriteBatch.Draw(Select, new Vector2(EnemyPos[EnePos].X + 25, EnemyPos[EnePos].Y - 50), Color.White);
            _spriteBatch.DrawString(font, ftest + Energy, new Vector2(50, 800), Color.Cornsilk);
            _spriteBatch.DrawString(font, check + SelPos, new Vector2(50, 900), Color.Cornsilk);
            _spriteBatch.DrawString(font, check2 + SkillName, new Vector2(50, 1000), Color.Cornsilk);
            _spriteBatch.DrawString(font, Hpcheck + enemyList[0].Hp, new Vector2(EnemyPos[0].X + 25, EnemyPos[0].Y - 100), Color.Cornsilk);
            _spriteBatch.DrawString(font, Hpcheck + enemyList[1].Hp, new Vector2(EnemyPos[1].X + 25, EnemyPos[1].Y - 100), Color.Cornsilk);
            _spriteBatch.DrawString(font, Hpcheck + enemyList[2].Hp, new Vector2(EnemyPos[2].X + 25, EnemyPos[2].Y - 100), Color.Cornsilk);
        }
    }
}
