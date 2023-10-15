using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manhattan.Screen_Management
{
    public class Titile_Screen : Screen
    {
        Texture2D menuTexture;
        MainGame game;
        public Titile_Screen(MainGame game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            menuTexture = game.Content.Load<Texture2D>("2D/BG/Title");
            this.game = game;
        }
        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                ScreenEvent.Invoke(game.mstory_Hostipal, new EventArgs());
            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(menuTexture, Vector2.Zero, Color.White);
        }
    }
}
