namespace BattleFiled
{
    using System;
    using Interfaces;
    using System.Collections.Generic;

    class Engine
    {
        //int sizeOfField;
        //public Engine(int sizeOfField)
        //{
        //    this.sizeOfField = sizeOfField;
        //}
        public Engine()
        {
        }
  
        /// <summary>
        /// Occurs when an arrow keypress is detected.
        /// </summary>
        private event EventHandler<KeyEventArgs> ArrowKeyPressed;

        private Render renderer;

        private bool keepRunning;
        private bool isRunning;

        /// <summary>
        /// Adds an event handler to the <see cref="E:ArrowKeyPressed"/> event.
        /// </summary>
        /// <param name="handler">The handler to be added</param>
        /// <exception cref="ArgumentNullException">null <paramref name="handler"/> argument.</exception>
        public void AddArrowKeyPressedHandler(EventHandler<KeyEventArgs> handler) 
        {
            if (handler == null) 
            {
                throw new ArgumentNullException("Null 'handler' argument.");
            }

            this.ArrowKeyPressed += handler;
        }

        /// <summary>
        /// Removes an event handler attached to the <see cref="E:ArrowKeyPressed"/> event.
        /// </summary>
        /// <param name="handler">The handler to be removed</param>
        /// <exception cref="ArgumentNullException">null <paramref name="handler"/> argument.</exception>
        public void RemoveArrowKeyPressedHandler(EventHandler<KeyEventArgs> handler) 
        {
            if (handler == null) 
            {
                throw new ArgumentNullException("Null 'handler' argument.");
            }

            this.ArrowKeyPressed -= handler;
        }


        public void Start()
        {
            if (!isRunning)
            {
                this.keepRunning = true;

                this.Run();
            }
        }

        public void Stop() 
        {
            this.keepRunning = false;
        }

        private void Run() 
        {
            if (this.renderer == null)
            {
                this.renderer = new Render();
            }

            int sizeOfField = this.ReadSize();
            Playfield field = InitializeField(sizeOfField);
            List<IGameObject> gameObjects = new List<IGameObject>();
            gameObjects.Add(field);
            this.renderer.DrawGameElements(gameObjects);
            // TODO Render Field, Handle Exceptions...

            while (keepRunning)
            {
                this.isRunning = true;

                //Check if any keys where pressed
                ConsoleKey pressedKey;
                if (Console.KeyAvailable)
                {
                    pressedKey = Console.ReadKey().Key;
                    switch (pressedKey)
                    { 
                        //TODO: Add move key events, maybe for <Enter>
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.DownArrow:
                            KeyEventArgs args = new KeyEventArgs(pressedKey);
                            this.OnArrowKeyPressed(args);
                            break;
                    }
                    
                    //Clear any pending keypresses from the inputstream.
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }
                }
            }
            this.isRunning = false;
        }

        /// <summary>
        /// Raises the <see cref="E:ArrowKeyPressed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BattleFiled.KeyEventArgs" /> instance containing the event data.</param>
        protected virtual void OnArrowKeyPressed(KeyEventArgs e)
        {
            if (this.ArrowKeyPressed != null)
            {
                this.ArrowKeyPressed(this, e);
            }
        }

        private int ReadSize()
        {
            Console.Write("Enter the size of the battle field: n = ");

            int sizeOfField = int.Parse(Console.ReadLine());

            return sizeOfField;
        }

        private Playfield InitializeField(int sizeOfField)
        {
            Playfield field = Playfield.Instance;

            field.SetFieldSize(sizeOfField);
            field.InitializeEmptyField();
            field.PlaceMines();

            return field;
        }
    }
}