using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project_Manhattan.CoreCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manhattan.Screen_Management
{
    public class Story_Hostipal_Screen : Screen
    {

        SpriteFont _font;

        int click_count = 0;

        bool isTitle = false;

        List<string> m_listTexts = new List<string>();

        KeyboardState NewKey, OldKey;

        bool IsRunning = false;
        bool IsWalkForward = true;

        Texture2D BG_hospital;
        AnimatedTexture Mickarey;
        AnimatedTexture Mickaidle;

        Vector2 PlayerPos = new Vector2(5400, 810);
        Vector2 CameraPos = new Vector2(3840, 0);
        Vector2 CameraLeft = new Vector2(5000, 0);
        Vector2 CameraRight = new Vector2(5450, 0);
        Vector2 BGPos = Vector2.Zero;

        MainGame game;
        public Story_Hostipal_Screen(MainGame game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            Mickarey = new AnimatedTexture(Vector2.Zero, 0, 0.5f, 0);
            Mickaidle = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0);
            BG_hospital = game.Content.Load<Texture2D>("BG_Hotel");

            Mickarey.Load(game.Content, "run-animation", 4, 1, 6);
            Mickaidle.Load(game.Content, "Mickey_idle", 9, 1, 9);

            string filepath = Path.Combine(@"Content\Dialog_test.txt");

            FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);

            while (!streamReader.EndOfStream)
            {
                string tmpStr = streamReader.ReadLine();
                m_listTexts.Add(tmpStr);
            }

            streamReader.Close();

            _font = game.Content.Load<SpriteFont>("Arial24");
            this.game = game;
        }
        public override void Update(GameTime gameTime)
        {
            NewKey = Keyboard.GetState();
            const int speed = 10;
            if (Keyboard.GetState().IsKeyDown(Keys.A) && isTitle == false)
            {
                if(PlayerPos.X <= CameraLeft.X)
                {
                    CameraLeft -= new Vector2(speed, 0);
                    CameraRight -= new Vector2(speed, 0);
                    CameraPos -= new Vector2(speed, 0);
                }
                PlayerPos -= new Vector2(speed, 0);
                IsWalkForward = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && isTitle == false)
            {
                if(PlayerPos.X >= CameraRight.X)
                {
                    CameraLeft += new Vector2(speed, 0);
                    CameraRight += new Vector2(speed, 0);
                    CameraPos += new Vector2(speed, 0);
                }
                PlayerPos += new Vector2(speed, 0);
                IsWalkForward = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) && isTitle == false)
            {
                if (PlayerPos.Y <= (game.GraphicsDevice.Viewport.Height/2))
                {

                }
                else if (PlayerPos.Y >= game.GraphicsDevice.Viewport.Height / 2)
                {
                    PlayerPos -= new Vector2(0, speed);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && isTitle == false)
            {
                if (PlayerPos.Y >= game.GraphicsDevice.Viewport.Height-200)
                {

                }
                else if (PlayerPos.Y <= game.GraphicsDevice.Viewport.Height)
                {
                    PlayerPos += new Vector2(0, speed);
                }
            }

            if (PlayerPos.X <= 1920)
            {
                isTitle = true;
            }

            if (NewKey.IsKeyDown(Keys.Enter) && OldKey.IsKeyUp(Keys.Enter) && isTitle)
            {
                if(click_count < m_listTexts.Count - 1)
                {
                    click_count++;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                ScreenEvent.Invoke(game.mTeam_Manage, new EventArgs());
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if(!isTitle)
                {
                    IsRunning = true;
                }
            }
            else
            {
                IsRunning = false;
            }

            OldKey = NewKey;
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Mickaidle.UpdateFrame(Elapsed);
            Mickarey.UpdateFrame(Elapsed);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {           
            for (int i = 0; i < 3; i++)
            {
                spriteBatch.Draw(BG_hospital, (BGPos-CameraPos) + new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width,0)*i, Color.White);
            }
            if(IsRunning ==  false)
            {
                Mickaidle.DrawFrame(spriteBatch, PlayerPos - CameraPos, IsWalkForward);
            }
            else if(IsRunning == true) 
            {
                Mickarey.DrawFrame(spriteBatch, PlayerPos - CameraPos, IsWalkForward);
            }
            if (isTitle)
            {
                spriteBatch.DrawString(_font, m_listTexts[click_count], new Vector2(CameraPos.X, CameraPos.Y + 900), Color.White);
            }
            base.Draw(spriteBatch);
        }
    }
}