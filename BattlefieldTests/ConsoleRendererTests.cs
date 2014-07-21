using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleFiled.GameEngine;
using BattleFiled.Renderer;
using BattleFiled.Interfaces;

namespace BattlefieldTests
{
    //can't create test because every time engine is instanace it will run and wait for user input
    //must find a workaround

    [TestClass]
    public class ConsoleRendererTests
    {
        //[TestMethod]
        //public void TestIfNewConsoleRendererIsCreatedWithEngine()
        //{
        //    FakeEngine gameEngine = new FakeEngine(6);
        //    ConsoleRenderer testRenderer = new ConsoleRenderer(gameEngine);

        //    Assert.AreEqual(testRenderer is ConsoleRenderer, true, string.Format("Expected testRenderer is ConsoleRenderer. Received {0}", 1));
        //}

        //private class FakeEngine : Engine
        //{
        //    public FakeEngine(int someRandomNumber)
        //    {
                
        //    }
        //}
    }
}
