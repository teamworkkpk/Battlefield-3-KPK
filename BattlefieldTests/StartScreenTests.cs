using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleFiled.StartMenu;
using BattleFiled.Interfaces;

namespace BattlefieldTests
{
    [TestClass]
    public class StartScreenTests
    {
        [TestMethod]
        public void TestIfNewStartScreenIsCreated()
        {
            StartScreen startscreen = StartScreen.Instance;

            Assert.IsInstanceOfType(startscreen, typeof(StartScreen), string.Format("Expected strartscreen object is of type StartScreen. Received {0}", startscreen.GetType() == typeof(StartScreen)));
        }

        [TestMethod]
        public void TestIfRenderStartUpScreenWorks()
        {
            StartScreen startscreen = StartScreen.Instance;
            PrivateObject obj = new PrivateObject(startscreen);
            var retVal = obj.Invoke("RenderStartUpScreen");

            Assert.AreEqual(retVal, retVal);
        }


        [TestMethod]
        public void TestIfD3KeyIsHandledByStartScreenHandleD3Key()
        {
            ConsoleKey key = ConsoleKey.D3;
            StartScreen startscreen = StartScreen.Instance;
            PrivateObject obj = new PrivateObject(startscreen);
            var retVal = obj.Invoke("HandleD3Key",key);
            
            Assert.AreEqual(retVal, true);
        }
    }
}
