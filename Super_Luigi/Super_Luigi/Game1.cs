using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Super_Luigi.Player;
using Super_Luigi.World;
using Super_Luigi.World.Levels;

namespace Super_Luigi
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Texture2D sprLuigi, sprFireLuigi;
        public static Texture2D sprEnemies;
        public static Texture2D sprParticles;
        public static Texture2D sprBlockTiles;
        public static Texture2D sprItemTiles;
        public static Texture2D sprAuras;

        public static SoundEffect[] luigiSounds = new SoundEffect[8];
        public static SoundEffect[] otherSounds = new SoundEffect[9];
        public static Song[] songs = new Song[3];

        //Fonts
        public static SpriteFont font1;

        HUD hud;
        Level level;
        Luigi luigi;
        Camera camera;

        KeyboardState k1;
        GamePadState g;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;
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

            sprLuigi = Content.Load<Texture2D>("Sprites//Luigi");
            sprFireLuigi = Content.Load<Texture2D>("Sprites//FireLuigi");
            sprEnemies = Content.Load<Texture2D>("Sprites//enemies");
            sprParticles = Content.Load<Texture2D>("Sprites//particles");
            sprBlockTiles = Content.Load<Texture2D>("Sprites//Blocks");
            sprItemTiles = Content.Load<Texture2D>("Sprites//Items");
            sprAuras = Content.Load<Texture2D>("Sprites//auras");

            font1 = Content.Load<SpriteFont>("Fonts//SpriteFont1");

            for (int i = 0; i < luigiSounds.Length; i++)
            {
                luigiSounds[i] = Content.Load<SoundEffect>("Audio//Sounds//Luigi//Luigi" + i);
            }

            for (int i = 0; i < otherSounds.Length; i++)
            {
                otherSounds[i] = Content.Load<SoundEffect>("Audio//Sounds//Blocks//" + i);
            }

            for (int i = 0; i < songs.Length; i++)
            {
                songs[i] = Content.Load<Song>("Audio//Music//music" + i);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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

            if (level == null)
                level = new LevelTest();
            if (camera == null)
                camera = new Camera(GraphicsDevice.Viewport);
            if (luigi == null)
                luigi = new Luigi(new Vector2(32, -100));
            if (hud == null)
                hud = new HUD();

            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(level.MainSong);
                MediaPlayer.IsRepeating = true;
            }

            k1 = Keyboard.GetState();
            g = GamePad.GetState(PlayerIndex.One);

            if (g.IsConnected)
                luigi.input(g);
            else
                luigi.input(k1);

            luigi.update(level);

            camera.Update(luigi, level);

            level.disposeBlocksandItems();
            level.update(luigi);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSkyBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);

            level.Draw(spriteBatch);

            luigi.draw(spriteBatch);

            spriteBatch.End();

            hud.draw(spriteBatch,luigi, GraphicsDevice.Viewport);

            base.Draw(gameTime);
        }
    }
}
