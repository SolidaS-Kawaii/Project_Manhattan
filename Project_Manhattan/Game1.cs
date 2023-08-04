using _321_Lab05_3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System.Collections.Generic;

namespace Project_Manhattan
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D Select;
        private SpriteFont font;

        private KeyboardState keytak;
        private KeyboardState keypiak;
        private Vector2[] PlayerPos = new Vector2[3];
        private int SelPos = 1;

        private AnimatedTexture Tester;
        private const float Rotation = 0.0f;
        private const float Scale = 2.0f;   //ใช้ขยายขนาด obj
        private const float Depth = 1.0f;

        private int Energy;
        private int EnergyMax = 6;

        Mickey mickey, heng;

        string ftest = "Bruhhhhhhh";

        List<Player_skill> playerList = new List<Player_skill>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Tester = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
        }

        private const int Frames = 3;
        private const int FramePerSec = 5;
        private const int FrameRow = 1;

        protected override void Initialize()
        {
            PlayerPos[1] = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 4, _graphics.GraphicsDevice.Viewport.Height / 2);
            PlayerPos[0] = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 4 - 200, _graphics.GraphicsDevice.Viewport.Height / 2 + 100);
            PlayerPos[2] = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 4 + 200, _graphics.GraphicsDevice.Viewport.Height / 2 + 100);
            mickey = new Mickey(10,20);
            heng = new Mickey(5,10);
            playerList.Add (mickey);
            playerList.Add (heng);
          
            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach(Player_skill p in playerList)
            {
                p.skill1(); //เรียกใช้สกิลตามตัวละครในlist
            }
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Tester.Load(Content, "CoketumpBreathe", Frames, FrameRow, FramePerSec);
            Select = Content.Load<Texture2D>("Selecting");
            font = Content.Load<SpriteFont>("File");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keytak = Keyboard.GetState();
            if (keytak.IsKeyDown(Keys.A) && keypiak.IsKeyUp(Keys.A))
            {
                if (SelPos > 0)
                {
                    SelPos -= 1;
                }
            }
            if (keytak.IsKeyDown(Keys.D) && keypiak.IsKeyUp(Keys.D)) 
            {
                if(SelPos < 2)
                {
                    SelPos += 1;
                }
            }

            keypiak = keytak;
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Tester.UpdateFrame(Elapsed);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            Tester.DrawFrame(_spriteBatch, PlayerPos[0], true);
            Tester.DrawFrame(_spriteBatch, PlayerPos[1], true);
            Tester.DrawFrame(_spriteBatch, PlayerPos[2], true);
            _spriteBatch.Draw(Select, new Vector2(PlayerPos[SelPos].X + 25, PlayerPos[SelPos].Y - 50), Color.White);
            _spriteBatch.DrawString(font, ftest + SelPos, new Vector2(50,520),Color.Cornsilk);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void SetEnergy()
        {
            Energy = 5;
        }   //กำหนด Energy

        protected int SkillClick()
        {
            return 0;
        }
    }
}