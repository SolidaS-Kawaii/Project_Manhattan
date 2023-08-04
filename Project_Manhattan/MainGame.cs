using _321_Lab05_3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;

namespace Project_Manhattan
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D Select;
        private SpriteFont font;

        public static KeyboardState keytak;                   //New Key
        public static KeyboardState keypiak;                  //Old key
        public static MouseState anutin;                      //Mouse click
        public static MouseState chanveerakul;                //Mouse Position

        private AnimatedTexture Tester;
        private AnimatedTexture Tester2;

        private const float Rotation = 0.0f;
        private const float Scale = 1.0f;   //ใช้ขยายขนาด obj
        private const float Depth = 1.0f;

        public static int Energy = 0;
        public const int EnergyMax = 6;

        public static Vector2[] PlayerPos = new Vector2[3];   //Player Position
        public static Vector2[] EnemyPos = new Vector2[3];      //Enemy Position

        public static bool IsMyPhase = true;
        public static bool StartPhase = true;

        string ftest = "Energy : ";         //เอาไว้dubug เฉยๆ
        string check = "Position : ";    //เอาไว้dubug เฉยๆ
        string check2 = "Skill : ";
        public static string SkillName;     //เอาไว้dubug เฉยๆ

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Tester = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
            Tester2 = new AnimatedTexture(Vector2.Zero, Rotation, 2.5f, Depth);
        }

        private const int Frames = 2;
        private const int FramePerSec = 4;
        private const int FrameRow = 1;

        protected override void Initialize()
        {
            PlayerPos[0] = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 4 - 200, _graphics.GraphicsDevice.Viewport.Height / 2 + 100);
            PlayerPos[1] = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 4, _graphics.GraphicsDevice.Viewport.Height / 2);
            PlayerPos[2] = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 4 + 200, _graphics.GraphicsDevice.Viewport.Height / 2 + 100);

            EnemyPos[0] = new Vector2(_graphics.GraphicsDevice.Viewport.Width - 800, _graphics.GraphicsDevice.Viewport.Height / 2 + 100);
            EnemyPos[1] = new Vector2(_graphics.GraphicsDevice.Viewport.Width - 600, _graphics.GraphicsDevice.Viewport.Height / 2);
            EnemyPos[2] = new Vector2(_graphics.GraphicsDevice.Viewport.Width - 400, _graphics.GraphicsDevice.Viewport.Height / 2 + 100);

            Gameplay.LoadPlayer();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Tester.Load(Content, "Mickey-Idle", Frames, FrameRow, FramePerSec);
            Tester2.Load(Content, "CoketumpBreathe", 3, 1, 4);
            Select = Content.Load<Texture2D>("Selecting");
            font = Content.Load<SpriteFont>("File");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            if(IsMyPhase == true)       //Phase อ้าย
            {
                Gameplay.GetEnergy();
                Gameplay.UpdateGameplay(gameTime);
            }

            if(IsMyPhase == false)      //Phase ศัตรู รอเขียนเพิ่ม
            {
                IsMyPhase = true;
                StartPhase = true;
            }

            keypiak = keytak;
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Tester.UpdateFrame(Elapsed);
            Tester2.UpdateFrame(Elapsed);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            for(int i = 0; i < 3; i++) 
            {
                Tester.DrawFrame(_spriteBatch, PlayerPos[i], true);
                Tester2.DrawFrame(_spriteBatch, EnemyPos[i], false);
            }
            _spriteBatch.Draw(Select, new Vector2(PlayerPos[Gameplay.SelPos].X + 25,  PlayerPos[Gameplay.SelPos].Y - 50), Color.White);
            _spriteBatch.Draw(Select, new Vector2(EnemyPos[Gameplay.EnePos].X + 25, EnemyPos[Gameplay.EnePos].Y - 50), Color.White);
            _spriteBatch.DrawString(font, ftest + Energy, new Vector2(50,200),Color.Cornsilk);
            _spriteBatch.DrawString(font, check + Gameplay.SelPos, new Vector2(50, 300), Color.Cornsilk);
            _spriteBatch.DrawString(font, check2 + Gameplay.SkillName, new Vector2(50, 400), Color.Cornsilk);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}