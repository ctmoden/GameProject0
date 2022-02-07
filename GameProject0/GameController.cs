using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject0
{
    /// <summary>
    /// Controls game loop
    /// </summary>
    public class GameController : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ChopperSprite chopper;
        private SpriteFont bangers;
        private Texture2D atlas;

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
            /*
            chopper = new ChopperSprite()
            {
                Position = new Vector2(275, 400), Direction = Direction.Right
            };*/
            //for game project 1
            chopper = new ChopperSprite();
            base.Initialize();
        }
        /// <summary>
        /// loads all assets of game
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //load chopper content
            chopper.LoadContent(Content);
            atlas = Content.Load<Texture2D>("colored_packed");
            bangers = Content.Load<SpriteFont>("bangers");
            // TODO: use this.Content to load your game content here
        }
        /// <summary>
        /// updates game as it is rendered, updates animated chopper and looks for exit input
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) 
                || Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();

            // TODO: Add your update logic here
            chopper.Update(gameTime);
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
            spriteBatch.Begin();
            /*
            spriteBatch.DrawString(bangers, "Choppa Fight!!", new Vector2(250, 10), Color.Black);
            spriteBatch.DrawString(bangers, "Press esc or q to quit", new Vector2(270, 80), Color.Black,0f,new Vector2(),.50f,SpriteEffects.None,0);
            */
            //drawing four clouds
            spriteBatch.Draw(atlas, new Vector2(50, 50), new Rectangle(80, 32, 16, 16), Color.White,0f,new Vector2(8,8),8,SpriteEffects.None,0);//, 1, new Vector2(100,100), 100,SpriteEffects.None, 1);
            spriteBatch.Draw(atlas, new Vector2(700, 100), new Rectangle(80, 32, 16, 16), Color.White, 0f, new Vector2(8, 8), 6, SpriteEffects.None, 0);
            spriteBatch.Draw(atlas, new Vector2(200, 223), new Rectangle(80, 32, 16, 16), Color.White, 0f, new Vector2(8, 8), 7, SpriteEffects.None, 0);
            spriteBatch.Draw(atlas, new Vector2(550, 300), new Rectangle(80, 32, 16, 16), Color.White, 0f, new Vector2(8, 8), 10, SpriteEffects.None, 0);
            chopper.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
