using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SortingAlgos.UI.UIElements
{
    internal class InputBox : UIElement
    {
        Color boxColor;
        Color textColor;
        string placeHolder;
        public string input;
        SpriteFont font;
        KeyboardState keyboardState;
        bool selected = false;
        Button button;

        int tickCount;


        public InputBox(int x, int y, int width,  int height, string placeHolder, SpriteFont font, Color primary, Color secondary) : base(new Vector2(x, y), new Vector2(width, height))
        {
            this.placeHolder = placeHolder;
            boxColor = primary;
            textColor = secondary;
            this.font = font;
            input = "";
            keyboardState = new KeyboardState();
            button = new Button(x, y, width, height, "", font, Color.Transparent, Color.Transparent, () =>
            {
                selected = !selected;
            });
        }

        public override void Draw(Game1 game, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(game.pixelSquare, new Rectangle((int)position.X - 1, (int)position.Y - 1, (int)scale.X + 2, (int)scale.Y + 2), textColor);
            spriteBatch.Draw(game.pixelSquare, new Rectangle((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y), boxColor);
            if (input.Length == 0 && !selected)
                spriteBatch.DrawString(font, placeHolder, position, new Color(textColor.R, textColor.G, textColor.B, 0.1f));
            else
                spriteBatch.DrawString(font, input, position, textColor);
            if (selected)
            {
                if (tickCount >= 60)
                {
                    tickCount = 0;
                }
                if (tickCount >= 30)
                    spriteBatch.Draw(game.pixelSquare, new Rectangle((int)position.X + (int)font.MeasureString(input).X, (int)position.Y, 5, (int)font.MeasureString(" ").Y), textColor);
                tickCount++;
            }
        }

        public override void Tick()
        {
            button.Tick();
            if (!selected)
                return;
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Back) && !keyboardState.IsKeyDown(Keys.Back))
            {
                if (input.Length == 0)
                    return;
                input = input.Remove(input.Length - 1);
            }
            else
            {
                foreach (Keys key in Enum.GetValues(typeof(Keys)))
                {
                    if (ks.IsKeyDown(key) && !keyboardState.IsKeyDown(key))
                    {
                        if (font.Characters.Contains((char)key))
                        {
                            input += (char)key;
                        }
                    }
                }
            }
            keyboardState = ks;
        }
    }
}
