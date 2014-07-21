namespace BattleFiled.GameEngine
{
    using System;
    using BattleFiled;
    using BattleFiled.Cells;
    using BattleFiled.Renderer;
    using BattleFiled.Sounds;
    using BattleFiled.SaveLoad;
    using BattleFiled.StartMenu;
    using System.Threading;

    class Engine
    {
        private const ConsoleKey SAVE_BUTTON = ConsoleKey.F5;
        private const ConsoleKey LOAD_BUTTON = ConsoleKey.F6;
        private const int IZCHAKAI_MUZIKATA_DA_SA_IZSVIRI_BE = 1200; //Magic constant DON'T TOUCH!

        private static Engine instance;

        public static Engine Instance
        {
            get
            {
                if (Engine.instance == null)
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
        private SaveLoadAPI gameSaver;
        private StartScreen startMenu = StartScreen.Instance;
        private Player gamePlayer;

        protected ICell CurrentCell
        {
            get
            {
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
                if (value.X < 0 || value.Y < 0)
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
        public event EventHandler<PlayfieldChangedEventArgs> PlayfieldChanged;
        public event EventHandler<CellEventArgs> CellChanged;
        public event EventHandler<CellEventArgs> CellRedefined;
        public event EventHandler<CellRegionEventArgs> CellsInRegionChanged;
        public event EventHandler<CellRegionEventArgs> CellsInRegionRedefined;

        public Engine()
        {
            this.startMenu.SetChoise();
            this.HandleUserChoise();
            //this.Initialize(false);
        }

        private void Initialize(bool isLoadGameChosen)
        {
            this.gameSaver = new SaveLoadAPI();
            this.gamePlayer = new Player("Pesho");

            if (isLoadGameChosen)
            {
                gameSaver.LoadGame();
                this.PlayField = InitializeField(gameSaver.MementoField.FieldDimension);
                this.PlayField.LoadMemento(gameSaver.MementoField);
            }
            else
            {
                this.PlayField = this.GetNewField();
            }
            
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
            Thread.Sleep(IZCHAKAI_MUZIKATA_DA_SA_IZSVIRI_BE);
            this.keepRunning = false;            
        }

        private void Run()
        {
            if (this.renderer == null)
            {
                this.renderer = new ConsoleRenderer(this);
            }
           
            this.isRunning = true;
            while (keepRunning)
            {
                
                ConsoleKey pressedKey;
                if (Console.KeyAvailable)
                {
                    pressedKey = Console.ReadKey().Key;
                    bool keyHandled = this.OnDirectionKeyPressed(pressedKey) ||
                        this.OnEnterKeyPressed(pressedKey) || this.OnSaveLoadButtonPressed(pressedKey);

                    this.renderer.DrawAll();
                    this.IsGameOver();

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
                    SoundsPlayer.PlayDetonatedBomb();
                    HandleExplosion(currentCell as BombCell);                    
                }

                else if (currentCell.CellType == CellType.BlownCell || currentCell.CellType == CellType.EmptyCell)
                {
                    SoundsPlayer.PlayInvalidSelection();
                }
                return true;
            }

            return false;
        }

        private void HandleExplosion(BombCell currentCell)
        {
            this.gamePlayer.DetonatedMines++;

            switch (currentCell.BombSize)
            {
                case 1:
                    this.HandleExplosionOneRadius(currentCell);
                    break;
                case 2:
                    this.HandleExplosionTwoRadius(currentCell);
                    break;
                case 3:
                    this.HandleExplosionThreeRadius(currentCell);
                    break;
                case 4:
                    this.HandleExplosionFourRadius(currentCell);
                    break;
                case 5:
                    this.HandleExplosionFiveRadius(currentCell);
                    break;
                default:
                    throw new NotImplementedException("Cannot handle explosion with radius more than 5.");
            }
        }

        private void MakeCellBlown(int posX, int posY)
        {
            ICell cell = CellFactory.CreateCell(CellType.BlownCell);
            cell.X = posX;
            cell.Y = posY;
            this.PlayField[posX, posY] = cell;

            //Raise event to update the cell view.
            CellEventArgs e = new CellEventArgs(cell);
            this.OnCellRedefined(e);
        }
        
        private void HandleExplosionOneRadius(BombCell cell)
        {
            int bombX = cell.X;
            int bombY = cell.Y;

            this.MakeCellBlown(bombX, bombY);

            if (bombX - 1 >= 0 && bombY - 1 >= 0)
            {
                this.MakeCellBlown(bombX - 1, bombY - 1);
            }

            if (bombX - 1 >= 0 && bombY + 1 < this.playField.PlayfieldSize)
            {
                this.MakeCellBlown(bombX - 1, bombY + 1);
            }

            if (bombX + 1 < this.playField.PlayfieldSize && bombY + 1 < this.playField.PlayfieldSize)
            {
                this.MakeCellBlown(bombX + 1, bombY + 1);
            }

            if (bombX + 1 < this.playField.PlayfieldSize && bombY - 1 >= 0)
            {
                this.MakeCellBlown(bombX + 1, bombY - 1);
            }
        }

        private void HandleExplosionTwoRadius(BombCell cell)
        {
            HandleExplosionOneRadius(cell);

            int bombX = cell.X;
            int bombY = cell.Y;

            if (bombX - 1 >= 0)
            {
                this.MakeCellBlown(bombX - 1, bombY);
            }

            if (bombY + 1 < this.playField.PlayfieldSize)
            {
                this.MakeCellBlown(bombX, bombY + 1);
            }

            if (bombX + 1 < this.playField.PlayfieldSize)
            {
                this.MakeCellBlown(bombX + 1, bombY);
            }

            if (bombY - 1 >= 0)
            {
                this.MakeCellBlown(bombX, bombY - 1);
            }
        }

        private void HandleExplosionThreeRadius(BombCell cell)
        {
            HandleExplosionTwoRadius(cell);

            int bombX = cell.X;
            int bombY = cell.Y;

            if (bombX - 2 >= 0)
            {
                this.MakeCellBlown(bombX - 2, bombY);
            }

            if (bombY + 2 < this.playField.PlayfieldSize)
            {
                this.MakeCellBlown(bombX, bombY + 2);
            }

            if (bombX + 2 < this.playField.PlayfieldSize)
            {
                this.MakeCellBlown(bombX + 2, bombY);
            }

            if (bombY - 2 >= 0)
            {
                this.MakeCellBlown(bombX, bombY - 2);
            }
        }

        private void HandleExplosionFourRadius(BombCell cell)
        {
            HandleExplosionThreeRadius(cell);

            int bombX = cell.X;
            int bombY = cell.Y;

            if (bombX - 2 >= 0)
            {
                if (bombY - 1 >= 0)
                {
                    this.MakeCellBlown(bombX - 2, bombY - 1);
                }

                if (bombY + 1 < this.playField.PlayfieldSize)
                {
                    this.MakeCellBlown(bombX - 2, bombY + 1);
                }
            }

            if (bombY + 2 < this.playField.PlayfieldSize)
            {
                if (bombX - 1 >= 0)
                {
                    this.MakeCellBlown(bombX - 1, bombY + 2);
                }

                if (bombX + 1 < this.playField.PlayfieldSize)
                {
                    this.MakeCellBlown(bombX + 1, bombY + 2);
                }
            }

            if (bombX + 2 < this.playField.PlayfieldSize)
            {
                if (bombY - 1 >= 0)
                {
                    this.MakeCellBlown(bombX + 2, bombY - 1);
                }

                if (bombY + 1 < this.playField.PlayfieldSize)
                {
                    this.MakeCellBlown(bombX + 2, bombY + 1);
                }
            }

            if (bombY - 2 >= 0)
            {
                if (bombX - 1 >= 0)
                {
                    this.MakeCellBlown(bombX - 1, bombY - 2);
                }

                if (bombX + 1 < this.playField.PlayfieldSize)
                {
                    this.MakeCellBlown(bombX + 1, bombY - 2);
                }
            }
        }

        private void HandleExplosionFiveRadius(BombCell cell)
        {
            HandleExplosionFourRadius(cell);

            int bombX = cell.X;
            int bombY = cell.Y;

            if (bombX - 2 >= 0 && bombY - 2 >= 0)
            {
                this.MakeCellBlown(bombX - 2, bombY - 2);
            }

            if (bombX - 2 >= 0 && bombY + 2 < this.playField.PlayfieldSize)
            {
                this.MakeCellBlown(bombX - 2, bombY + 2);
            }

            if (bombX + 2 < this.playField.PlayfieldSize && bombY + 2 < this.playField.PlayfieldSize)
            {
                this.MakeCellBlown(bombX + 2, bombY + 2);
            }

            if (bombX + 2 < this.playField.PlayfieldSize && bombY - 2 >= 0)
            {
                this.MakeCellBlown(bombX + 2, bombY - 2);
            }
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

            this.SoundsPlayer.PlayPositionChanged();

            return true;
        }

        private void ChangeCurrentCell(int deltaX, int deltaY)
        {
            if (!(this.Pointer.X + deltaX < 0 || this.Pointer.X + deltaX > this.playField.PlayfieldSize - 1))
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
            string pathToPositionChangedSound = "../../Sounds/Resources/move.wav";
            Sounds player = new Sounds(pathToInvalidMoveSound, pathToDetonatedBombSound, pathToPositionChangedSound);

            return player;
        }
                
        protected virtual void OnCurrentCellChanged(CellEventArgs e)
        {
            if (this.CurrentCellChanged != null)
            {
                this.CurrentCellChanged(this, e);
            }
        }

        protected virtual void OnCellChanged(CellEventArgs e)
        {
            if (this.CellChanged != null)
            {
                this.CellChanged(this, e);
            }
        }

        protected virtual void OnCellRedefined(CellEventArgs e)
        {
            if (this.CellRedefined != null)
            {
                this.CellRedefined(this, e);
            }
        }

        protected virtual void OnCellsInRegionChanged(CellRegionEventArgs e)
        {
            if (this.CellsInRegionChanged != null)
            {
                this.CellsInRegionChanged(this, e);
            }
        }

        protected virtual void OnCellsInRegionRedefined(CellRegionEventArgs e)
        {
            if (this.CellsInRegionRedefined!= null)
            {
                this.CellsInRegionRedefined(this, e);
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
            if (Int32.TryParse(Console.ReadLine(), out sizeOfField))
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

        private bool OnSaveLoadButtonPressed(ConsoleKey key)
        {            
            switch (key)
            {
                case SAVE_BUTTON:
                    {   
                        this.gameSaver.MementoField = this.PlayField.SaveMemento();
                        this.gameSaver.MementoPlayer = this.gamePlayer.SaveMemento();
                        this.gameSaver.SaveGame();                        
                        return true;
                    }

                case LOAD_BUTTON:
                    {
                        this.gameSaver.LoadGame();
                        this.PlayField = InitializeField(gameSaver.MementoField.FieldDimension);
                        this.PlayField.LoadMemento(this.gameSaver.MementoField);
                        this.gamePlayer.LoadMemento(this.gameSaver.MementoPlayer);
                        CellRegionEventArgs e = new CellRegionEventArgs(0, 0, 
                            this.PlayField.PlayfieldSize, this.PlayField.PlayfieldSize);
                        this.OnCellsInRegionRedefined(e);
                        return true;
                    }
                default:
                    return false;
            }
        }

        private void HandleUserChoise()
        {
            if (startMenu.IsStartGameChosen)
            {
                this.Initialize(false);
            } 
            else if(startMenu.IsQuitGameChosen)
            {
                Console.WriteLine("Goodbye...");
                this.isRunning = true;
            }
            else if (startMenu.IsLoadGameChosen)
            {
                this.Initialize(true);                
            }
        }

        private void IsGameOver()
        {            
            foreach (ICell item in this.PlayField)
            {
                if (item.CellType == CellType.Bomb)
                {
                    return; 
                }
            }

            renderer.DrawGameOver(this.gamePlayer.DetonatedMines);            
            this.Stop();
        }
    }
}