namespace BattlefieldTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BattleFiled.GameEngine;
    using System.IO;
    using BattleFiled.Cells;
    using BattleFiled.Renderer;
    using BattleFiled;

    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        public void TestIfEngineHandlesQuitGameCommandOnStartScreen()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsQuitGameChosen = true;
            Engine gameEngine = new Engine();
            PrivateObject obj = new PrivateObject(gameEngine);
            var isRunning = obj.GetFieldOrProperty("isRunning");
            var retVal = obj.Invoke("HandleUserChoise");

            Assert.AreEqual(isRunning, true, "Set user choice to quit game. Expected isRunning property true. Received false");
        }

        [TestMethod]
        public void TestIfEngineHasCurrentCellWithCoordinates00WhenInitialized()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();

            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            var currentCell = enginePrivateInstance.GetFieldOrProperty("CurrentCell");
            PrivateObject engineCurrentCell = new PrivateObject(currentCell);
            int currentCellX = (int)engineCurrentCell.GetFieldOrProperty("X");
            int currentCellY = (int)engineCurrentCell.GetFieldOrProperty("Y");
            bool isCurrentCellAtX0AndY0 = (currentCellX == 0 && currentCellY == 0);

            Assert.AreEqual(isCurrentCellAtX0AndY0, true, "Expected that current cell is on coordinate 0,0. Received x={0} y={1}", currentCellX, currentCellY);
        }

        [TestMethod]
        public void TestIfPointerChangesItsCoordinateWithPlusOneOnChangeCurrentCell()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();

            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            enginePrivateInstance.Invoke("ChangeCurrentCell", 1, 1);
            var pointer = enginePrivateInstance.GetFieldOrProperty("Pointer");
            PrivateObject enginePointer = new PrivateObject(pointer);
            int currentPointerX = (int)enginePointer.GetFieldOrProperty("X");
            int currentPointerY = (int)enginePointer.GetFieldOrProperty("Y");
            bool isCurrentCellAtX1AndY1 = (currentPointerX == 1 && currentPointerY == 1);

            Assert.AreEqual(isCurrentCellAtX1AndY1, true, "Expected that current cell is on coordinate 1,1. Received x={0} y={1}", currentPointerX, currentPointerY);
        }

        [TestMethod]
        public void TestIfBombWithSizeFiveExplodesAsExpected()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();
            testField[4, 4] = new BombCell(5);

            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            enginePrivateInstance.Invoke("HandleExplosion", testField[4, 4]);
            Playfield engineField = (Playfield)enginePrivateInstance.GetFieldOrProperty("playField");
            BombCell bomb = new BombCell(5);
            bomb.X = 4;
            bomb.Y = 4;
            engineField[4, 4] = bomb;
            
            enginePrivateInstance.Invoke("ChangeCurrentCell", 4, 4);
            enginePrivateInstance.Invoke("HandleExplosion", engineField[4, 4]);

            Assert.AreEqual(engineField[4, 4].CellType == CellType.BlownCell, true, "Expected that the cell on coordinates 4,5 is CellType.BlownCell. Received {0} ", engineField[4, 4].CellType);
        }

        [TestMethod]
        public void TestIfBombWithSizeFiveExplodesAsExpectedWhenTheBombIsNearTheTopLeftFieldCorner()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();
            testField[1, 1] = new BombCell(5);

            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            enginePrivateInstance.Invoke("HandleExplosion", testField[1, 1]);
            Playfield engineField = (Playfield)enginePrivateInstance.GetFieldOrProperty("playField");
            BombCell bomb = new BombCell(5);
            bomb.X = 4;
            bomb.Y = 4;
            engineField[1, 1] = bomb;

            enginePrivateInstance.Invoke("ChangeCurrentCell", 1, 1);
            enginePrivateInstance.Invoke("HandleExplosion", engineField[1, 1]);

            Assert.AreEqual(engineField[1, 2].CellType == CellType.BlownCell, true, "Expected that the cell on coordinates 1,2 is CellType.BlownCell. Received {0} ", engineField.ToString());
        }

        [TestMethod]
        public void TestIfBombWithSizeFiveExplodesAsExpectedWhenTheBombIsNearTheBottompLeftFieldCorner()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();
            testField[1, 5] = new BombCell(5);

            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            enginePrivateInstance.Invoke("HandleExplosion", testField[1, 5]);
            Playfield engineField = (Playfield)enginePrivateInstance.GetFieldOrProperty("playField");
            BombCell bomb = new BombCell(5);
            bomb.X = 4;
            bomb.Y = 4;
            engineField[1, 5] = bomb;

            enginePrivateInstance.Invoke("ChangeCurrentCell", 1, 5);
            enginePrivateInstance.Invoke("HandleExplosion", engineField[1, 5]);

            Assert.AreEqual(engineField[2, 5].CellType == CellType.BlownCell, true, "Expected that the cell on coordinates 2,5 is CellType.BlownCell. Received {0} ", engineField.ToString());
        }

        [TestMethod]
        public void TestIfBombWithSizeFourExplodesAsExpectedWhenTheBombIsNearTheBottompLeftFieldCorner()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();
            testField[1, 5] = new BombCell(4);

            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            enginePrivateInstance.Invoke("HandleExplosion", testField[1, 5]);
            Playfield engineField = (Playfield)enginePrivateInstance.GetFieldOrProperty("playField");
            BombCell bomb = new BombCell(4);
            bomb.X = 4;
            bomb.Y = 4;
            engineField[1, 5] = bomb;

            enginePrivateInstance.Invoke("ChangeCurrentCell", 1, 5);
            enginePrivateInstance.Invoke("HandleExplosion", engineField[1, 5]);

            Assert.AreEqual(engineField[2, 5].CellType == CellType.BlownCell, true, "Expected that the cell on coordinates 2,5 is CellType.BlownCell. Received {0} ", engineField.ToString());
        }

        [TestMethod]
        public void TestIfBombWithSizeFourExplodesAsExpectedWhenTheBombIsNearTheTopLeftFieldCorner()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();
            testField[1, 1] = new BombCell(4);

            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            enginePrivateInstance.Invoke("HandleExplosion", testField[1, 1]);
            Playfield engineField = (Playfield)enginePrivateInstance.GetFieldOrProperty("playField");
            BombCell bomb = new BombCell(4);
            bomb.X = 4;
            bomb.Y = 4;
            engineField[1, 1] = bomb;

            enginePrivateInstance.Invoke("ChangeCurrentCell", 1, 1);
            enginePrivateInstance.Invoke("HandleExplosion", engineField[1, 1]);

            Assert.AreEqual(engineField[1, 2].CellType == CellType.BlownCell, true, "Expected that the cell on coordinates 1,2 is CellType.BlownCell. Received {0} ", engineField.ToString());
        }

        [TestMethod]
        public void TestIfBombWithSizeFourExplodesAsExpected()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();
            testField[4, 4] = new BombCell(4);

            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            enginePrivateInstance.Invoke("HandleExplosion", testField[4, 4]);
            Playfield engineField = (Playfield)enginePrivateInstance.GetFieldOrProperty("playField");
            BombCell bomb = new BombCell(4);
            bomb.X = 4;
            bomb.Y = 4;
            engineField[4, 4] = bomb;

            enginePrivateInstance.Invoke("ChangeCurrentCell", 4, 4);
            enginePrivateInstance.Invoke("HandleExplosion", engineField[4, 4]);

            Assert.AreEqual(engineField[4, 4].CellType == CellType.BlownCell, true, "Expected that the cell on coordinates 4,5 is CellType.BlownCell. Received {0} ", engineField[4, 4].CellType);
        }

        [TestMethod]
        public void TestIfBombWithSizeFiveExplodesAsExpectedWhenOnTheTopLeft()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();
            testField[0, 0] = new BombCell(5);

            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            enginePrivateInstance.Invoke("HandleExplosion", testField[0, 0]);
            Playfield engineField = (Playfield)enginePrivateInstance.GetFieldOrProperty("playField");
            BombCell bomb = new BombCell(5);
            bomb.X = 0;
            bomb.Y = 0;
            engineField[0, 0] = bomb;

            enginePrivateInstance.Invoke("ChangeCurrentCell", 1, 1);
            enginePrivateInstance.Invoke("HandleExplosion", engineField[0, 0]);

            Assert.AreEqual(engineField[0, 1].CellType == CellType.BlownCell, true, "Expected that the cell on coordinates 4,5 is CellType.BlownCell. Received {0} ", engineField[4, 4].CellType);
        }

        [TestMethod]
        public void TestIfOnDirectionKeyPressedReturnsTrue()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();


            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            var output = enginePrivateInstance.Invoke("OnDirectionKeyPressed", ConsoleKey.UpArrow);

            Assert.IsTrue((bool)output, "When left key arrow is pressed should return true");

            output = enginePrivateInstance.Invoke("OnDirectionKeyPressed", ConsoleKey.LeftArrow);

            Assert.IsTrue((bool)output, "When left key arrow is pressed should return true");

            output = enginePrivateInstance.Invoke("OnDirectionKeyPressed", ConsoleKey.RightArrow);

            Assert.IsTrue((bool)output, "When left key arrow is pressed should return true");

            output = enginePrivateInstance.Invoke("OnDirectionKeyPressed", ConsoleKey.DownArrow);

            Assert.IsTrue((bool)output, "When left key arrow is pressed should return true");

            output = enginePrivateInstance.Invoke("OnDirectionKeyPressed", ConsoleKey.E);

            Assert.IsFalse((bool)output, "When left key arrow is pressed should return true");

        }

        [TestMethod]
        public void TestIfOnSaveLoadButtonPressedReturnsTrue()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();


            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            var output = enginePrivateInstance.Invoke("OnSaveLoadButtonPressed", ConsoleKey.F5);

            Assert.IsTrue((bool)output, "When save key is pressed should return true");

            output = enginePrivateInstance.Invoke("OnSaveLoadButtonPressed", ConsoleKey.F6);

            Assert.IsTrue((bool)output, "When load button is pressed should return true");
        }


        [TestMethod]
        public void TestIfHandleExplosionHandleAllCases()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();


            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            enginePrivateInstance.Invoke("HandleExplosion", new BombCell(1));
            enginePrivateInstance.Invoke("HandleExplosion", new BombCell(2));
            enginePrivateInstance.Invoke("HandleExplosion", new BombCell(3));
            enginePrivateInstance.Invoke("HandleExplosion", new BombCell(4));

        }

        [TestMethod]
        public void TestOnCellChangedDontRiseEvent()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();


            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            enginePrivateInstance.Invoke("OnCellChanged", new CellEventArgs(new BombCell(1)));
        }

        [TestMethod]
        public void TestOnCellsInRegionChangedDontRiseEvent()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();


            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            enginePrivateInstance.Invoke("OnCellsInRegionChanged", new CellRegionEventArgs(0, 0, 0, 0));
        }


        [TestMethod]
        public void TestOnCurrentCellChangedDontRiseEvent()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();


            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            enginePrivateInstance.Invoke("OnCurrentCellChanged", new CellEventArgs(new EmptyCell()));
        }

        [TestMethod]
        public void TestIfDrawGameOver()
        {
            int moves = 6;
            string testFieldSize = "6";

            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = Engine.Instance;
            ConsoleRenderer cr = new ConsoleRenderer(gameEngine);

            cr.DrawGameOver(moves);
        }

        [TestMethod]
        public void TestIfEngineRun()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();

            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);

            enginePrivateInstance.Invoke("Run");
        }

        [TestMethod]
        public void TestIfEngineStop()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();

            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);

            enginePrivateInstance.Invoke("Stop");
        }
        [TestMethod]
        public void TestIfEngineHandlesLoadGameCommandOnStartScreen()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsLoadGameChosen = true;
            Engine gameEngine = new Engine();
            PrivateObject obj = new PrivateObject(gameEngine);
            var isRunning = obj.GetFieldOrProperty("isRunning");
            var retVal = obj.Invoke("HandleUserChoise");

            Assert.AreEqual(isRunning, false, "Set user choice to quit game. Expected isRunning property true. Received false");
        }

        [TestMethod]
        public void TestIfEngineInitializeHandlesLoadGameCommandOnStartScreen()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsLoadGameChosen = true;
            Engine gameEngine = new Engine();
            PrivateObject obj = new PrivateObject(gameEngine);
            obj.Invoke("Initialize", true);
            var isRunning = obj.GetFieldOrProperty("isRunning");
            var retVal = obj.Invoke("HandleUserChoise");

            Assert.AreEqual(isRunning, false, "Set user choice to quit game. Expected isRunning property true. Received false");
        }

        [TestMethod]
        public void TestIfEngineGameOverWorks()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();
            
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);
            testField.InitializeEmptyField();

            PrivateObject testPrivateEngine = new PrivateObject(gameEngine);
            testPrivateEngine.Invoke("Run");
            testPrivateEngine.Invoke("IsGameOver");
            bool keepRunning = (bool)testPrivateEngine.GetFieldOrProperty("keepRunning");

            Assert.AreEqual(keepRunning, false, "Check if keepRunning variable in Engine is set to false after GameOver is called with field full with empty cells only. Expected keepRunning property false. Received true");
        }

    }
}
