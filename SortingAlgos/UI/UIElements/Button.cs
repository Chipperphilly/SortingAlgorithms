using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SortingAlgos.UI.UIElements
{
    internal class Button : UIElement
    {
        public TextBox textbox;
        public bool clicked;
        Action action;
        Color buttonColor;
        Color textColor;
        SpriteFont font;
        public Button(int x, int y, int width, int height, string text, SpriteFont font, Color primaryColor, Color secondaryColor, Action action) : base(new Vector2(x,y), new Vector2(width, height))
        {
            buttonColor = primaryColor;
            textColor = secondaryColor;
            this.font = font;
            textbox = new TextBox(x, y, width, height, text, font, textColor, true);
            this.action = action;
            clicked = false;
        }

        public override void Draw(Game1 game, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(game.pixelSquare, new Rectangle((int)position.X - 1, (int)position.Y - 1, (int)scale.X + 2, (int)scale.Y + 2), textColor);
            spriteBatch.Draw(game.pixelSquare, new Rectangle((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y), buttonColor);
            textbox.Draw(game, spriteBatch);
        }

        public override void Tick()
        {
            MouseState ms = Mouse.GetState();
            if (!clicked && ms.LeftButton.HasFlag(ButtonState.Pressed) && ms.Position.X >= position.X && ms.Position.X <= position.X + scale.X && ms.Position.Y >= position.Y && ms.Position.Y <= position.Y + scale.Y)
            {
                Press();
                clicked = true;
                return;
            }
            if (!ms.LeftButton.HasFlag(ButtonState.Pressed))
            {
                clicked = false;
            }
        }

        void Press()
        {
            action();
        }
    }
}
