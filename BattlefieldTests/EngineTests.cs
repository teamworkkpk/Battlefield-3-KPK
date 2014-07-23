using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleFiled.GameEngine;
using System.IO;
using BattleFiled.Cells;
using System.Collections;
using System.Collections.Generic;

namespace BattlefieldTests
{
    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        //[ExpectedException(typeof(ArgumentNullException), "Engine cannot be set, because Quit Game is chosen")]
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
        //[ExpectedException(typeof(NotImplementedException), "Explosion with radius six is not handled")]
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

            Assert.AreEqual(isCurrentCellAtX0AndY0, true, "Expected that current cell is on coordinate 0,0. Received x={0} y={0}",currentCellX, currentCellY);
        }

        [TestMethod]
        public void TestIfPointerChangesItsCoordinateWithPlusOneOnChangeCurrentCell()
        {
            string testFieldSize = "6";


            Engine.fieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.startMenu.IsStartGameChosen = true;
            Engine gameEngine = new Engine();

            PrivateObject enginePrivateInstance = new PrivateObject(gameEngine);
            enginePrivateInstance.Invoke("ChangeCurrentCell",1,1);
            var pointer = enginePrivateInstance.GetFieldOrProperty("Pointer");
            PrivateObject enginePointer = new PrivateObject(pointer);
            int currentPointerX = (int)enginePointer.GetFieldOrProperty("X");
            int currentPointerY = (int)enginePointer.GetFieldOrProperty("Y");
            bool isCurrentCellAtX0AndY0 = (currentPointerX == 1 && currentPointerY == 1);

            Assert.AreEqual(isCurrentCellAtX0AndY0, true, "Expected that current cell is on coordinate 0,0. Received x={0} y={0}", currentPointerX, currentPointerY);
        }
    }
}
