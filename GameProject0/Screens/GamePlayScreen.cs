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
        
        private GameController controller;

        public GamePlayScreen(GameController controller)
        {
            this.controller = controller;
            
        }
        public void Initialize()
        {
            
        }
        public void LoadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

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
