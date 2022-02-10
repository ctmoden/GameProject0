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
        //FIXME group private fields into regions
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ChopperSprite chopper;
        private CoinSprite[] coins;
        private MissileSprite[] missiles;
        private CloudSprite[] clouds;
        private SpriteFont bangers;
        private Texture2D atlas;
        private Texture2D ball;
        private Texture2D rec;
        private static int coinCount = 0;
        private static int hitCount = 0;

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
            coins = new CoinSprite[]
            {
                new CoinSprite(),
                new CoinSprite(),
                new CoinSprite(),
                new CoinSprite(),
                new CoinSprite()
            };
            missiles = new MissileSprite[]
            {
                //new MissileSprite(new Vector2((float)rand.NextDouble() * Constants.GAME_WIDTH, (float)rand.NextDouble() * Constants.GAME_WIDTH - Constants.GAME_WIDTH)),
                //new MissileSprite(new Vector2((float)rand.NextDouble() * Constants.GAME_WIDTH, (float)rand.NextDouble() * Constants.GAME_WIDTH - Constants.GAME_WIDTH))
                new MissileSprite(),
                new MissileSprite(),
                new MissileSprite(),
                new MissileSprite(),
                new MissileSprite()
            };
            clouds = new CloudSprite[]
            {
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite(),
                new CloudSprite()
            };
            base.Initialize();
        }
        /// <summary>
        /// loads all assets of game
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (var coin in coins) coin.LoadContent(Content);
            foreach (var missile in missiles) missile.LoadContent(Content);
            chopper.LoadContent(Content);
            foreach (var cloud in clouds) cloud.LoadContent(Content);
            bangers = Content.Load<SpriteFont>("bangers");
            ball = Content.Load<Texture2D>("ball");
            rec = Content.Load<Texture2D>("Water32Frames8x4");
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
            chopper.Update(gameTime);
            foreach (var coin in coins)
            {
                if (coin.Bounds.CollidesWith(chopper.Bounds))
                {
                    coin.Collided = true;
                    coinCount++;
                }
                coin.Update(gameTime);
            }
            foreach (var missile in missiles)
            {
                if (missile.Bounds.CollidesWith(chopper.Bounds))
                {
                    hitCount++;
                }
                missile.Update(gameTime);
            }
            foreach (var cloud in clouds) cloud.Update(gameTime);
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
            foreach (var coin in coins)
            {
                #region coin bounding region debugging
                /*
                var rect = new Rectangle((int)(coin.Bounds.Center.X - coin.Bounds.Radius),
                    (int)(coin.Bounds.Center.Y - coin.Bounds.Radius),
                    (int)(2*coin.Bounds.Radius), (int)(2*coin.Bounds.Radius));

                spriteBatch.Draw(ball, rect, Color.White);*/
                #endregion coin bounding region debugging
                coin.Draw(gameTime, spriteBatch);                
            }
            foreach (var missile in missiles)
            {
                #region missile bounding region debugging
                
                var rectM = new Rectangle((int)missile.Bounds.X, (int)missile.Bounds.Y,
                    (int)missile.Bounds.Height, (int)missile.Bounds.Width);
                spriteBatch.Draw(rec, rectM, Color.White);
                #endregion missile bounding region debugging
                missile.Draw(gameTime, spriteBatch);
            }
            foreach (var cloud in clouds)
            {
                cloud.Draw(gameTime, spriteBatch);
            }
            spriteBatch.DrawString(bangers, $"Coins Collected: {coinCount}", new Vector2(10, 10), Color.Gold, 0f, new Vector2(), .5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(bangers, $"Hit Count: {hitCount}", new Vector2(30, 30), Color.Gold,0f,new Vector2(),.5f,SpriteEffects.None,0);
            chopper.Draw(gameTime, spriteBatch);            
            #region chopper bounding region debugging
            
            var rectC = new Rectangle((int)chopper.Bounds.X,(int)chopper.Bounds.Y,
                    (int)chopper.Bounds.Height, (int)chopper.Bounds.Width);
            spriteBatch.Draw(rec, rectC, Color.White);           
            #endregion chopper bounding region debugging
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}