namespace BattlefieldTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BattleFiled.GameEngine;

    [TestClass]
    public class CellRegionEventTests
    {
        [TestMethod]
        public void TestIfCellRegionEventArgsIsSetWithCorrectStartXStartYEndXAndY()
        {
            CellRegionEventArgs testRegionEvent = new CellRegionEventArgs(1, 2, 3, 4);
            bool arePropertiesCorrect = testRegionEvent.RegionStartX == 1 && testRegionEvent.RegionStartY == 2
                                        && testRegionEvent.RegionEndX == 3 && testRegionEvent.RegionEndY == 4;

            Assert.AreEqual(arePropertiesCorrect, true, string.Format("Exprected CellRegionEvent with region start x = 1, region start y = 2, region end x = 3, region end y = 4. Received region start x = {0}, region start y = {1}, region end x = {2}, region end y = {3}", testRegionEvent.RegionStartX, testRegionEvent.RegionStartY, testRegionEvent.RegionEndX, testRegionEvent.RegionEndY));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "CellEventsArgs Cannot Be Set With Null Cell")]
        public void TestIfCellRegionEventArgsRegionStartXCreateWithNegativeNumberWillThrowArgumentNullException()
        {
            CellRegionEventArgs testRegionEvent = new CellRegionEventArgs(-1, 2, 3, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "CellEventsArgs Cannot Be Set With Null Cell")]
        public void TestIfCellRegionEventArgsRegionStartYCreateWithNegativeNumberWillThrowArgumentNullException()
        {
            CellRegionEventArgs testRegionEvent = new CellRegionEventArgs(1, -2, 3, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "CellEventsArgs Cannot Be Set With Null Cell")]
        public void TestIfCellRegionEventArgsRegionEndXCreateWithNegativeNumberWillThrowArgumentNullException()
        {
            CellRegionEventArgs testRegionEvent = new CellRegionEventArgs(1, 2, -3, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "CellEventsArgs Cannot Be Set With Null Cell")]
        public void TestIfCellRegionEventArgsRegionEndYCreateWithNegativeNumberWillThrowArgumentNullException()
        {
            CellRegionEventArgs testRegionEvent = new CellRegionEventArgs(1, 2, 3, -4);
        }
    }
}
