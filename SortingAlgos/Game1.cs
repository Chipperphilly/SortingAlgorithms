using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SortingAlgos.Sorting;
using SortingAlgos.Sorting.Algorithms;
using SortingAlgos.UI;
using SortingAlgos.UI.UIElements;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SortingAlgos
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Texture2D pixelSquare;
        Stopwatch sw = new Stopwatch();
        SpriteFont font;
        SpriteFont titleFont;
        Display sortingDisplay;
        Display sortingDisplayOverlay;
        Display menuDisplay;
        int speed = 1;
        bool stepButtonHeld = false;
        Vector2 dimenisions;
        List<string> sortingAlgosNames = new List<string>
        {
            "Quick Sort",
            "Merge Sort",
            "Bubble Sort",
            "Selection Sort",
            "Insertion Sort",
            "Binary Search",
            "Linear Search"
        };

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            /*_graphics.PreferredBackBufferWidth = 2880;
            _graphics.PreferredBackBufferHeight = 1920;*/
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            dimenisions = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            _graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            // TODO: Add your initialization logic here

            pixelSquare = new Texture2D(GraphicsDevice, 1, 1);
            pixelSquare.SetData(new[] { Color.White });

            Random rand = new Random();
            int seed = rand.Next(1000);

            sortingDisplay = new Display(0,0,_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            
            sortingDisplayOverlay = new Display(0,0,_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            sortingDisplayOverlay.Add(new Button((int)dimenisions.X - 280, 0, 280, 100, "Restart", font, Color.Black, Color.White, () =>
            {
                sortingDisplay.elements[0].Reset();
            }));

            // All menu elements
            menuDisplay = new Display(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            menuDisplay.Add(new TextBox(0, 0, (int)dimenisions.X, 80, "Standard Algorithms HSC", titleFont, Color.White, true));
            menuDisplay.Add(new Button((int)dimenisions.X/2 - 200, 1000, 400, 100, "Start The Algorithms", font,  Color.Black, Color.White,() => 
            {
                int count = (int)((Slider)menuDisplay.elements[2]).value;

                List<SortingAlgorithm> algos = new List<SortingAlgorithm>();
                Dictionary<string, CheckBox> data = ((CheckList)menuDisplay.elements[3]).boxStates;

                if (data[sortingAlgosNames[0]].isMarked)
                    algos.Add(new Quicksort(count, seed));
                if (data[sortingAlgosNames[1]].isMarked)
                    algos.Add(new MergeSort(count, 0, seed));
                if (data[sortingAlgosNames[2]].isMarked)
                    algos.Add(new QuickerBubbleSort(count, seed));
                if (data[sortingAlgosNames[3]].isMarked)
                    algos.Add(new SelectionSort(count, seed));
                if (data[sortingAlgosNames[4]].isMarked)
                    algos.Add(new InsertionSort(count, seed));
                if (data[sortingAlgosNames[5]].isMarked)
                    algos.Add(new BinarySearch(count, seed));
                if (data[sortingAlgosNames[6]].isMarked)
                    algos.Add(new LinearSearch(count, seed));

                if (algos.Count == 0)
                    return;

                sortingDisplay.Add(new WindowDisplay(algos.Count, (int)dimenisions.X, (int)dimenisions.Y, font, algos.ToArray()));
                menuDisplay.SetActive(false);
                sortingDisplay.SetActive(true);
                sortingDisplayOverlay.SetActive(true);
            }));
            menuDisplay.Add(new Slider((int)dimenisions.X/2 -200, 900, 400, 15, new Range(10, 1000), 1f, font));
            menuDisplay.Add(new CheckList(10, 10, 50, 50, font, sortingAlgosNames));
            menuDisplay.Add(new TextBox(0, 400, 400, 400, "Space to play algorithms\nRight arrow to speed up\nLeft arrow to slow down\nUp arrow to step", font, Color.White, false));
            menuDisplay.Add(new TextBox(0, 300, 400, 400, "Controls", titleFont, Color.White, false));
            menuDisplay.Add(new TextBox((int)dimenisions.X/2 - (int)font.MeasureString("Number of values").X/2, 840, 400, 400, "Number of values", font, Color.White, false));

            sortingDisplayOverlay.SetActive(false);
            sortingDisplay.SetActive(false);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("File");
            titleFont = Content.Load<SpriteFont>("Title");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                speed++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                speed = Math.Max(speed - 1, 1);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && !stepButtonHeld)
            {
                if (sortingDisplay.isActive)
                {
                    sortingDisplay.Tick();
                }
                stepButtonHeld = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Up)) 
            {
                stepButtonHeld = false;
            }


            if (sortingDisplay.isActive)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    for (int i = 0; i < speed; i++)
                    {
                        sortingDisplay.Tick();
                    }
                }
            }
            if (menuDisplay.isActive)
                menuDisplay.Tick();

            if (sortingDisplayOverlay.isActive)
                sortingDisplayOverlay.Tick();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            sortingDisplay.Draw(this, _spriteBatch);
            menuDisplay.Draw(this, _spriteBatch);
            sortingDisplayOverlay.Draw(this, _spriteBatch);

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        public Vector3 HSVtoRGB(float h, float s, float v)
        {
            float c = v * s;
            float x = c * (1 - Math.Abs(h / 60 % 2 - 1));
            float m = v - c;
            Vector3 rgbp;
            if (h <= 60)
                rgbp = new Vector3(c, x, 0);
            else if (h <= 120)
                rgbp = new Vector3(x, c, 0);
            else if (h <= 180)
                rgbp = new Vector3(0, c, x);
            else if (h <= 240)
                rgbp = new Vector3(0, x, c);
            else if (h <= 300)
                rgbp = new Vector3(x, 0, c);
            else
                rgbp = new Vector3(c, 0, x);

            return new Vector3(rgbp.X + m, rgbp.Y + m, rgbp.Z + m);
        }
    }

    
}