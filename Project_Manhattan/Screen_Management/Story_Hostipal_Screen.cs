using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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

        bool IsRunning = false;
        bool IsWalkForward = true;

        Texture2D BG_hospital;
        Texture2D BG_temple;
        Texture2D BG_hotel;
        Texture2D BG_tapae;
        Texture2D UI_story;
        Texture2D UI_name;
        AnimatedTexture Mickarey;
        AnimatedTexture Mickaidle;
        AnimatedTexture Sensei;

        Vector2 PlayerPos = new Vector2(5400, 700);
        Vector2 PlayerOldPos = Vector2.Zero;
        Vector2 CameraPos = new Vector2(3840, 0);
        Vector2 CameraLeft = new Vector2(5000, 0);
        Vector2 CameraRight = new Vector2(5450, 0);
        Vector2 BGPos = Vector2.Zero;
        Vector2 UI_Pos = new Vector2(112, 724);
        Level elevel;

        enum Level
        {
            Hospital,
            Temple,
            Hotel,
            Tapae
        }

        MainGame game;
        public Story_Hostipal_Screen(MainGame game, EventHandler theScreenEvent) : base(game, theScreenEvent)
        {
            Mickarey = new AnimatedTexture(Vector2.Zero, 0, 0.5f, 0);
            Mickaidle = new AnimatedTexture(Vector2.Zero, 0, 0.5f, 0);
            Sensei = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0);
            BG_hospital = game.Content.Load<Texture2D>("2D/BG/Hospital_empty");
            BG_temple = game.Content.Load<Texture2D>("2D/BG/TemplePara");
            BG_hotel = game.Content.Load<Texture2D>("2D/BG/Hotel (1)");
            BG_tapae = game.Content.Load<Texture2D>("2D/BG/TapaePara");

            UI_name = game.Content.Load<Texture2D>("2D/UI/UI1 (1)");
            UI_story = game.Content.Load<Texture2D>("2D/UI/UI1 (1)");

            Sensei.Load(game.Content, "2D/Enemy/Sensei_idle", 4, 1, 4);
            Mickarey.Load(game.Content, "2D/Friend/Mickey/run-animation", 4, 1, 6);
            Mickaidle.Load(game.Content, "2D/Friend/Mickey/Mickey_Stand", 4, 1, 4);

            elevel = Level.Hospital;

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
            switch (elevel)
            {
                case Level.Hospital:
                    {
                        if ((NewKey.IsKeyDown(Keys.Up) && OldKey.IsKeyUp(Keys.Up)) || click_count >= m_listTexts.Count)
                        {
                            ResetCam();
                            ResetFede();
                            elevel = Level.Temple;
                            HospitalAct();
                            MediaPlayer.Play(song[2]);
                            ScreenEvent.Invoke(game.mTeam_Manage, new EventArgs());                        
                        }
                        if (click_count >= 0 && click_count < 2)
                        {
                            Name = "Miki";
                        }
                        else if (click_count >= 2 && click_count < 5)
                        {
                            Name = "???";
                        }
                        else if (click_count >= 5)
                        {
                            Name = "Miki";
                        }
                        break;
                    }
                case Level.Temple:
                    {
                        if ((NewKey.IsKeyDown(Keys.Up) && OldKey.IsKeyUp(Keys.Up)) || click_count >= m_listTexts.Count)
                        {
                            ResetCam();
                            ResetFede();
                            elevel = Level.Hotel;
                            TempleAct();
                            MediaPlayer.Play(song[2]);
                            ScreenEvent.Invoke(game.mTeam_Manage, new EventArgs());
                        }
                        break;
                    }
                case Level.Hotel:
                    {
                        if ((NewKey.IsKeyDown(Keys.Up) && OldKey.IsKeyUp(Keys.Up)) || click_count >= m_listTexts.Count)
                        {
                            ResetCam();
                            ResetFede();
                            elevel = Level.Tapae;
                            ScreenEvent.Invoke(game.mstory_Hostipal, new EventArgs());
                        }
                        break;
                    }
                case Level.Tapae:
                    {
                        if ((NewKey.IsKeyDown(Keys.Up) && OldKey.IsKeyUp(Keys.Up)) || click_count >= m_listTexts.Count)
                        {
                            ResetCam();
                            ResetFede();
                            elevel = Level.Hospital;
                            TapaeAct();
                            MediaPlayer.Play(song[2]);
                            ScreenEvent.Invoke(game.mTeam_Manage, new EventArgs());
                        }
                        break;
                    }
            }
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

            if(PlayerPos.X >= (BG_hospital.Width * 3) - 150)
            {
                PlayerPos = PlayerOldPos;
            }

            if (PlayerPos.X <= 1920)
            {
                isTitle = true;
            }
            else if(PlayerPos.X > 1920)
            {
                isTitle = false;
            }

            if (NewKey.IsKeyDown(Keys.Enter) && OldKey.IsKeyUp(Keys.Enter) && isTitle)
            {
                if (click_count < m_listTexts.Count - 1)
                {
                    click_count++;
                }
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                elevel = Level.Temple;
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
            PlayerOldPos = PlayerPos;
            Console.WriteLine(click_count);
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            ScreenFadeIn(gameTime);
            Mickaidle.UpdateFrame(Elapsed);
            Mickarey.UpdateFrame(Elapsed);
            Sensei.UpdateFrame(Elapsed);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {           
            switch (elevel)
            {
                case Level.Hospital:
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            spriteBatch.Draw(BG_hospital, (BGPos - CameraPos) + new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width, 0) * i, Color.White);
                        }
                        break;
                    }
                case Level.Temple:
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            spriteBatch.Draw(BG_temple, (BGPos - CameraPos) + new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width, 0) * i, Color.White);
                        }
                        break;
                    }
                case Level.Hotel:
                    {
                        spriteBatch.Draw(BG_hotel, Vector2.Zero, Color.White);                     
                        break;
                    }
                case Level.Tapae:
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            spriteBatch.Draw(BG_tapae, (BGPos - CameraPos) + new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width, 0) * i, Color.White);
                        }
                        break;
                    }
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

            spriteBatch.Draw(ScreenHider, Vector2.Zero, Color.White * ScreenOpa);

            base.Draw(spriteBatch);
        }
        private void ResetCam()
        {
            PlayerPos = new Vector2(5400, 700);
            CameraPos = new Vector2(3840, 0);
            CameraLeft = new Vector2(5000, 0);
            CameraRight = new Vector2(5450, 0);
        }
        private void HospitalAct()
        {
            LEC.enemies[0] = new MuscleRat(game);
            LEC.enemies[1] = new Doktah(game);
            LEC.enemies[2] = new MuscleRat(game);
        }
        private void TempleAct()
        {
            LEC.enemies[0] = new Worm(game);
            LEC.enemies[1] = new Shakya(game);
            LEC.enemies[2] = new Worm(game);
        }
        private void TapaeAct()
        {
            LEC.enemies[0] = new Bocchi(game);
            LEC.enemies[1] = new Sensei(game);
            LEC.enemies[2] = new Nijika(game);
        }
    }
}