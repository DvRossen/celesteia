using System.Diagnostics;
using Celesteia.GUIs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework.Media;
using Celesteia.Graphics;
using MonoGame.Extended.Entities;
using Celesteia.Game.Systems.MainMenu;
using Celesteia.Game.ECS;

namespace Celesteia.Screens {
    public class MainMenuScreen : GameScreen
    {
        private new GameInstance Game => (GameInstance) base.Game;
        public MainMenuScreen(GameInstance game) : base(game) {}

        private MainMenu mainMenu;

        private Song mainMenuTheme;

        private Camera2D Camera;
        private World _world;
        private EntityFactory _entityFactory;

        public override void LoadContent()
        {
            base.LoadContent();

            mainMenuTheme = Game.Content.Load<Song>("music/stargaze_symphony");
            Game.Music.PlayNow(mainMenuTheme);

            Camera = new Camera2D(GraphicsDevice);

            _world = new WorldBuilder()
                .AddSystem(new MainMenuBackgroundSystem())
                .AddSystem(new MainMenuRenderSystem(Camera, Game.SpriteBatch))
                .Build();

            _entityFactory = new EntityFactory(_world, Game);

            _entityFactory.CreateSkyboxPortion("stars", Color.White, -0.1f, .9f);
            _entityFactory.CreateSkyboxPortion("shadow", Color.Black, 5f, .7f);
            _entityFactory.CreateSkyboxPortion("shadow", Color.Black, 3f, .6f);
            _entityFactory.CreateSkyboxPortion("nebula", new Color(165,216,255,45), 3f, .5f);
            _entityFactory.CreateSkyboxPortion("nebula", new Color(255,165,246,45), -2f, .3f);

            this.mainMenu = new MainMenu(Game);
            this.mainMenu.LoadContent(Content);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _world.Draw(gameTime);
            this.mainMenu.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            _world.Update(gameTime);
            this.mainMenu.Update(gameTime, out _);
        }

        public override void Dispose()
        {
            Debug.WriteLine("Unloading MainMenuScreen content...");
            base.UnloadContent();
            Debug.WriteLine("Disposing MainMenuScreen...");
            base.Dispose();
        }
    }
}