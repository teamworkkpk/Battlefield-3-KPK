// <copyright file="Engine.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.GameEngine
{
    using System;
    using System.IO;
    using System.Threading;
    using BattleFiled;
    using BattleFiled.Cells;
    using BattleFiled.Renderer;
    using BattleFiled.SaveLoad;
    using BattleFiled.Sounds;
    using BattleFiled.StartMenu;
    using Interfaces;
    
    /// <summary>
    /// Engine will handle all the game logic.
    /// </summary>
    public class Engine : IEngine
    {
        /// <summary>
        /// Constant key value for saving the game.
        /// </summary>
        private const ConsoleKey SAVE_BUTTON = ConsoleKey.F5;

        /// <summary>
        /// Constant key value for loading the game.
        /// </summary>
        private const ConsoleKey LOAD_BUTTON = ConsoleKey.F6;

        /// <summary>
        /// Constant value for waiting the last bomb to explode, when the game ends.
        /// </summary>
        private const int IZCHAKAI_MUZIKATA_DA_SA_IZSVIRI_BE = 1200; ////Magic constant DON'T TOUCH!   
  
        /// <summary>
        /// The start screen of the game, which is singleton.
        /// </summary>
        private static StartScreen startMenu = StartScreen.Instance;

        /// <summary>
        /// Holds a size of a field, which serves for unit tests only.
        /// </summary>
        private static StringReader fieldSizeUnitTestSetter;

        /// <summary>
        /// Object of Engine type, which will serve for the single implementation.
        /// </summary>
        private static Engine instance;  

        /// <summary>
        /// The current cell at which the pointer is on the field.
        /// </summary>
        private ICell currentCell;

        /// <summary>
        /// The renderer that will render the game.
        /// </summary>
        private Renderer renderer;

        /// <summary>
        /// Shows the current visited cell on the screen.
        /// </summary>
        private Pointer pointer;

        /// <summary>
        /// Shows is the game will continue to run.
        /// </summary>
        private bool keepRunning;

        /// <summary>
        /// Shows if the game is currently running or should stop.
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// The playfield for the game.
        /// </summary>
        private Playfield playField;

        /// <summary>
        /// The object used for saving and loading.
        /// </summary>
        private SaveLoadAPI gameSaver;   
     
        /// <summary>
        /// The player of the game.
        /// </summary>
        private Player gamePlayer;

        /// <summary>
        /// Initializes a new instance of the Engine class.
        /// </summary>
        public Engine()
        {
            startMenu.SetChoise(ConsoleKey.Enter);
            this.HandleUserChoise();
        }

        /// <summary>
        /// Occurs when an arrow key press is detected.
        /// </summary>
        public event EventHandler<CellEventArgs> CurrentCellChanged;

        /// <summary>
        /// Occurs when a bomb explodes is detected.
        /// </summary>
        public event EventHandler<PlayfieldChangedEventArgs> PlayfieldChanged;

        /// <summary>
        /// Occurs when a cells is changed.
        /// </summary>
        public event EventHandler<CellEventArgs> CellChanged;

        /// <summary>
        /// Occurs when a should change type.
        /// </summary>
        public event EventHandler<CellEventArgs> CellRedefined;

        /// <summary>
        /// Occurs when a region of cells is changed.
        /// </summary>
        public event EventHandler<CellRegionEventArgs> CellsInRegionChanged;

        /// <summary>
        /// Occurs when a region of cells should change.
        /// </summary>
        public event EventHandler<CellRegionEventArgs> CellsInRegionRedefined;

        /// <summary>
        /// Gets the instance of the Engine class.
        /// </summary>
        /// <value>Engine instance.</value>
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

        /// <summary>
        /// Gets or sets the pointer.
        /// </summary>
        /// <value>Pointer pointer.</value>
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

        /// <summary>
        /// Gets the playfield.
        /// </summary>
        /// <value>Playfield playfield.</value>
        public Playfield PlayField
        {
            get
            {
                return this.playField;
            }

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
        }

        /// <summary>
        /// Gets the current cell.
        /// </summary>
        /// <value>ICell currentCell.</value>
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

        /// <summary>
        /// Gets or sets the sound player.
        /// </summary>
        /// <value>Sounds SoundPlayer.</value>
        protected Sounds SoundsPlayer { get; set; }

        /// <summary>
        /// Starts the engine.
        /// </summary>
        public void Start()
        {            
            if (!this.isRunning)
            {               
                this.keepRunning = true;
                this.Run();
            }
        }

        /// <summary>
        /// Stops the engine.
        /// </summary>
        public void Stop()
        {
            Thread.Sleep(IZCHAKAI_MUZIKATA_DA_SA_IZSVIRI_BE);
            this.keepRunning = false;            
        }

        /// <summary>
        /// Register event for cell changed.
        /// </summary>
        /// <param name="e">CellEventArgs e.</param>
        protected virtual void OnCurrentCellChanged(CellEventArgs e)
        {
            if (this.CurrentCellChanged != null)
            {
                this.CurrentCellChanged(this, e);
            }
        }

        /// <summary>
        /// Register event when a cell changes.
        /// </summary>
        /// <param name="e">CellEventArgs e.</param>
        protected virtual void OnCellChanged(CellEventArgs e)
        {
            if (this.CellChanged != null)
            {
                this.CellChanged(this, e);
            }
        }

        /// <summary>
        /// Register event for cell changed.
        /// </summary>
        /// <param name="e">CellEventArgs e.</param>
        protected virtual void OnCellRedefined(CellEventArgs e)
        {
            if (this.CellRedefined != null)
            {
                this.CellRedefined(this, e);
            }
        }

        /// <summary>
        /// Register event for cell region changed.
        /// </summary>
        /// <param name="e">CellEventArgs e.</param>
        protected virtual void OnCellsInRegionChanged(CellRegionEventArgs e)
        {
            if (this.CellsInRegionChanged != null)
            {
                this.CellsInRegionChanged(this, e);
            }
        }

        /// <summary>
        /// Register event for cell changes.
        /// </summary>
        /// <param name="e">CellEventArgs e.</param>
        protected virtual void OnCellsInRegionRedefined(CellRegionEventArgs e)
        {
            if (this.CellsInRegionRedefined != null)
            {
                this.CellsInRegionRedefined(this, e);
            }
        }

        /// <summary>
        /// Register event for playfield changed.
        /// </summary>
        /// <param name="e">CellEventArgs e.</param>
        protected virtual void OnPlayfieldChanged(PlayfieldChangedEventArgs e)
        {
            if (this.PlayfieldChanged != null)
            {
                this.PlayfieldChanged(this, e);
            }
        }

        /// <summary>
        /// Runs the initial engine preferences.
        /// </summary>
        private void Run()
        {
            if (this.renderer == null)
            {
                this.renderer = new ConsoleRenderer(this);
            }
           
            this.isRunning = true;
            while (this.keepRunning)
            {
                ConsoleKey pressedKey;
                if (Console.KeyAvailable)
                {
                    pressedKey = Console.ReadKey().Key;
                    bool keyHandled = this.OnDirectionKeyPressed(pressedKey) ||
                        this.OnEnterKeyPressed(pressedKey) || this.OnSaveLoadButtonPressed(pressedKey);

                    this.renderer.DrawAll();
                    this.IsGameOver();

                    ////Clear any pending keypresses from the inputstream.
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }                    
                }
            }

            this.isRunning = false;
        }

        /// <summary>
        /// Handle key pressed events.
        /// </summary>
        /// <param name="key">ConsoleKey key.</param>
        /// <returns>Returns true if current cell is a bomb and false, if it's not.</returns>
        private bool OnEnterKeyPressed(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            {
                int cellX = this.Pointer.X;
                int cellY = this.Pointer.Y;

                ICell currentCell = this.playField[cellX, cellY];

                if (currentCell.CellType == CellType.Bomb)
                {
                    this.SoundsPlayer.PlayDetonatedBomb();
                    this.HandleExplosion(currentCell as BombCell);                    
                }
                else if (currentCell.CellType == CellType.BlownCell || currentCell.CellType == CellType.EmptyCell)
                {
                    this.SoundsPlayer.PlayInvalidSelection();
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Handle explosion with a bomb cell.
        /// </summary>
        /// <param name="currentCell">BombCell currentCell.</param>
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

        /// <summary>
        /// Changes the cell to BlownCell.
        /// </summary>
        /// <param name="posX">X position of the cell.</param>
        /// <param name="posY">Y position of the cell.</param>
        private void MakeCellBlown(int posX, int posY)
        {
            ICell cell = CellFactory.CreateCell(CellType.BlownCell);
            cell.X = posX;
            cell.Y = posY;
            this.PlayField[posX, posY] = cell;

            ////Raise event to update the cell view.
            CellEventArgs e = new CellEventArgs(cell);
            this.OnCellRedefined(e);
        }
        
        /// <summary>
        /// Handle the explosion within one explosion radius.
        /// </summary>
        /// <param name="cell">BombCell cell.</param>
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

        /// <summary>
        /// Handle the explosion within two explosion radius.
        /// </summary>
        /// <param name="cell">BombCell cell.</param>
        private void HandleExplosionTwoRadius(BombCell cell)
        {
            this.HandleExplosionOneRadius(cell);

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

        /// <summary>
        /// Handle the explosion within three explosion radius.
        /// </summary>
        /// <param name="cell">BombCell cell.</param>
        private void HandleExplosionThreeRadius(BombCell cell)
        {
            this.HandleExplosionTwoRadius(cell);

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

        /// <summary>
        /// Handle the explosion within four explosion radius.
        /// </summary>
        /// <param name="cell">BombCell cell.</param>
        private void HandleExplosionFourRadius(BombCell cell)
        {
            this.HandleExplosionThreeRadius(cell);

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

        /// <summary>
        /// Handle the explosion within five explosion radius.
        /// </summary>
        /// <param name="cell">BombCell cell.</param>
        private void HandleExplosionFiveRadius(BombCell cell)
        {
            this.HandleExplosionFourRadius(cell);

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

        /// <summary>
        /// Change the current cell based on the pressed key.
        /// </summary>
        /// <param name="key">ConsoleKey key.</param>
        /// <returns>Return true if the current cell is changed and false if it is not.</returns>
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

        /// <summary>
        /// Change the current cell coordinates.
        /// </summary>
        /// <param name="deltaX">Integer deltaX.</param>
        /// <param name="deltaY">Integer deltaY.</param>
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

        /// <summary>
        /// Get a new instance of a playfield.
        /// </summary>
        /// <returns>Return the new playfield.</returns>
        private Playfield GetNewField()
        {
            int sizeOfField = this.ReadSize();
            Playfield field = this.InitializeField(sizeOfField);
            return field;
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="isLoadGameChosen">Check if a load game is chosen.</param>
        private void Initialize(bool isLoadGameChosen)
        {
            this.gameSaver = new SaveLoadAPI();
            this.gamePlayer = new Player("Pesho");

            if (isLoadGameChosen)
            {
                this.gameSaver.LoadGame();
                this.PlayField = this.InitializeField(this.gameSaver.MementoField.FieldDimension);
                this.PlayField.LoadMemento(this.gameSaver.MementoField);
            }
            else
            {
                this.PlayField = this.GetNewField();
            }

            this.CurrentCell = this.PlayField[0, 0];
            this.SoundsPlayer = this.GetNewSoundsPlayer();
            this.Pointer = new Pointer(this.playField[0, 0].X, this.playField[0, 0].Y);
        }

        /// <summary>
        /// Get a new instance of a sound player.
        /// </summary>
        /// <returns>Return the new player.</returns>
        private Sounds GetNewSoundsPlayer()
        {
            string pathToInvalidMoveSound = "../../Sounds/Resources/invalid.wav";
            string pathToDetonatedBombSound = "../../Sounds/Resources/boom.wav";
            string pathToPositionChangedSound = "../../Sounds/Resources/move.wav";
            Sounds player = new Sounds(pathToInvalidMoveSound, pathToDetonatedBombSound, pathToPositionChangedSound);

            return player;
        }

        /// <summary>
        /// Read user input for field size.
        /// </summary>
        /// <returns>Returns the size.</returns>
        private int ReadSize()
        {
            Console.Write("Enter the size of the battle field: n = ");

            if (fieldSizeUnitTestSetter != null)
            {
                Console.SetIn(fieldSizeUnitTestSetter);
            }

            int sizeOfField;
            if (int.TryParse(Console.ReadLine(), out sizeOfField))
            {
                return sizeOfField;
            }
            else
            {
                Console.WriteLine("Wrong input, try again...");
                return this.ReadSize();
            }
        }

        /// <summary>
        /// A new instance of a playfield is initialized.
        /// </summary>
        /// <param name="sizeOfField">Integer sizeOfField.</param>
        /// <returns>Returns the field.</returns>
        private Playfield InitializeField(int sizeOfField)
        {
            Playfield field = Playfield.Instance;
            field.SetFieldSize(sizeOfField);
            field.InitializeEmptyField();
            field.PlaceMines();

            return field;
        }

        /// <summary>
        /// Handles the action, when a save or load button is pressed.
        /// </summary>
        /// <param name="key">ConsoleKey key.</param>
        /// <returns>Returns true, if save/load button is pressed. Otherwise false.</returns>
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
                        this.PlayField = this.InitializeField(this.gameSaver.MementoField.FieldDimension);
                        this.PlayField.LoadMemento(this.gameSaver.MementoField);
                        this.gamePlayer.LoadMemento(this.gameSaver.MementoPlayer);
                        CellRegionEventArgs e = new CellRegionEventArgs(0, 0, this.PlayField.PlayfieldSize, this.PlayField.PlayfieldSize);
                        this.OnCellsInRegionRedefined(e);
                        return true;
                    }

                default:
                    return false;
            }
        }

        /// <summary>
        /// Handles the user choice on the start screen.
        /// </summary>
        private void HandleUserChoise()
        {
            if (startMenu.IsStartGameChosen)
            {
                this.Initialize(false);
            } 
            else if (startMenu.IsQuitGameChosen)
            {
                Console.WriteLine("Goodbye...");
                this.isRunning = true;
            }
            else if (startMenu.IsLoadGameChosen)
            {
                this.Initialize(true);                
            }
        }

        /// <summary>
        /// Ends the game.
        /// </summary>
        private void IsGameOver()
        {            
            foreach (ICell item in this.PlayField)
            {
                if (item.CellType == CellType.Bomb)
                {
                    return; 
                }
            }

            this.renderer.DrawGameOver(this.gamePlayer.DetonatedMines);            
            this.Stop();
        }
    }
}