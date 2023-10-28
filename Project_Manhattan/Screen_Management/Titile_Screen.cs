using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Project_Manhattan.CoreCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manhattan.Screen_Management
{
    public class Titile_Screen : Screen
    {
        private KeyboardState keytak;                   //New Key
        private KeyboardState keypiak;                  //Old key

        Texture2D menuTexture;
        Texture2D UI_menu;
        AnimatedTexture SelectUI = new AnimatedTexture(Vector2.Zero, 0, 1, 0);
        Vector2 SelectPos = new Vector2(830, 615);
        int check = 0;
        MainGame game;
        public Titile_Screen(MainGame game, EventHandler theScreenEvent) : base(game ,theScreenEvent)
        {
            menuTexture = game.Content.Load<Texture2D>("2D/BG/MenuVer2");
            UI_menu = game.Content.Load<Texture2D>("2D/UI/UI_menu");
            SelectUI.Load(game.Content, "2D/UI/SelectMenu", 2, 1, 4);
            MediaPlayer.Play(song[0]);
            this.game = game;
        }
        public override void Update(GameTime gameTime)
        {
            keytak = Keyboard.GetState();   //รับคีย์

            if (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter) && check == 0)
            {
                MediaPlayer.Play(song[1]);
                ScreenEvent.Invoke(game.mstory_Screen, new EventArgs());
            }
            else if (keytak.IsKeyDown(Keys.Enter) && keypiak.IsKeyUp(Keys.Enter) && check == 1)
            {
                MainGame.IsNightMare = true;
                MediaPlayer.Play(song[2]);
                ScreenEvent.Invoke(game.mTeam_Manage, new EventArgs());
                LEC.enemies[0] = new Doktah(game);
                LEC.enemies[1] = new Sensei(game);
                LEC.enemies[2] = new Shakya(game);
            }
            if (keytak.IsKeyDown(Keys.W) && keypiak.IsKeyUp(Keys.W))
            {
                check = 0;
                sfx[0].Play();
            }
            else if (keytak.IsKeyDown(Keys.S) && keypiak.IsKeyUp(Keys.S))
            {
                check = 1;
                sfx[0].Play();
            }

            keypiak = keytak;
            float Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            SelectUI.UpdateFrame(Elapsed);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(menuTexture, Vector2.Zero, Color.White);
            spriteBatch.Draw(UI_menu, new Vector2(900, 600), new Rectangle(0, 0, 456, 92), Color.White);
            spriteBatch.Draw(UI_menu, new Vector2(900, 750), new Rectangle(0, 256, 456, 92), Color.White);
            SelectUI.DrawFrame(spriteBatch, SelectPos + new Vector2(0, 150 * check));
        }
    }
}
