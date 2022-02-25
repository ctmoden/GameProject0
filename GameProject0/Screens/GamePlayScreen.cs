using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
/// <summary>
/// TO-DO: Pass graphics and Content
/// </summary>
namespace GameProject0.Screens
{
    public class GamePlayScreen
    {
        private SpriteBatch spriteBatch;
        private GameController controller;
        private KeyboardState keyboardState;
        private SpriteFont bangers;


        public GamePlayScreen(GameController controller)
        {
            this.controller = controller;
            
        }
        public void Initialize()
        {
            spriteBatch = new SpriteBatch(controller.GraphicsDevice);//FIXME is this right?

        }
        public void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(controller.GraphicsDevice);//FIXME is this right?
            bangers = controller.Content.Load<SpriteFont>("bangers");
        }

        public void Update(GameTime gameTime, out bool switchScreen)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape) || keyboardState.IsKeyDown(Keys.Q))
            {
                switchScreen = true;
                unload();//TODO how to switch back to other screen?  out param?
            }
            else
            {
                switchScreen = false;
            }
        }
        public void Draw(GameTime gameTime)
        {
            controller.GraphicsDevice.Clear(Color.CornflowerBlue);//this will draw, but string will not

            spriteBatch.Begin();
            spriteBatch.DrawString(bangers, "Test Screen", new Vector2(250, 250), Color.Black);
            
            spriteBatch.End();
        }
        /// <summary>
        /// TODO how to unload?
        /// </summary>
        private void unload()
        {
            controller.Content.Unload();
        }

    }
}
