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
            
        }
        public void LoadContent()
        {
            bangers = controller.Content.Load<SpriteFont>("bangers");
            spriteBatch = new SpriteBatch(controller.GraphicsDevice);//FIXME is this right?

        }

        public void Update(GameTime gameTime, out bool switchScreen)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape) || keyboardState.IsKeyDown(Keys.Q))
            {
                switchScreen = true;
                Unload();//TODO how to switch back to other screen?  out param?
            }
            else
            {
                switchScreen = false;
            }
        }
        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(bangers, "Test Screen", new Vector2(250, 10), Color.Black);
            spriteBatch.End();
        }
        /// <summary>
        /// TODO how to unload?
        /// </summary>
        public void Unload()
        {
            controller.Content.Unload();
        }

    }
}
