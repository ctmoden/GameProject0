using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using GameProject0;
/*
         when game initialy loads, load title screen
        when enter is hit, load new game controller and 
        unload title screen
        when esc is hit after game is done, unload all game assets
        and reload title screen
        when esc or quit is pressed while on title screen, shut the program down
        use content.unload() to unload screens

        put menu screen logic in here
        other methods needed besides 
         */
namespace GameProject0.Screens
{
    /// <summary>
    /// Class is responsible for loading main menu screen and gameplay screen
    /// Also sends command to revert back to main menu screen when game is over
    /// </summary>
    public class MenuScreen
    {
        
        private GameController controller;
        private ChopperSprite chopper;
        private SpriteBatch spriteBatch;
        private SpriteFont bangers;
        private CloudSprite[] clouds;
        private KeyboardState keyboardState;
        private Tilemap tilemap;
        BlendState blendState;

        /// <summary>
        /// Constructor for menu, sets parent game controller
        /// </summary>
        /// <param name="controller"></param>
        public MenuScreen(GameController controller)
        {
            this.controller = controller;
            tilemap = new Tilemap("map.txt");
            
        }
        /// <summary>
        /// initializes assets used in menu screen
        /// </summary>
        public void Initialize()
        {
            chopper = new ChopperSprite()
            {
                MenuPosition = new Vector2(275, 400),
                Direction = Direction.Right
            };
            clouds = new CloudSprite[]
            {
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite()

            };
            blendState = BlendState.AlphaBlend;
            //spriteBatch = new SpriteBatch(controller.GraphicsDevice);
        }
        public void LoadContent()
        {

            chopper.LoadContent(controller.Content);//FIXME is this right?
            spriteBatch = new SpriteBatch(controller.GraphicsDevice);//FIXME is this right?
            bangers = controller.Content.Load<SpriteFont>("bangers");
            tilemap.LoadContent(controller.Content);
            foreach (var cloud in clouds) cloud.LoadContent(controller.Content);
        }

        public void Update(GameTime gameTime, out bool switchScreen)
        {
            switchScreen = false;
            keyboardState = Keyboard.GetState();
            foreach (var cloud in clouds) cloud.Update(false);
            chopper.Update(gameTime);
            if(keyboardState.IsKeyDown(Keys.Escape) || keyboardState.IsKeyDown(Keys.Q))
            {
                switchScreen = false;
                controller.Exit();
            }
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                switchScreen = true;              
            }
        }
        /// <summary>
        /// Draws menu screen with gameplay instructions
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            controller.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(blendState: blendState);
            tilemap.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(bangers, "Choppa Fight!!", new Vector2(250, 10), Color.Black);
            spriteBatch.DrawString(bangers, "Press esc or q to quit, enter to play", new Vector2(220, 80), Color.Black, 0f, new Vector2(), .50f, SpriteEffects.None, 0);
            spriteBatch.DrawString(bangers, "Press esc or q in game to return to menu screen", new Vector2(170, 130), Color.Black, 0f, new Vector2(), .50f, SpriteEffects.None, 0);
            foreach (var cloud in clouds) cloud.Draw(gameTime, spriteBatch);
            chopper.Draw(gameTime, spriteBatch,true);
            spriteBatch.End();
            /*spriteBatch.Begin(blendState: blendState);
            foreach (var cloud in clouds) cloud.Draw(gameTime, spriteBatch);
            spriteBatch.End();*/
        }

    }
}
