using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        SpriteFont font;
        
        KeyboardState NigKey, OppKey;

        MainGame game;
        public Team_Manager_Screen(MainGame game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            Hotel = game.Content.Load<Texture2D>("2D/BG/Hotel (1)");
            font = game.Content.Load<SpriteFont>("Arial24");
            select = game.Content.Load<Texture2D>("2D/UI/Selecting4");

            Player_Pos[0] = new Vector2(300, 500);
            Player_Pos[1] = new Vector2(900, 500);
            Player_Pos[2] = new Vector2(1500, 500);

            Name[0] = "";
            Name[1] = "";
            Name[2] = "";
            this.game = game;
        }
        public override void Update(GameTime gameTime)
        {   
            Console.Clear();
            NigKey = Keyboard.GetState();

            if (NigKey.IsKeyDown(Keys.D) && OppKey.IsKeyUp(Keys.D))
            {
                if (Pos < LFC.friend.Length - 1)
                {
                    Pos++;
                }
            }
            
            else if (NigKey.IsKeyDown(Keys.A) && OppKey.IsKeyUp(Keys.A))
            {
                if (Pos > 0)
                {
                    Pos--;
                }
            }

            if (NigKey.IsKeyDown(Keys.J) && OppKey.IsKeyUp(Keys.J))
            {
                if (LFC.select[Pos] > 0)
                {
                    LFC.select[Pos]--;
                }
            }
            else if (NigKey.IsKeyDown(Keys.L) && OppKey.IsKeyUp(Keys.L))
            {
                if(LFC.select[Pos] < LFC.friendList.Count - 1)
                {
                    LFC.select[Pos]++;
                }
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
                }
                LEC.enemies[1].Resetto();
                ScreenEvent.Invoke(game.mGameplay_Screen, new EventArgs());
            }

            if (!isReady)
            {
                isReady = true;
            }

            Name[0] = LFC.friend[0].Name;
            Name[1] = LFC.friend[1].Name;
            Name[2] = LFC.friend[2].Name;

            OppKey = NigKey;
            Console.WriteLine(LFC.select[Pos]);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Hotel, Vector2.Zero,new Rectangle(0, 0, Hotel.Width, Hotel.Height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            for (int i = 0; i < LFC.friend.Length; i++)
            {
                LFC.friend[i].UpdateDraw(spriteBatch, Player_Pos[i]);
                spriteBatch.DrawString(font, Name[i], Player_Pos[i] + new Vector2(50, 300), Color.White);
            }

            spriteBatch.Draw(select, Player_Pos[Pos] + new Vector2(100, 0), Color.White);
        }
    }
}
