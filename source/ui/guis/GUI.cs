using System.Diagnostics;
using Celesteia.UI;
using Celesteia.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Celesteia.GUIs {
    public class GUI {
        public GameInstance Game;

        public IContainer Root;

        public GUI(GameInstance Game, Rect rect) {
            this.Game = Game;
            this.Root = new Container(rect);
        }

        public virtual void LoadContent(ContentManager Content) {
            Debug.WriteLine("Loaded GUI.");
        }

        public virtual void Update(GameTime gameTime, out bool clickedAnything) {
            Root.Update(gameTime, out clickedAnything);
        }

        // Draw all elements.
        public virtual void Draw(GameTime gameTime) {
            
            Game.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointWrap, null, null, null);

            if (UIReferences.GUIEnabled) Root.Draw(Game.SpriteBatch);

            Game.SpriteBatch.End();
        }

        // If the menu is referred to as a boolean, return whether it is non-null (true) or null (false).
        public static implicit operator bool(GUI gui)
        {
            return !object.ReferenceEquals(gui, null);
        }
    }
}