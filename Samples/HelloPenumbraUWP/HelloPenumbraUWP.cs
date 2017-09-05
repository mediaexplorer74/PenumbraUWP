using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using System;

namespace HelloPenumbraUWP
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HelloPenumbraUWP : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Store reference to lighting system.
        PenumbraComponent penumbra;

        // Create sample light source and shadow hull.
        Light light = new PointLight
        {
            Scale = new Vector2(1000f), // Range of the light source (how far the light will travel)
            ShadowType = ShadowType.Solid // Will not lit hulls themselves
        };
        Hull hull = new Hull(new Vector2(1.0f), new Vector2(-1.0f, 1.0f), new Vector2(-1.0f), new Vector2(1.0f, -1.0f))
        {
            Position = new Vector2(400f, 240f),
            Scale = new Vector2(50f)
        };

        public HelloPenumbraUWP()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Create the lighting system and add sample light and hull.
            penumbra = new PenumbraComponent(this);
            penumbra.Lights.Add(light);
            penumbra.Hulls.Add(hull);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Initialize the lighting system.
            penumbra.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Animate light position and hull rotation.
            light.Position =
                new Vector2(400f, 240f) + // Offset origin
                new Vector2( // Position around origin
                    (float)Math.Cos(gameTime.TotalGameTime.TotalSeconds),
                    (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds)) * 240f;
            hull.Rotation = MathHelper.WrapAngle(-(float)gameTime.TotalGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Everything between penumbra.BeginDraw and penumbra.Draw will be
            // lit by the lighting system.
            penumbra.BeginDraw();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw items affected by lighting here ...

            penumbra.Draw(gameTime);

            // Draw items NOT affected by lighting here ... (UI, for example)

            base.Draw(gameTime);
        }
    }
}
