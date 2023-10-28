using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Project_Manhattan.Content;
using Project_Manhattan.CoreCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manhattan.Screen_Management
{
    public class Team_Manager_Screen : Screen
    {
        private int Pos = 0;

        bool isDuplicate = false;
        bool isReady = false;

        Texture2D Hotel;
        Texture2D select;

        Vector2[] Player_Pos = new Vector2[3];

        string[] Name = new string[3];
        string[] info = new string[3];
        string[] trait = new string[3];

        SpriteFont font24, font16;
        
        KeyboardState NigKey, OppKey;

        MainGame game;
        public Team_Manager_Screen(MainGame game, EventHandler theScreenEvent) : base(game, theScreenEvent)
        {
            Hotel = game.Content.Load<Texture2D>("2D/BG/SelectA");
            font24 = game.Content.Load<SpriteFont>("Arial24");
            font16 = game.Content.Load<SpriteFont>("File");
            select = game.Content.Load<Texture2D>("2D/UI/SelectChar");

            Player_Pos[0] = new Vector2(300, 375);
            Player_Pos[1] = new Vector2(850, 375);
            Player_Pos[2] = new Vector2(1400, 375);

            for (int i = 0; i < 3; i++)
            {
                Name[i] = "";
                info[i] = "";
                trait[i] = "";
            }

            this.game = game;
        }
        public override void Update(GameTime gameTime)
        {   
            //Console.Clear();
            NigKey = Keyboard.GetState();

            if (NigKey.IsKeyDown(Keys.D) && OppKey.IsKeyUp(Keys.D))
            {
                if (Pos < LFC.friend.Length - 1)
                {
                    Pos++;
                }
                sfx[0].Play();
            }
            
            else if (NigKey.IsKeyDown(Keys.A) && OppKey.IsKeyUp(Keys.A))
            {
                if (Pos > 0)
                {
                    Pos--;
                }
                sfx[0].Play();
            }

            if (NigKey.IsKeyDown(Keys.W) && OppKey.IsKeyUp(Keys.W))
            {
                if (LFC.select[Pos] > 0)
                {
                    LFC.select[Pos]--;
                }
                sfx[1].Play();
            }
            else if (NigKey.IsKeyDown(Keys.S) && OppKey.IsKeyUp(Keys.S))
            {
                if(LFC.select[Pos] < LFC.friendList.Count - 1)
                {
                    LFC.select[Pos]++;
                }
                sfx[1].Play();
            }

            if (LFC.select[0] == LFC.select[1] || LFC.select[0] == LFC.select[2])
            {
                isDuplicate = true;
            }
            else if(LFC.select[1] == LFC.select[0] || LFC.select[1] == LFC.select[2])
            {
                isDuplicate = true;
            }
            else if (LFC.select[2] == LFC.select[0] || LFC.select[2] == LFC.select[1])
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            if (NigKey.IsKeyDown(Keys.Enter) && OppKey.IsKeyUp(Keys.Enter) && !isDuplicate && isReady)
            {
                for(int i = 0; i < 3; i++)
                {
                    LFC.friend[i].Resetto();
                    LEC.enemies[i].Resetto();
                }
                isReady = false;
                MediaPlayer.Play(song[3]);
                ResetFede();
                ScreenEvent.Invoke(game.mGameplay_Screen, new EventArgs());
            }
            else if(NigKey.IsKeyDown(Keys.Enter) && OppKey.IsKeyUp(Keys.Enter) && isDuplicate && isReady)
            {
                sfx[4].Play();
            }

            if (!isReady)
            {
                isReady = true;
            }

            for (int i = 0;i < 3; i++)
            {
                Name[i] = LFC.friend[i].Name;
                info[i] = LFC.friend[i].role;
                trait[i] = LFC.friend[i].ability;
            }

            OppKey = NigKey;
            ScreenFadeIn(gameTime);
            //Console.WriteLine(LFC.select[Pos]);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Hotel, Vector2.Zero,new Rectangle(0, 0, Hotel.Width, Hotel.Height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            for (int i = 0; i < LFC.friend.Length; i++)
            {
                LFC.friend[i].UpdateDraw(spriteBatch, Player_Pos[i] + LFC.friend[i].AbsPos);
                spriteBatch.DrawString(font24, Name[i], Player_Pos[i] + new Vector2(60, 350), Color.White);
                spriteBatch.DrawString(font16, "Role : " + info[i], Player_Pos[i] + new Vector2(30, 450), Color.White);
                spriteBatch.DrawString(font16, "Ability : " + trait[i], Player_Pos[i] + new Vector2(30, 500), Color.White);
            }
            if(!isDuplicate)
            {
                spriteBatch.DrawString(font24, ">>Press Enter to Battle", new Vector2(1050, 1000), Color.White);
            }
            else
            {
                spriteBatch.DrawString(font24, ">>Press Enter to Battle", new Vector2(1050, 1000), Color.Gray);
            }

            spriteBatch.Draw(select, Player_Pos[Pos] + new Vector2(-200, -50), Color.White);

            spriteBatch.Draw(ScreenHider, Vector2.Zero, Color.White * ScreenOpa);

        }
    }
}
