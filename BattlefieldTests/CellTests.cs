namespace BattlefieldTests
{
    using System;
    using BattleFiled;
    using BattleFiled.Cells;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CellTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CellsTestIfEmptyCellXIsPositive()
        {
            var emptyCell = new EmptyCell();
            emptyCell.X = -1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CellsTestIfBombCellXIsPositive()
        {
            var bombCell = new BombCell();
            bombCell.X = -1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CellsTestIfBlownCellXIsPositive()
        {
            var blownCell = new BlownCell();
            blownCell.X = -1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CellsTestIfEmptyCellYIsPositive()
        {
            var cell = new EmptyCell();
            cell.Y = -1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CellsTestIfBombCellYIsPositive()
        {
            var cellBomb = new BombCell();
            cellBomb.Y = -1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CellsTestIfBlownCellYIsPositive()
        {
            var cellBlown = new BlownCell();
            cellBlown.Y = -1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CellsTestExceptionWhenBombSizeIsLessThanMinAllowed()
        {
            var bombCell = new BombCell();
            bombCell.BombSize = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CellsTestExceptionWhenBombSizeIsLessThanMaxAllowed()
        {
            var bombCell = new BombCell();
            bombCell.BombSize = 6;
        }

        [TestMethod]
        public void CellsTestIfEmptyCellIsCloned()
        {
            EmptyCell emptyCell = new EmptyCell();
            emptyCell.X = 2;
            emptyCell.Y = 3;
            emptyCell.Color = Color.White;
            emptyCell.CellView = CellView.Bomb1;

            ICell clonedEmptyCell = emptyCell.Clone();

            Assert.AreEqual(emptyCell.X, clonedEmptyCell.X, "X coordinate has not cloned.");
            Assert.AreEqual(emptyCell.Y, clonedEmptyCell.Y, "Y coordinate has not cloned.");
            Assert.AreEqual(emptyCell.Color, clonedEmptyCell.Color, "Cell Color has not cloned.");
            Assert.AreEqual(emptyCell.CellView, clonedEmptyCell.CellView, "CellView Color has not cloned.");
        }

        [TestMethod]
        public void CellsTestIfBombCellIsCloned()
        {
            int bombSize = 1;
            BombCell bombCell = new BombCell(bombSize);
            bombCell.X = 2;
            bombCell.Y = 3;
            bombCell.Color = Color.White;
            bombCell.CellView = CellView.Bomb1;

            ICell clonedBombCell = bombCell.Clone();

            Assert.AreEqual(bombCell.X, clonedBombCell.X, "X coordinate has not cloned.");
            Assert.AreEqual(bombCell.Y, clonedBombCell.Y, "Y coordinate has not cloned.");
            Assert.AreEqual(bombCell.Color, clonedBombCell.Color, "Cell Color has not cloned.");
            Assert.AreEqual(bombCell.CellView, clonedBombCell.CellView, "CellView Color has not cloned.");
        }

        [TestMethod]
        public void CellsTestIfBlowCellIsCloned()
        {
            BlownCell blownCell = new BlownCell();
            blownCell.X = 2;
            blownCell.Y = 3;
            blownCell.Color = Color.White;
            blownCell.CellView = CellView.Bomb1;

            ICell clonedBlownCell = blownCell.Clone();

            Assert.AreEqual(blownCell.X, clonedBlownCell.X, "X coordinate has not cloned.");
            Assert.AreEqual(blownCell.Y, clonedBlownCell.Y, "Y coordinate has not cloned.");
            Assert.AreEqual(blownCell.Color, clonedBlownCell.Color, "Cell Color has not cloned.");
            Assert.AreEqual(blownCell.CellView, clonedBlownCell.CellView, "CellView Color has not cloned.");
        }
    }
}
