using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manhattan.Screen_Management
{
    public class Screen
    {
        protected float ScreenOpa = 1;
        protected Texture2D ScreenHider;
        Color[] CData = new Color[1920 * 1080];
        protected float TimeProg = 0;
        protected bool FadingIsDone = false;
        protected EventHandler ScreenEvent;

        protected Song[] song = new Song[4];
        public Screen(Game game1,EventHandler theScreenEvent) 
        { 
            ScreenEvent = theScreenEvent;
            ScreenHider = new Texture2D(game1.GraphicsDevice, 1920, 1080);
            for (int i = 0; i < CData.Length; i++)
            {
                CData[i] = Color.Black;
            }
            ScreenHider.SetData(CData);

            this.song[0] = game1.Content.Load<Song>("Oth/Song/menu");
            this.song[1] = game1.Content.Load<Song>("Oth/Song/story");
            this.song[2] = game1.Content.Load<Song>("Oth/Song/pickchar");
            this.song[3] = game1.Content.Load<Song>("Oth/Song/gameplay");
            MediaPlayer.Volume = 0.05f;
            MediaPlayer.IsRepeating = true;
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
        public virtual void ScreenFadeIn(GameTime time)
        {
            TimeProg += (float)time.ElapsedGameTime.TotalSeconds;
            if (ScreenOpa > 0)
            {
                if (TimeProg > 0.02)
                {
                    ScreenOpa -= (float)0.1;
                    TimeProg = 0;
                }
                //IsPaused = true;
            }
            else
            {
                ScreenOpa = 0;
                //IsPaused = false;
                FadingIsDone = true;
            }
        }
        public virtual void ResetFede()
        {
            ScreenOpa = 1;
            FadingIsDone = false;
        }
    }
}
