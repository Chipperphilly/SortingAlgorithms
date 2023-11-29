using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SortingAlgos.UI.UIElements
{
    internal class Display : UIElement
    {
        public List<UIElement> elements;

        public Display(int x, int y, int width, int height) : base(new Vector2(x, y), new Vector2(width, height))
        {
            elements = new List<UIElement>();
        }

        // Add an UIElement to the display
        public void Add(UIElement element)
        {
            elements.Add(element);
        }

        // Draw all elements to screen
        public override void Draw(Game1 game, SpriteBatch spriteBatch)
        {
            if (!isActive)
                return;
            foreach (UIElement element in elements)
            {
                element.Draw(game, spriteBatch);
            }
        }

        // Tick each element
        public override void Tick()
        {
            foreach (UIElement element in elements)
            {
                element.Tick();
            }
        }
    }
}
