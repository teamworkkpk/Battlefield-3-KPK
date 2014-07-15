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
        private Pointer pointer;
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

        public Pointer Pointer
        {
            get
            {
                return this.pointer;
            }
            set
            {
                if(value.X < 0 || value.Y < 0)
                {
                    throw new ArgumentException("Pointer x and y coordinate must be positive integer numbers");
                }

                this.pointer = value;
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
            this.Pointer = new Pointer(this.playField[0, 0].X, this.playField[0, 0].Y);
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
                    bool keyHandled = this.OnDirectionKeyPressed(pressedKey) ||
                        this.OnEnterKeyPressed(pressedKey);

                    this.renderer.DrawAll();

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
                int cellX = this.Pointer.X;
                int cellY = this.Pointer.Y;

                ICell currentCell = this.playField[cellX, cellY];

                if (currentCell.CellType == CellType.Bomb)
                {
                    HandleExplosion(currentCell as BombCell);
                    SoundsPlayer.PlayDetonatedBomb();                    
                }

                else if (currentCell.CellType == CellType.BlownCell || currentCell.CellType == CellType.EmptyCell)
                {
                    SoundsPlayer.PlayInvalidMove();
                }
                return true;
            }

            return false;
        }

        private void HandleExplosion(BombCell currentCell)
        {
            switch (currentCell.BombSize)
            {
                case 1:
                    ExplosionOneRadius(currentCell);
                    break;
                //case 2:
                //    ExplosionTwoRadius();
                //    break;
                //case 3:
                //    ExplosionThreeRadius();
                //    break;
                //case 4:
                //    ExplosionFourRadius();
                //    break;
                //case 5:
                //    ExplosionFiveRadius();
                //    break;

            }
        }

        private void ExplosionOneRadius(BombCell cell)
        {
            int bombX = cell.X;
            int bombY = cell.Y;

            this.playField[bombX, bombY] = CellFactory.CreateCell(CellType.BlownCell);
            SetBlownCellPosition(bombX, bombY);

            if (bombX - 1 >= 0 && bombY - 1 >= 0)
            {                
                this.PlayField[bombX - 1, bombY - 1] = CellFactory.CreateCell(CellType.BlownCell);
                SetBlownCellPosition(bombX -1 , bombY - 1);
            }
            if (bombX - 1 >= 0 && bombY < this.playField.PlayfieldSize - 1)
            {
                this.PlayField[bombX - 1, bombY + 1] = CellFactory.CreateCell(CellType.BlownCell);
                SetBlownCellPosition(bombX -1, bombY + 1);
            }
            if (bombX < this.playField.PlayfieldSize - 1 && bombY - 1 > 0)
            {
                this.PlayField[bombX + 1, bombY - 1] = CellFactory.CreateCell(CellType.BlownCell);
                SetBlownCellPosition(bombX + 1, bombY -1);
            }
            if (bombX < this.playField.PlayfieldSize - 1 && bombY < this.playField.PlayfieldSize - 1)
            {
                this.PlayField[bombX + 1, bombY + 1] = CellFactory.CreateCell(CellType.BlownCell);
                SetBlownCellPosition(bombX + 1, bombY + 1);
            }
            
            renderer.ChangeCellView(this.playField);
        }

        private void SetBlownCellPosition(int oldX, int oldY)
        {
            this.PlayField[oldX, oldY].X = oldX;
            this.PlayField[oldX, oldY].Y = oldY;
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
            if (!(this.Pointer.X + deltaX < 0 || this.Pointer.X + deltaX > this.playField.PlayfieldSize-1))
            {
                this.Pointer.X += deltaX;
            }

            if (!(this.Pointer.Y + deltaY < 0 || this.Pointer.Y + deltaY > this.playField.PlayfieldSize - 1))
            {
                this.Pointer.Y += deltaY;
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