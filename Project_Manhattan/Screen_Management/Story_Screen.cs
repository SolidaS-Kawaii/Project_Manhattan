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
using System.Xml.Linq;

namespace Project_Manhattan.Screen_Management
{
    public class Story_Screen : Screen
    {
        SpriteFont _font;

        int click_count = 0;
        const int speed = 10;

        bool isTitle = false;

        List<string>[] txt_Hospital = new List<string>[2];
        List<string>[] txt_Temple = new List<string>[2];
        List<string>[] txt_Tapae = new List<string>[2];

        string filepath;
        FileStream fileStream;
        StreamReader streamReader;

        KeyboardState NewKey, OldKey;

        bool IsRunning = false;
        bool IsWalkForward = true;
        bool AfterBattle= false;

        Texture2D BG_hospital;
        Texture2D BG_temple;
        Texture2D BG_hotel;
        Texture2D BG_tapae;
        Texture2D BG_tapae_End;
        Texture2D UI_story;
        Texture2D UI_name;
        AnimatedTexture Mickarey;
        AnimatedTexture Mickaidle;
        AnimatedTexture Sensei;

        Doktah doktah;
        Heng heng;
        Dome dome;
        Ohm ohm;
        Shakya shakya;

        Vector2 PlayerPos = new Vector2(5300, 500);
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
        public Story_Screen(MainGame game, EventHandler theScreenEvent) : base(game, theScreenEvent)
        {
            doktah = new Doktah(game);
            heng = new Heng(game);
            dome = new Dome(game);
            ohm = new Ohm(game);
            shakya = new Shakya(game);

            txt_Hospital[0] = new List<string>();
            txt_Hospital[1] = new List<string>();
            txt_Temple[0] = new List<string>();
            txt_Temple[1] = new List<string>();
            txt_Tapae[0] = new List<string>();
            txt_Tapae[1] = new List<string>();

            Mickarey = new AnimatedTexture(Vector2.Zero, 0, 0.5f, 0);
            Mickaidle = new AnimatedTexture(Vector2.Zero, 0, 0.5f, 0);
            Sensei = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0);
            BG_hospital = game.Content.Load<Texture2D>("2D/BG/Hospital_empty");
            BG_temple = game.Content.Load<Texture2D>("2D/BG/TemplePara");
            BG_hotel = game.Content.Load<Texture2D>("2D/BG/Hotel (1)");
            BG_tapae = game.Content.Load<Texture2D>("2D/BG/TapaePara");
            BG_tapae_End = game.Content.Load<Texture2D>("2D/BG/Tapae");

            UI_name = game.Content.Load<Texture2D>("2D/UI/UI1 (1)");
            UI_story = game.Content.Load<Texture2D>("2D/UI/UI1 (1)");

            Sensei.Load(game.Content, "2D/Enemy/Sensei_idle", 4, 1, 4);
            Mickarey.Load(game.Content, "2D/Friend/Mickey/run-animation", 4, 1, 6);
            Mickaidle.Load(game.Content, "2D/Friend/Mickey/Mickey_Stand", 4, 1, 4);

            elevel = Level.Hospital;

            LoadText();

