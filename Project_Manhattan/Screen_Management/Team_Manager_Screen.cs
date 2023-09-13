using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project_Manhattan.Content;
using Project_Manhattan.CoreCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manhattan.Screen_Management
{

    public class Team_Manager_Screen : Screen
    {
        AnimatedTexture Mickuy = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0);
        AnimatedTexture Dan_Heng = new AnimatedTexture(Vector2.Zero, 0, 0.55f, 0);
        AnimatedTexture YoungOhm = new AnimatedTexture(Vector2.Zero, 0, 0.55f, 0);
        AnimatedTexture Tornadome = new AnimatedTexture(Vector2.Zero, 0, 0.55f, 0);

        private int select = 0;

        KeyboardState NigKey, OppKey;

        private List<AnimatedTexture> player_lista = new List<AnimatedTexture>();
        private AnimatedTexture[] player_array = new AnimatedTexture[3];
        MainGame game;
        public Team_Manager_Screen(MainGame game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            Mickuy.Load(game.Content, "Mickey_idle", 9, 1, 4);
            Dan_Heng.Load(game.Content, "heng-stand-animation 128x32", 4, 1, 4);
            YoungOhm.Load(game.Content, "om-stand-animation 132x33", 4, 1, 4);
            Tornadome.Load(game.Content, "dome-stand-animation 128x32", 4, 1, 4);

            player_lista.Add(Dan_Heng); //0
            player_lista.Add(Mickuy);   //1
            player_lista.Add(YoungOhm); //2
            player_lista.Add(Tornadome);//3
            this.game = game;
        }
        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            NigKey = Keyboard.GetState();
            if (NigKey.IsKeyDown(Keys.A) && OppKey.IsKeyUp(Keys.A))
            {
                if (select > 0)
                {
                    select--;
                }
            }
            else if (NigKey.IsKeyDown(Keys.D) && OppKey.IsKeyUp(Keys.D))
            {
                if(select < player_lista.Count - 1)
                {
                    select++;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) == true)
            {
                ScreenEvent.Invoke(game.mGameplay_Screen, new EventArgs());
            }

            OppKey = NigKey;
            Mickuy.UpdateFrame(elapsed);
            Dan_Heng.UpdateFrame(elapsed);
            YoungOhm.UpdateFrame(elapsed);
            Tornadome.UpdateFrame(elapsed);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            player_array[0] = player_lista[select];
            player_array[0].DrawFrame(spriteBatch, new Vector2(1000, 500), true);
        }
    }
}
