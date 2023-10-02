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
        const int speed = 10;

        bool isTitle = false;

        List<string> m_listTexts = new List<string>();
        string Name;

        KeyboardState NewKey, OldKey;

        bool IsStart = true;
        bool IsRunning = false;
        bool IsWalkForward = true;

        Texture2D BG_hospital;
        Texture2D UI_story;
        Texture2D UI_name;
        AnimatedTexture Mickarey;
        AnimatedTexture Mickaidle;
        AnimatedTexture Sensei;

        Vector2 PlayerPos = new Vector2(5400, 810);
        Vector2 CameraPos = new Vector2(3840, 0);
        Vector2 CameraLeft = new Vector2(5000, 0);
        Vector2 CameraRight = new Vector2(5450, 0);
        Vector2 BGPos = Vector2.Zero;
        Vector2 UI_Pos = new Vector2(112, 724);

        MainGame game;
        public Story_Hostipal_Screen(MainGame game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            Mickarey = new AnimatedTexture(Vector2.Zero, 0, 0.5f, 0);
            Mickaidle = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0);
            Sensei = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0);
            BG_hospital = game.Content.Load<Texture2D>("2D/BG/Hospital_empty");
            UI_name = game.Content.Load<Texture2D>("2D/UI/UI1 (1)");
            UI_story = game.Content.Load<Texture2D>("2D/UI/UI1 (1)");

            Sensei.Load(game.Content, "2D/Enemy/Sensei_idle", 4, 1, 4);
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
            Console.Clear();
            NewKey = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.A) && isTitle == false)
            {
                if (PlayerPos.X <= CameraLeft.X)
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
                if (PlayerPos.X >= CameraRight.X)
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
                if (PlayerPos.Y <= 300)
                {

                }
                else if (PlayerPos.Y >= 300)
                {
                    PlayerPos -= new Vector2(0, speed);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && isTitle == false)
            {
                if (PlayerPos.Y >= game.GraphicsDevice.Viewport.Height - 200)
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
                if (click_count < m_listTexts.Count)
                {
                    click_count++;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) || click_count >= m_listTexts.Count)
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

            if(click_count >= 0 && click_count < 2)
            {
                Name = "Miki";
            }
            else if(click_count >= 2 && click_count < 5)
            {
                Name = "???";
            }
            else if(click_count >= 5)
            {
                Name = "Miki";
            }

            OldKey = NewKey;
            Console.WriteLine(click_count);
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Mickaidle.UpdateFrame(Elapsed);
            Mickarey.UpdateFrame(Elapsed);
            Sensei.UpdateFrame(Elapsed);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {           
            for (int i = 0; i < 3; i++)
            {
                spriteBatch.Draw(BG_hospital, (BGPos-CameraPos) + new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width,0)*i, Color.White);
            }

            Sensei.DrawFrame(spriteBatch, new Vector2(1200, 300) - CameraPos);

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
                spriteBatch.Draw(UI_story, UI_Pos, new Rectangle(0, 0, 1696, 288), Color.White);
                spriteBatch.Draw(UI_name, UI_Pos + new Vector2(50, -135), new Rectangle(640, 768, 384, 128), Color.White);
                spriteBatch.DrawString(_font, m_listTexts[click_count], UI_Pos + new Vector2(25, 25), Color.White);
                spriteBatch.DrawString(_font, Name, UI_Pos + new Vector2(100, -100), Color.White);
            }
            base.Draw(spriteBatch);
        }
    }
}