namespace BattleFiled.GameEngine
{
    using System;
    using BattleFiled;
    using BattleFiled.Cells;
    using BattleFiled.Renderer;
    using BattleFiled.Sounds;

    class Engine
    {
        private static Engine instance;
        
        public static Engine Instance
        {
            get
            { 
                if(Engine.instance == null)
                {
                    Engine.instance = new Engine();
                }

                return Engine.instance;
            }
        }
        
        private ICell currentCell;
        private Renderer renderer;

        private bool keepRunning;
        private bool isRunning;
        private Playfield playField;

        protected ICell CurrentCell
        {
            get {
                return this.currentCell;
            }

            private set
            {
                this.currentCell = value;
            }
        }

        public Playfield PlayField 
        {
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Cannot set Playfield to null.");
                }
                this.playField = value;
                PlayfieldChangedEventArgs e = new PlayfieldChangedEventArgs(value);
                this.OnPlayfieldChanged(e);
            }

            get
            {
                return this.playField;
            }
        }

        protected Sounds SoundsPlayer { get; set; }

        /// <summary>
        /// Occurs when an arrow keypress is detected.
        /// </summary>
        public event EventHandler<CellEventArgs> CurrentCellChanged;
        public event EventHandler<CellRegionEventArgs> CellsInRegionChanged;
        public event EventHandler<PlayfieldChangedEventArgs> PlayfieldChanged;

        public Engine()
        {
            this.Initialize();
        }

        public void Initialize()
        { 
            //TODO: Read last playfield.
            this.PlayField = this.GetNewField();
            this.CurrentCell = this.PlayField[0, 0];
            this.SoundsPlayer = this.GetNewSoundsPlayer();
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
                this.renderer = new ConsoleRenderer(this);
            }

            //why the same value is given as in Start() method ?
            this.isRunning = true;
            while (keepRunning)
            {
                //Check if any keys where pressed
                ConsoleKey pressedKey;
                if (Console.KeyAvailable)
                {
                    pressedKey = Console.ReadKey().Key;
                    bool keyHandled = this.OnDirectionKeyPressed(pressedKey) &&
                        this.OnEnterKeyPressed(pressedKey);
                    
                    //Clear any pending keypresses from the inputstream.
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }
                }
            }
            this.isRunning = false;
        }

        private bool OnEnterKeyPressed(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            { 
                //TODO: Act on event
                return true;
            }

            return false;
        }

        private bool OnDirectionKeyPressed(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    this.ChangeCurrentCell(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    this.ChangeCurrentCell(1, 0);
                    break;
                case ConsoleKey.UpArrow:
                    this.ChangeCurrentCell(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    this.ChangeCurrentCell(0, 1);
                    break;
                default:
                    return false;
            }

            return true;
        }

        private void ChangeCurrentCell(int deltaX, int deltaY)
        {
            int currPosX = this.CurrentCell.X;
            int currPosY = this.CurrentCell.Y;
            int newPosX, newPosY;

            if (currPosX + deltaX < 0)
            {
                newPosX = 0;
            }
            else
            {
                newPosX = currPosX + deltaX;
            }

            if (currPosY + deltaY < 0)
            {
                newPosY = 0;
            }
            else
            {
                newPosY = currPosY + deltaY;
            }

            //Console.WriteLine("X " + newPosX+ "Y "+newPosY);
            Console.SetCursorPosition(newPosX, newPosY);

            if (currPosX != newPosX || currPosY != newPosY) 
            {
                ICell cell = this.PlayField[newPosX, newPosY];
                CellEventArgs e = new CellEventArgs(cell);
                this.OnCurrentCellChanged(e);
            }
        }

        private Playfield GetNewField()
        { 
            int sizeOfField = this.ReadSize();
            Playfield field = InitializeField(sizeOfField);
            return field;
        }

        private Sounds GetNewSoundsPlayer()
        {
            string pathToInvalidMoveSound = "../../Sounds/Resources/invalid.wav";
            string pathToDetonatedBombSound = "../../Sounds/Resources/boom.wav";
            Sounds player = new Sounds(pathToInvalidMoveSound, pathToDetonatedBombSound);

            return player;
        }

        /// <summary>
        /// Raises the <see cref="E:ArrowKeyPressed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BattleFiled.KeyEventArgs" /> instance containing the event data.</param>
        protected virtual void OnCurrentCellChanged(CellEventArgs e)
        {
            if (this.CurrentCellChanged != null)
            {
                this.CurrentCellChanged(this, e);
            }
        }

        protected virtual void OnCellsInRegionChanged(CellRegionEventArgs e)
        {
            if (this.CellsInRegionChanged != null)
            {
                this.CellsInRegionChanged(this, e);
            }
        }

        protected virtual void OnPlayfieldChanged(PlayfieldChangedEventArgs e)
        {
            if (this.PlayfieldChanged != null)
            {
                this.PlayfieldChanged(this, e);
            }
        }

        private int ReadSize()
        {
            Console.Write("Enter the size of the battle field: n = ");

            int sizeOfField;
            if(Int32.TryParse(Console.ReadLine(), out sizeOfField))
            {
                return sizeOfField;
            }
            else
            {
                Console.WriteLine("Wrong input, try again...");
                return this.ReadSize();
            }
        }

        private Playfield InitializeField(int sizeOfField)
        {
            Playfield field = Playfield.Instance;
            //field = null;

            field.SetFieldSize(sizeOfField);
            field.InitializeEmptyField();
            field.PlaceMines();

            return field;
        }
    }
}