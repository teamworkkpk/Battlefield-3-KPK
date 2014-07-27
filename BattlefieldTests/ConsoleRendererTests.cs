using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleFiled.GameEngine;
using BattleFiled.Renderer;
using BattleFiled.Interfaces;
using System.IO;

namespace BattlefieldTests
{
    //can't create test because every time engine is instanace it will run and wait for user input
    //must find a workaround

    [TestClass]
    public class ConsoleRendererTests
    {
        [TestMethod]
        public void TestIfNewConsoleRendererIsCreatedWithEngine()
        {
            string testFieldSize = "6";

            Engine.FieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.StartMenu.IsStartGameChosen = true;            
            Engine gameEngine = Engine.Instance;

            ConsoleRenderer testRenderer = new ConsoleRenderer(gameEngine);
            Assert.AreEqual(testRenderer is ConsoleRenderer, true, string.Format("Expected testRenderer is ConsoleRenderer. Received {0}", 1));
        }

        [TestMethod]
        public void TestOnEnterKeyPressedReturnsTrue()
        {
            string testFieldSize = "6";


            Engine.FieldSizeUnitTestSetter = new StringReader(testFieldSize);
            Engine.StartMenu.IsStartGameChosen = true;
            Engine gameEngine = Engine.Instance;

            PrivateObject obj = new PrivateObject(gameEngine);
            var result = obj.Invoke("OnEnterKeyPressed", ConsoleKey.Enter);

            Assert.AreEqual(result, true, "");
        }
    }
}