            _font = game.Content.Load<SpriteFont>("Oth/Font/Arial24Real");
            this.game = game;
        }
        public override void Update(GameTime gameTime)
        {
            //Console.Clear();
            NewKey = Keyboard.GetState();
            switch (elevel)
            {
                case Level.Hospital:
                    {
                        HospitalAct();
                        break;
                    }
                case Level.Temple:
                    {
                        TempleAct();
                        break;
                    }
                case Level.Hotel:
                    {
                        HotelAct();
                        break;
                    }
                case Level.Tapae:
                    {
                        TapaeAct();
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
                if (PlayerPos.Y >= game.GraphicsDevice.Viewport.Height/2)
                {

                }
                else if (PlayerPos.Y <= game.GraphicsDevice.Viewport.Height)
                {
                    PlayerPos += new Vector2(0, speed);
                }
            }

            if(PlayerPos.X >= (BG_hospital.Width * 3) - 400)
            {
                PlayerPos = PlayerOldPos;
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
            //Console.WriteLine(click_count);
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            ScreenFadeIn(gameTime);
            Mickaidle.UpdateFrame(Elapsed);
            Mickarey.UpdateFrame(Elapsed);
            doktah.This_Ani[0].UpdateFrame(Elapsed);
            heng.This_Ani[0].UpdateFrame(Elapsed);
            ohm.This_Ani[0].UpdateFrame(Elapsed);
            dome.This_Ani[0].UpdateFrame(Elapsed);
            shakya.This_Ani[0].UpdateFrame(Elapsed);
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
                        if (!AfterBattle)
                        {
                            heng.This_Ani[0].DrawFrame(spriteBatch, new Vector2(5400, 400) - CameraPos, true);
                            doktah.This_Ani[0].DrawFrame(spriteBatch, new Vector2(1200, 300) - CameraPos);
                        }
                        else
                        {
                            heng.This_Ani[0].DrawFrame(spriteBatch, new Vector2(1700, 400) - CameraPos, true);
                            ohm.This_Ani[0].DrawFrame(spriteBatch, new Vector2(1500, 400) - CameraPos);
                        }

                        if (isTitle)
                        {
                            spriteBatch.Draw(UI_story, UI_Pos, new Rectangle(0, 0, 1696, 288), Color.White);
                            spriteBatch.Draw(UI_name, UI_Pos + new Vector2(50, -135), new Rectangle(640, 768, 384, 128), Color.White);
                            spriteBatch.DrawString(_font, txt_Hospital[1][click_count], UI_Pos + new Vector2(25, 25), Color.White);
                            spriteBatch.DrawString(_font, txt_Hospital[0][click_count], UI_Pos + new Vector2(100, -100), Color.White);
                        }
                        break;
                        
                    }
                case Level.Temple:
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            spriteBatch.Draw(BG_temple, (BGPos - CameraPos) + new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width, 0) * i, Color.White);
                        }

                        heng.This_Ani[0].DrawFrame(spriteBatch, new Vector2(5400, 400) - CameraPos, true);
                        shakya.This_Ani[0].DrawFrame(spriteBatch, new Vector2(1200, 300) - CameraPos);

