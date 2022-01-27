using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject0
{
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

        protected override void Initialize()
        {
            //instantiate animated chopper sprite
            chopper = new ChopperSprite()
            {
                Position = new Vector2(250, 300), Direction = Direction.Right
            };
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //load chopper content
            chopper.LoadContent(Content);
            atlas = Content.Load<Texture2D>("colored_packed");
            bangers = Content.Load<SpriteFont>("bangers");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) 
                || Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();

            // TODO: Add your update logic here
            chopper.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.DrawString(bangers, "Choppa Fight!!", new Vector2(250, 100), Color.Black);
            spriteBatch.DrawString(bangers, "Press esc to quit", new Vector2(250, 200), Color.Black);
            //drawing a cloud
            //FIXME scale to appropriate size
            spriteBatch.Draw(atlas, new Vector2(50, 50), new Rectangle(80, 32, 16, 16), Color.White);
            chopper.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
