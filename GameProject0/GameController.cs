using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Media;
using GameProject0.Screens;
namespace GameProject0
{
    
    /// <summary>
    /// Controls game loop
    /// TODO have an enum for different gameplay screens?  Use case statements to update/initialize
    /// </summary>
    public class GameController : Game
    {
        private GameState gameState = GameState.Menu;//initially load menu
        private MenuScreen menuScreen;
        private GamePlayScreen gameScreen;//TODO send in states to screens so they can load differently?  Pass in game controller to these objects 
        //FIXME group private fields into regions
        private GraphicsDeviceManager graphics;
        
        private SpriteBatch spriteBatch;
        private Song backgroundMusic;
        

        public GameController()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        /// <summary>
        /// initializes assets as needed
        /// </summary>
        protected override void Initialize()
        {
            menuScreen = new MenuScreen(this);
            gameScreen = new GamePlayScreen(this);
            gameScreen.Initialize();
            menuScreen.Initialize();
            
            base.Initialize();
        }
        /// <summary>
        /// loads all assets of game
        /// </summary>
        protected override void LoadContent()
        {
            gameScreen.LoadContent();
            menuScreen.LoadContent();
            backgroundMusic = Content.Load<Song>("Pure Country Gold - Wasted Day");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            MediaPlayer.IsRepeating = true;
        }
        /// <summary>
        /// updates game as it is rendered, updates animated chopper and looks for exit input
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            bool switchScreen = false;
            switch (gameState)
            {             
                case GameState.Menu:
                    MediaPlayer.Play(backgroundMusic);
                    menuScreen.Update(gameTime,out switchScreen);
                    break;
                case GameState.GamePlay:
                    
                    gameScreen.Update(gameTime, out switchScreen);
                    
                    break;
            }
            if (switchScreen)
            {             
                if (gameState == GameState.GamePlay) gameState = GameState.Menu;
                else if (gameState == GameState.Menu) gameState = GameState.GamePlay;
            }           
            base.Update(gameTime);
        }
        /// <summary>
        /// draw sprites in order specified, multiple clouds drawn at different positions and 
        /// sizes
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            //TODO use this in other methods
            spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.Menu:
                    menuScreen.Draw(gameTime);
                    break;
                case GameState.GamePlay:
                    gameScreen.Draw(gameTime);
                    break;
            }            
            spriteBatch.End();
            base.Draw(gameTime);
        }
        
    }
}