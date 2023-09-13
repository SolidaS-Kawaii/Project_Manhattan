using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project_Manhattan.Screen_Management;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;

namespace Project_Manhattan
{
    public class MainGame : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;

        public static KeyboardState keytak;                   //New Key
        public static KeyboardState keypiak;                  //Old key
        public static MouseState anutin;                      //Mouse click
        public static MouseState chanveerakul;                //Mouse Position

        public Titile_Screen mTitile_Screen;
        public Gameplay_Screen mGameplay_Screen;
        public Story_Hostipal_Screen mstory_Hostipal;
        public Team_Manager_Screen mTeam_Manage;
        public Screen mCurrentScreen;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            mGameplay_Screen = new Gameplay_Screen(this, new EventHandler(GameplayScreenEvent));
            mTitile_Screen = new Titile_Screen(this, new EventHandler(GameplayScreenEvent));
            mstory_Hostipal = new Story_Hostipal_Screen(this, new EventHandler(GameplayScreenEvent));
            mTeam_Manage = new Team_Manager_Screen(this, new EventHandler(GameplayScreenEvent));
            mCurrentScreen = mTitile_Screen;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mCurrentScreen.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            mCurrentScreen.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public void GameplayScreenEvent(object obj, EventArgs eventArgs)
        {
            mCurrentScreen = (Screen)obj;
        }
    }
}