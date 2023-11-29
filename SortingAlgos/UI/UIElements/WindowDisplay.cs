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
    internal class WindowDisplay : UIElement
    {
        public Window[] windows;
        int height;
        int width;

        public WindowDisplay(int windowCount, int screenWidth, int screenHeight, SpriteFont font, SortingAlgorithm[] algs) : base(new Vector2(0, 0), new Vector2(screenWidth, screenHeight))
        {
            List<Rectangle> rects;
            switch (windowCount)
            {
                case 1:
                    rects = new List<Rectangle>
                    {
                        new Rectangle(0, 0, screenWidth, screenHeight)
                    };
                    break;
                case 2:
                    rects = new List<Rectangle>
                    {
                        new Rectangle(0, 0, screenWidth, screenHeight / 2),
                        new Rectangle(0, screenHeight / 2, screenWidth, screenHeight / 2)
                    };
                    break;
                case 3:
                    rects = new List<Rectangle>
                    {
                        new Rectangle(0, 0, screenWidth, screenHeight / 3),
                        new Rectangle(0, screenHeight / 3, screenWidth, screenHeight / 3),
                        new Rectangle(0, screenHeight / 3 * 2, screenWidth, screenHeight / 3)
                    };
                    break;
                case 4:
                    rects = new List<Rectangle>
                    {
                        new Rectangle(0, 0, screenWidth / 2, screenHeight / 2),
                        new Rectangle(0, screenHeight / 2, screenWidth / 2, screenHeight / 2),
                        new Rectangle(screenWidth / 2, 0, screenWidth / 2, screenHeight / 2),
                        new Rectangle(screenWidth / 2, screenHeight / 2, screenWidth / 2, screenHeight / 2)
                    };
                    break;
                case 5:
                    rects = new List<Rectangle>
                    {
                        new Rectangle(0, 0, screenWidth, screenHeight / 5),
                        new Rectangle(0, screenHeight / 5 * 1, screenWidth, screenHeight / 5),
                        new Rectangle(0, screenHeight / 5 * 2, screenWidth, screenHeight / 5),
                        new Rectangle(0, screenHeight / 5 * 3, screenWidth, screenHeight / 5),
                        new Rectangle(0, screenHeight / 5 * 4, screenWidth, screenHeight / 5)
                    };
                    break;
                default:
                    rects = new List<Rectangle> { new Rectangle(0, 0, screenWidth, screenHeight) };
                    break;
            }

            windows = new Window[windowCount];
            for (int i = 0; i < windowCount; i++)
            {
                if (i < rects.Count)
                    windows[i] = new Window(rects[i].X, rects[i].Y, rects[i].Width, rects[i].Height, algs[i], font);
            }
            int height = screenHeight;
            int width = screenWidth;
        }

        public override void Reset()
        {
            foreach (Window w in windows)
            {
                w.algorithm.Reset();
            }
        }

        public override void Tick()
        {
            foreach (Window win in windows)
            {
                win.Tick();
            }
        }

        public override void Draw(Game1 game, SpriteBatch spriteBatch)
        {
            if (!isActive) return;
            foreach (Window window in windows)
            {
                window.Draw(game, spriteBatch);
            }
        }
    }
}
