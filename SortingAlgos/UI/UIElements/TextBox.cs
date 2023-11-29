using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SortingAlgos.UI.UIElements
{
    internal class TextBox : UIElement
    {
        public string text;
        public Color color;
        bool centered;
        SpriteFont font;

        public TextBox(int x, int y, int width, int height, string text, SpriteFont font, Color color, bool centered) : base(new Vector2(x, y), new Vector2(width, height))
        {
            this.text = text;
            this.color = color;
            this.font = font;
            this.centered = centered;
        }

        public override void Draw(Game1 game, SpriteBatch spriteBatch)
        {
            if (centered)
            {
                spriteBatch.DrawString(font, text, new Vector2(scale.X / 2 - font.MeasureString(text).X / 2 + position.X, scale.Y / 2 - font.MeasureString(text).Y / 2 + position.Y), color);
            }
            else
                spriteBatch.DrawString(font, text, position, color);
        }
    }
}
