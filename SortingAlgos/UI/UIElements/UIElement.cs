using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgos.UI.UIElements
{
    internal class UIElement
    {
        /*The base of all UI components*/
        public Vector2 position;
        public Vector2 scale;

        // Determines if the object should be drawn to screen or not
        public bool isActive { get; private set; } = true;

        public UIElement(Vector2 position, Vector2 scale)
        {
            this.position = position;
            this.scale = scale;
        }

        // Method to be overriden to draw the element to the screen
        public virtual void Draw(Game1 game, SpriteBatch spriteBatch) { }

        public virtual void Reset() { }
        // Method to be overriden to do all update logic 
        public virtual void Tick() { }

        // Reposition element
        public void Reposition(Vector2 newPos)
        {
            position.X = newPos.X;
            position.Y = newPos.Y;
        }

        // Rescale element
        public void Rescale(Vector2 newScale) 
        {
            scale.X = newScale.X;
            scale.Y = newScale.Y;
        }

        // Set the isActive state
        public void SetActive(bool isActive)
        {
            this.isActive = isActive;
        }
    }
}
