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
        private GraphicsDeviceManager graphics;
        private ChopperSprite chopper;
        private SpriteBatch spriteBatch;
        private SpriteFont bangers;
        private CloudSprite[] clouds;



        public MenuScreen(GameController controller)
        {
            this.controller = controller;
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
                new CloudSprite()

            };
        }
        public void LoadContent()
        {

            chopper.LoadContent(controller.Content);//FIXME is this right?
            spriteBatch = new SpriteBatch(controller.GraphicsDevice);//FIXME is this right?
            bangers = controller.Content.Load<SpriteFont>("bangers");


        }

        public void Update(GameTime gameTime)
        {
            foreach (var cloud in clouds) cloud.Update(false);
            chopper.Update(gameTime);

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
