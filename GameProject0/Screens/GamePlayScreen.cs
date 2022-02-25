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

        private ChopperSprite chopper;


        public GamePlayScreen(GameController controller)
        {
            this.controller = controller;
            
        }
        public void Initialize()
        {
            chopper = new ChopperSprite()
            {
                MenuPosition = new Vector2(275, 400),
                Direction = Direction.Right
            };
            //spriteBatch = new SpriteBatch(controller.GraphicsDevice);//FIXME is this right?

        }
        public void LoadContent()
        {
            chopper.LoadContent(controller.Content);//FIXME is this right?
            spriteBatch = new SpriteBatch(controller.GraphicsDevice);//FIXME is this right?
            bangers = controller.Content.Load<SpriteFont>("bangers");
        }

        public void Update(GameTime gameTime, out bool switchScreen)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape) || keyboardState.IsKeyDown(Keys.Q))
            {
                switchScreen = true;             
            }
            else
            {
                switchScreen = false;
            }
            chopper.Update(gameTime);
        }
        public void Draw(GameTime gameTime)
        {
            controller.GraphicsDevice.Clear(Color.CornflowerBlue);//this will draw, but string will not
            spriteBatch.Begin();
            chopper.Draw(gameTime, spriteBatch, true);
            spriteBatch.DrawString(bangers, "Test Screen", new Vector2(250, 250), Color.Black);           
            spriteBatch.End();
        }
    }
}
