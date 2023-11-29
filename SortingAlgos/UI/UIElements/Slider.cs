using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SortingAlgos.UI.UIElements
{
    internal class Slider : UIElement
    {
        Range range;
        float step;
        bool selected = false;
        public float value;
        SpriteFont font;

        public Slider(int x, int y, int width, int height, Range range, float step, SpriteFont font) : base(new Vector2(x,y), new Vector2(width, height))
        {
            this.range = range;
            this.step = step;
            this.font = font;
        }

        public override void Tick()
        {
            base.Tick();
            MouseState ms = Mouse.GetState();
            if ((selected && ms.LeftButton.HasFlag(ButtonState.Pressed)) || (ms.LeftButton.HasFlag(ButtonState.Pressed) && ms.Position.X < position.X + scale.X && ms.Position.X > position.X && ms.Position.Y < position.Y + scale.Y && ms.Position.Y > position.Y))
            {
                selected = true;
                value = (ms.Position.X - position.X) / scale.X * (range.End.Value - range.Start.Value) + range.Start.Value;
                if (step == 0)
                    return;
                value = (int)(value / step) * step;
            }
            else
            {
                selected = false;
            }
            value = Math.Min(value, range.End.Value);
            value = Math.Max(value, range.Start.Value);
        }

        public override void Draw(Game1 game, SpriteBatch spriteBatch)
        {
            base.Draw(game, spriteBatch);
            spriteBatch.Draw(game.pixelSquare, new Rectangle  
                (
                (int)position.X - 1,
                (int)position.Y - 1,
                (int)scale.X + 2, 
                (int)scale.Y + 2
                ), Color.White);
            spriteBatch.Draw(game.pixelSquare, new Rectangle((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y), Color.Black);
            spriteBatch.Draw(game.pixelSquare, new Rectangle
                (
                (int)(value / (range.End.Value - range.Start.Value) * scale.X + position.X - (scale.Y + 10)/2),
                (int)(position.Y - (scale.Y + 10)/2 + scale.Y/2), 
                (int)scale.Y + 10,
                (int)scale.Y + 10
                ), Color.White);
            spriteBatch.DrawString(font, value.ToString(), new Vector2(position.X + scale.X + scale.Y + 10, position.Y + scale.Y/2 - font.MeasureString(value.ToString()).Y/2), Color.White);
        }
    }
}