                        if (isTitle)
                        {
                            spriteBatch.Draw(UI_story, UI_Pos, new Rectangle(0, 0, 1696, 288), Color.White);
                            spriteBatch.Draw(UI_name, UI_Pos + new Vector2(50, -135), new Rectangle(640, 768, 384, 128), Color.White);
                            spriteBatch.DrawString(_font, txt_Temple[1][click_count], UI_Pos + new Vector2(25, 25), Color.White);
                            spriteBatch.DrawString(_font, txt_Temple[0][click_count], UI_Pos + new Vector2(100, -100), Color.White);
                        }
                        break;
                    }
                case Level.Hotel:
                    {
                        if(click_count < 10)
                        {
                            spriteBatch.Draw(BG_hotel, Vector2.Zero, Color.White);
                            heng.This_Ani[0].DrawFrame(spriteBatch, new Vector2(1300, 400), true);
                            dome.This_Ani[0].DrawFrame(spriteBatch, new Vector2(1100, 400), false);
                        }

                        if (isTitle)
                        {
                            spriteBatch.Draw(UI_story, UI_Pos, new Rectangle(0, 0, 1696, 288), Color.White);
                            spriteBatch.Draw(UI_name, UI_Pos + new Vector2(50, -135), new Rectangle(640, 768, 384, 128), Color.White);
                            spriteBatch.DrawString(_font, txt_Tapae[0][click_count], UI_Pos + new Vector2(25, 25), Color.White);
                            spriteBatch.DrawString(_font, txt_Tapae[1][click_count], UI_Pos + new Vector2(100, -100), Color.White);
                        }
                        break;
                    }
                case Level.Tapae:
                    {
                        if (!AfterBattle)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                spriteBatch.Draw(BG_tapae, (BGPos - CameraPos) + new Vector2(MainGame._graphics.GraphicsDevice.Viewport.Width, 0) * i, Color.White);
                            }
                            heng.This_Ani[0].DrawFrame(spriteBatch, new Vector2(5400, 400) - CameraPos, true);
                            
                            Sensei.DrawFrame(spriteBatch, new Vector2(1200, 100) - CameraPos);
                            if (isTitle)
                            {
                                spriteBatch.Draw(UI_story, UI_Pos, new Rectangle(0, 0, 1696, 288), Color.White);
                                spriteBatch.Draw(UI_name, UI_Pos + new Vector2(50, -135), new Rectangle(640, 768, 384, 128), Color.White);
                                spriteBatch.DrawString(_font, txt_Tapae[0][click_count], UI_Pos + new Vector2(25, 25), Color.White);
                                spriteBatch.DrawString(_font, txt_Tapae[1][click_count], UI_Pos + new Vector2(100, -100), Color.White);
                            }
                        }
                        else
                        {
                            spriteBatch.Draw(BG_tapae_End, Vector2.Zero, Color.White);
                            heng.This_Ani[0].DrawFrame(spriteBatch, new Vector2(1200, 400), false);
                            ohm.This_Ani[0].DrawFrame(spriteBatch, new Vector2(900, 400), false);
                            dome.This_Ani[0].DrawFrame(spriteBatch, new Vector2(600, 400), false);
                            if (isTitle)
                            {
                                spriteBatch.Draw(UI_story, UI_Pos, new Rectangle(0, 0, 1696, 288), Color.White);
                                spriteBatch.Draw(UI_name, UI_Pos + new Vector2(50, -135), new Rectangle(640, 768, 384, 128), Color.White);
                                spriteBatch.DrawString(_font, txt_Tapae[0][click_count], UI_Pos + new Vector2(25, 25), Color.White);
                                spriteBatch.DrawString(_font, txt_Tapae[1][click_count], UI_Pos + new Vector2(100, -100), Color.White);
                            }
                        }
                        break;
                    }
            }
            
            if(IsRunning ==  false)
            {
                Mickaidle.DrawFrame(spriteBatch, PlayerPos - CameraPos, IsWalkForward);
            }
            else if(IsRunning == true) 
            {
                Mickarey.DrawFrame(spriteBatch, PlayerPos - CameraPos, IsWalkForward);
            }

            spriteBatch.Draw(ScreenHider, Vector2.Zero, Color.White * ScreenOpa);

            base.Draw(spriteBatch);
        }

        private void LoadText()
        {
            ///Hospital
            filepath = Path.Combine(@"Content\Hospital_Speak.txt");
            fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            streamReader = new StreamReader(fileStream);
            while (!streamReader.EndOfStream)
            {
                string tmpStr = streamReader.ReadLine();
                txt_Hospital[0].Add(tmpStr);
            }
            streamReader.Close();
            filepath = Path.Combine(@"Content\Hospital_Story.txt");
            fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            streamReader = new StreamReader(fileStream);
            while (!streamReader.EndOfStream)
            {
                string tmpStr = streamReader.ReadLine();
                txt_Hospital[1].Add(tmpStr);
            }
            streamReader.Close();

            ///Temple
            filepath = Path.Combine(@"Content\Temple_Speak.txt");
            fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            streamReader = new StreamReader(fileStream);
            while (!streamReader.EndOfStream)
            {
                string tmpStr = streamReader.ReadLine();
                txt_Temple[1].Add(tmpStr);
            }
            streamReader.Close();
            filepath = Path.Combine(@"Content\Temple_Story.txt");
            fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            streamReader = new StreamReader(fileStream);
            while (!streamReader.EndOfStream)
            {
                string tmpStr = streamReader.ReadLine();
                txt_Temple[0].Add(tmpStr);
            }
            streamReader.Close();
            ///Hotel+Tapae
            filepath = Path.Combine(@"Content\Final_Speak.txt");
            fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            streamReader = new StreamReader(fileStream);
            while (!streamReader.EndOfStream)
            {
                string tmpStr = streamReader.ReadLine();
                txt_Tapae[1].Add(tmpStr);
            }
            streamReader.Close();
            filepath = Path.Combine(@"Content\Final_Story.txt");
            fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            streamReader = new StreamReader(fileStream);
            while (!streamReader.EndOfStream)
            {
                string tmpStr = streamReader.ReadLine();
                txt_Tapae[0].Add(tmpStr);
            }
            streamReader.Close();

        }
        private void ResetCam()
        {
            PlayerPos = new Vector2(5300, 500);
            CameraPos = new Vector2(3840, 0);
            CameraLeft = new Vector2(5000, 0);
            CameraRight = new Vector2(5450, 0);
        }
        private void HospitalAct()
        {
            if(FadingIsDone)
            {
                isTitle = true;
            }
            if(click_count == 11)
            {
                isTitle = false;
            }
            if (PlayerPos.X <= 1920)
            {
                isTitle = true;
            }

            if (NewKey.IsKeyDown(Keys.Space) && OldKey.IsKeyUp(Keys.Space) && isTitle)
            {
                if (click_count < txt_Hospital[1].Count)
                {
                    click_count++;
                }
                sfx[0].Play();
            }

            if (((NewKey.IsKeyDown(Keys.Up) && OldKey.IsKeyUp(Keys.Up)) || click_count > 34 ) && AfterBattle)
            {
                ResetCam();
                ResetFede();
                isTitle = false;
                elevel = Level.Temple;
                AfterBattle = false;
                click_count = 0;
                ScreenEvent.Invoke(game.mstory_Screen, new EventArgs());
            }
            else if (((NewKey.IsKeyDown(Keys.Up) && OldKey.IsKeyUp(Keys.Up)) || click_count > 27) && !AfterBattle)
            {
                ResetFede();
                MediaPlayer.Play(song[2]);
                AfterBattle = true;
                ScreenEvent.Invoke(game.mTeam_Manage, new EventArgs());
            }

            LEC.enemies[0] = new MuscleRat(game);
            LEC.enemies[1] = new Doktah(game);
            LEC.enemies[2] = new MuscleRat(game);
        }
        private void TempleAct()
        {
            if (FadingIsDone)
            {
                isTitle = true;
            }
            if (click_count == 7)
            {
                isTitle = false;
            }
            if (PlayerPos.X <= 1920)
            {
                isTitle = true;
            }

            if (NewKey.IsKeyDown(Keys.Space) && OldKey.IsKeyUp(Keys.Space) && isTitle)
            {
                if (click_count < txt_Temple[1].Count)
                {
                    click_count++;
                }
                sfx[0].Play();
            }

            if (((NewKey.IsKeyDown(Keys.Up) && OldKey.IsKeyUp(Keys.Up)) || click_count > 15) && !AfterBattle)
            {
                PlayerPos = new Vector2(1460, 500);
                CameraPos = Vector2.Zero;
                ResetFede();
                MediaPlayer.Play(song[2]);
                isTitle = false;
                elevel = Level.Hotel;
                AfterBattle = false;
                click_count = 0;
                ScreenEvent.Invoke(game.mTeam_Manage, new EventArgs());
            }
            LEC.enemies[0] = new Worm(game);
            LEC.enemies[1] = new Shakya(game);
            LEC.enemies[2] = new Worm(game);
        }
        private void HotelAct()
        {
            if (FadingIsDone)
            {
                isTitle = true;
            }

            if (NewKey.IsKeyDown(Keys.Space) && OldKey.IsKeyUp(Keys.Space) && isTitle)
            {
                if (click_count < txt_Tapae[1].Count)
                {
                    click_count++;
                }
                sfx[0].Play();
            }
            if(click_count > 16)
            {
                ResetCam();
                ResetFede();
                isTitle = false;
                elevel = Level.Tapae;
                AfterBattle = false;
                ScreenEvent.Invoke(game.mstory_Screen, new EventArgs());
            }
        }
        private void TapaeAct()
        {
            if (FadingIsDone)
            {
                isTitle = true;
            }
            if (click_count == 22)
            {
                isTitle = false;
            }
            if (PlayerPos.X <= 1920)
            {
                isTitle = true;
            }

            if (NewKey.IsKeyDown(Keys.Space) && OldKey.IsKeyUp(Keys.Space) && isTitle)
            {
                if (click_count < txt_Tapae[1].Count)
                {
                    click_count++;
                }
                sfx[0].Play();
            }
            if (click_count > 23 && !AfterBattle)
            {
                PlayerPos = new Vector2(1200, 500);
                CameraPos = Vector2.Zero;
                ResetFede();
                MediaPlayer.Play(song[2]);
                isTitle = false;
                AfterBattle = true;
                ScreenEvent.Invoke(game.mTeam_Manage, new EventArgs());
            }
            if (click_count > 29 && AfterBattle)
            {
                ResetCam();
                ResetFede();
                isTitle = false;
                AfterBattle = false;
                click_count = 0;
                MediaPlayer.Play(song[0]);
                ScreenEvent.Invoke(game.mTitile_Screen, new EventArgs());
            }
            LEC.enemies[0] = new Bocchi(game);
            LEC.enemies[1] = new Sensei(game);
            LEC.enemies[2] = new Nijika(game);
        }
    }
}