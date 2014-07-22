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

            Assert.AreEqual(retVal, true, "Expected result true when StartScreen.HandleD3Key is called with D3 key. Returned false");
        }

        [TestMethod]
        public void TestIfD3KeyIsHandledByStartScreenHandleD2Key()
        {
            ConsoleKey key = ConsoleKey.D2;
            StartScreen startscreen = StartScreen.Instance;
            PrivateObject obj = new PrivateObject(startscreen);
            var retVal = obj.Invoke("HandleD2Key", key);

            Assert.AreEqual(retVal, true, "Expected result true when StartScreen.HandleD2Key is called with D2 key. Returned false");
        }

        [TestMethod]
        public void TestIfD3KeyIsHandledByStartScreenHandleD1Key()
        {
            ConsoleKey key = ConsoleKey.D1;
            StartScreen startscreen = StartScreen.Instance;
            PrivateObject obj = new PrivateObject(startscreen);
            var retVal = obj.Invoke("HandleD1Key", key);

            Assert.AreEqual(retVal, true, "Expected result true when StartScreen.HandleD1Key is called with D1 key. Returned false");
        }

        [TestMethod]
        public void TestIfNumpad3KeyIsHandledByStartScreenHandleD3Key()
        {
            ConsoleKey key = ConsoleKey.NumPad3;
            StartScreen startscreen = StartScreen.Instance;
            PrivateObject obj = new PrivateObject(startscreen);
            var retVal = obj.Invoke("HandleD3Key", key);

            Assert.AreEqual(retVal, true, "Expected result true when StartScreen.HandleD3Key is called with NumPad3. Returned false");
        }

        [TestMethod]
        public void TestIfNumpad2KeyIsHandledByStartScreenHandleD2Key()
        {
            ConsoleKey key = ConsoleKey.NumPad2;
            StartScreen startscreen = StartScreen.Instance;
            PrivateObject obj = new PrivateObject(startscreen);
            var retVal = obj.Invoke("HandleD2Key", key);

            Assert.AreEqual(retVal, true, "Expected result true when StartScreen.HandleD2Key is called with NumPad2. Returned false");
        }

        [TestMethod]
        public void TestIfNumpad1KeyIsHandledByStartScreenHandleD1Key()
        {
            ConsoleKey key = ConsoleKey.NumPad1;
            StartScreen startscreen = StartScreen.Instance;
            PrivateObject obj = new PrivateObject(startscreen);
            var retVal = obj.Invoke("HandleD1Key", key);

            Assert.AreEqual(retVal, true, "Expected result true when StartScreen.HandleD1Key is called with NumPad1. Returned false");
        }

        [TestMethod]
        public void TestIfStartScreenHandleD3KeyRerunsFalseWhenSomeOtherKeyThanD3OrNumPad3IsChosen()
        {
            ConsoleKey key = ConsoleKey.NumPad1;
            StartScreen startscreen = StartScreen.Instance;
            PrivateObject obj = new PrivateObject(startscreen);
            var retVal = obj.Invoke("HandleD3Key", key);

            Assert.AreEqual(retVal, false, "Expected result false when StartScreen.HandleD3Key is called with different key than D3 or NumPad3. Returned true");
        }

        [TestMethod]
        public void TestIfStartScreenHandleD2KeyRerunsFalseWhenSomeOtherKeyThanD2OrNumPad2IsChosen()
        {
            ConsoleKey key = ConsoleKey.NumPad1;
            StartScreen startscreen = StartScreen.Instance;
            PrivateObject obj = new PrivateObject(startscreen);
            var retVal = obj.Invoke("HandleD2Key", key);

            Assert.AreEqual(retVal, false,"Expected result false when StartScreen.HandleD2Key is called with different key than D2 or NumPad2. Returned true");
        }

        [TestMethod]
        public void TestIfStartScreenHandleD1KeyRerunsFalseWhenSomeOtherKeyThanD1OrNumPad1IsChosen()
        {
            ConsoleKey key = ConsoleKey.NumPad2;
            StartScreen startscreen = StartScreen.Instance;
            PrivateObject obj = new PrivateObject(startscreen);
            var retVal = obj.Invoke("HandleD1Key", key);

            Assert.AreEqual(retVal, false, "Expected result false when StartScreen.HandleD1Key is called with different key than D1 or NumPad1. Returned true");
        }

        [TestMethod]
        public void TestIfStartScreenSetChoiseIsExecutedWithD1Key()
        {
            StartScreen startscreen = StartScreen.Instance;
            startscreen.SetChoise(ConsoleKey.D1);

            Assert.AreEqual(true, true, "StartScreen.SetChoise() didn't execute with D1 key passed");
        }

        [TestMethod]
        public void TestIfStartScreenSetChoiseIsExecutedWithD2Key()
        {
            StartScreen startscreen = StartScreen.Instance;
            startscreen.SetChoise(ConsoleKey.D2);

            Assert.AreEqual(true, true, "StartScreen.SetChoise() didn't execute with D2 key passed");
        }

        [TestMethod]
        public void TestIfStartScreenSetChoiseIsExecutedWithD3Key()
        {
            StartScreen startscreen = StartScreen.Instance;
            startscreen.SetChoise(ConsoleKey.D3);

            Assert.AreEqual(true, true, "StartScreen.SetChoise() didn't execute with D3 key passed");
        }
    }
}
