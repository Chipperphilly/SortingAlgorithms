using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SortingAlgos.UI.UIElements
{
    internal class CheckList : UIElement
    {
        public Dictionary<string, CheckBox> boxStates;
        public CheckList(int x, int y, int width, int height, SpriteFont font, List<string> names) : base(new Vector2(x,y), new Vector2(width, height))
        {
            boxStates = new Dictionary<string, CheckBox>();
            for (int i = 0; i < names.Count; i++)
            {
                boxStates.Add(names[i], new CheckBox(x,y + 35 * i, 25, 25, names[i], font, Color.Black, Color.White));
            }
        }

        public override void Tick()
        {
            foreach (CheckBox box in boxStates.Values)
            {
                box.Tick();
            }
        }

        public override void Draw(Game1 game, SpriteBatch spriteBatch)
        {
            foreach (CheckBox box in boxStates.Values)
            {
                box.Draw(game, spriteBatch);
            }
        }
    }
}
