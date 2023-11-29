using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SortingAlgos.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgos.UI.UIElements
{
    internal class Window : UIElement
    {
        public Rectangle windowData;
        public SortingAlgorithm algorithm;
        public SpriteFont font;

        public Window(int x, int y, int width, int height, SortingAlgorithm alg, SpriteFont font) : base(new Vector2(x, y), new Vector2(width, height))
        {
            windowData = new Rectangle(x, y, width, height);
            algorithm = alg;
            this.font = font;
        }

        public override void Draw(Game1 game, SpriteBatch spriteBatch)
        {
            if (!isActive)
                return;
            algorithm.Draw(spriteBatch, game, this);
        }

        public override void Tick()
        {
            algorithm.Tick();
        }
    }
}
