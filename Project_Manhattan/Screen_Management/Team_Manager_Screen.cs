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

            Player_Pos[0] = new Vector2(400, 500);
            Player_Pos[1] = new Vector2(1000, 500);
            Player_Pos[2] = new Vector2(1600, 500);

            Name[0] = "";
            Name[1] = "";
            Name[2] = "";
            this.game = game;
        }
        public override void Update(GameTime gameTime)
        {   
            Console.Clear();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            NigKey = Keyboard.GetState();

            if (NigKey.IsKeyDown(Keys.L) && OppKey.IsKeyUp(Keys.L))
            {
                if (Pos < LFC.PAA.Length - 1)
                {
                    Pos++;
                }
            }
            
            else if (NigKey.IsKeyDown(Keys.J) && OppKey.IsKeyUp(Keys.J))
            {
                if (Pos > 0)
                {
                    Pos--;
                }
            }

            if (NigKey.IsKeyDown(Keys.A) && OppKey.IsKeyUp(Keys.A))
            {
                if (LFC.select[Pos] > 0)
                {
                    LFC.select[Pos]--;
                }
            }
            else if (NigKey.IsKeyDown(Keys.D) && OppKey.IsKeyUp(Keys.D))
            {
                if(LFC.select[Pos] < LFC.PLA.Count - 1)
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

            if (Keyboard.GetState().IsKeyDown(Keys.Left) == true && isDuplicate == false)
            {
                ScreenEvent.Invoke(game.mGameplay_Screen, new EventArgs());
            }

            Name[0] = LFC.PAS[0].Name;
            Name[1] = LFC.PAS[1].Name;
            Name[2] = LFC.PAS[2].Name;

            OppKey = NigKey;
            Console.WriteLine(LFC.select[Pos]);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Hotel, Vector2.Zero,new Rectangle(0, 0, Hotel.Width, Hotel.Height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            for (int i = 0; i < LFC.PAA.Length; i++)
            {
                LFC.PAA[i].DrawFrame(spriteBatch, Player_Pos[i], true);
                spriteBatch.DrawString(font, Name[i], Player_Pos[i] + new Vector2(50, 200), Color.White);
            }

            spriteBatch.Draw(select, Player_Pos[Pos] + new Vector2(50, -75), Color.White);
        }
    }
}
