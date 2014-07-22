using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleFiled;
using BattleFiled.GameEngine;
using BattleFiled.Cells;

namespace BattlefieldTests
{
    [TestClass]
    public class CellEventsTests
    {
        [TestMethod]
        public void TestIfCellEventArgsIsSetWithCorrectCellOnInitialization()
        {
            CellEventArgs testCellEvents = new CellEventArgs(new BombCell(5));

            Assert.AreEqual(testCellEvents.Target.CellType.Equals(CellType.Bomb), true, string.Format("Expected CellEventArgs created with bomb type. Received {0}",testCellEvents.Target.CellType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "CellEventsArgs Cannot Be Set With Null Cell")]
        public void TestIfCellEventArgsCreateWithNullCellWillThrowArgumentNullException()
        {
            CellEventArgs testCellEvents = new CellEventArgs(null);
        }    
    }
}
