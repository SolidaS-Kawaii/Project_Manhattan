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

        KeyboardState NigKey, OppKey;

        MainGame game;
        public Team_Manager_Screen(MainGame game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            this.game = game;
        }
        public override void Update(GameTime gameTime)
        {   
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            NigKey = Keyboard.GetState();

            if (NigKey.IsKeyDown(Keys.L) && OppKey.IsKeyUp(Keys.L))
            {
                if (Pos < LFC.PAA.Length)
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

            OppKey = NigKey;
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            LFC.PAA[0].DrawFrame(spriteBatch, new Vector2(400, 500), true);
            LFC.PAA[1].DrawFrame(spriteBatch, new Vector2(1000, 500), true);
            LFC.PAA[2].DrawFrame(spriteBatch, new Vector2(1600, 500), true);
        }
    }
}
