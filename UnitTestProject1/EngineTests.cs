using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleFiled.GameEngine;
using System.IO;
using BattleFiled.Cells;
using System.Collections;
using System.Collections.Generic;
using BattleFiled;

namespace BattlefieldTests
{
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

        //Doesn't work as expected. Neighbour cells are not blown
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

            Assert.AreEqual(testField[4, 5].CellType == CellType.BlownCell, true, "Expected that the cell on coordinates 4,5 is CellType.BlownCell. Received {0} ", testField[4, 5].CellType);
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
            enginePrivateInstance.Invoke("OnCellsInRegionChanged", new CellRegionEventArgs (0,0,0,0) );
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
    }
}
