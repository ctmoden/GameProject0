using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        /*
        private ChopperSprite chopper;
        private CoinSprite[] coins;
        private MissileSprite[] missiles;
        private CloudSprite[] clouds;
        private SpriteFont bangers;
        private Texture2D ball;
        private Texture2D explosion;
        private Texture2D rec;
        private static int coinCount = 0;
        private static int hitCount = 0;
        private string playTime = "";
        */

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
            //load content 
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
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
        /// <summary>
        /// When chopper is hit, display end game message 
        /// and draw explosion sprite over the chopper
        /// stop chopper movements?
        /// </summary>
        private void EndGame()
        {
            
        }
    }
}