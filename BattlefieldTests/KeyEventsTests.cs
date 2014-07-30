namespace BattlefieldTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BattleFiled;

    [TestClass]
    public class KeyEventsTests
    {
        [TestMethod]
        public void TestIfCorrectKeyIsSetWhenKeyEventIsCreated()
        {
            KeyEventArgs testEvent = new KeyEventArgs(ConsoleKey.D9);

            Assert.AreEqual(testEvent.PressedKey, ConsoleKey.D9, string.Format("The created event is not set with ConsoleKey.D9 as expected. Instead set key {0}", testEvent.PressedKey));
        }
    }
}
