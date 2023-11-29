using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SortingAlgos.UI.UIElements
{
    internal class CheckBox : UIElement
    {
        Color empty;
        Color marked;
        public string text;
        SpriteFont font;
        Button button;
        public bool isMarked = false;

        public CheckBox(int x, int y, int width, int height, string text, SpriteFont font, Color empty, Color marked) : base(new Vector2(x,y), new Vector2(width,height))
        {
            this.empty = empty;
            this.marked = marked;
            this.text = text;
            this.font = font;
            button = new Button(x,y,width,height,"",font, Color.Transparent, Color.Transparent, () =>
            {
                isMarked = !isMarked;
            });
        }

        public override void Draw(Game1 game, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(game.pixelSquare, new Rectangle((int)position.X - 1, (int)position.Y - 1, (int)scale.X + 2, (int)scale.Y + 2), marked);
            spriteBatch.Draw(game.pixelSquare, new Rectangle((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y), isMarked ? marked : empty);
            spriteBatch.DrawString(font, text, new Vector2(position.X + scale.X + 10, position.Y + scale.Y / 2 - font.MeasureString(text).Y / 2), marked);
        }

        public override void Tick()
        {
            if (!isActive)
                return;
            button.Tick();
        }
    }
}
