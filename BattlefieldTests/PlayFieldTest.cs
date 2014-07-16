using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleFiled;
using BattleFiled.Cells;
using BattleFiled.SaveLoad;

namespace BattleFieldTests
{
    [TestClass]
    public class PlayFieldTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cells array cannot be null")]
        public void InitializeEmptyFieldThrowsExeptionIfCellsIsNull()
        {
            Playfield testField = Playfield.Instance;
            testField.InitializeEmptyField();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cells array cannot be null")]
        public void PlaceMinesThrowsExeptionIfCellsIsNull()
        {
            Playfield testField = Playfield.Instance;
            testField.PlaceMines();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cells array cannot be null")]
        public void SaveThrowsExeptionIfCellsIsNull()
        {
            Playfield testField = Playfield.Instance;
            testField.SaveMemento();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cells array cannot be null")]
        public void ToStringThrowsExeptionIfCellsIsNull()
        {
            Playfield testField = Playfield.Instance;
            testField.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cells array cannot be null")]
        public void InitializeEmptyFieldIfAcceptNullCellValues()
        {
            Playfield testField = Playfield.Instance;
            testField.ToString();
        } 

        [TestMethod]
        public void TestIfFieldWithSizeSixIsSetWithCorrectSize()
        {
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);

            Assert.AreEqual(6, testField.PlayfieldSize, string.Format("Expected result true that field initialized with size 6 returns 6. Received {0}", testField.PlayfieldSize));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Cells array size must be between 2 and 10")]
        public void SetFieldSizeThrowsExpetionIfFieldSizeIsLessThanLowerBound()
        {
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Cells array size must be between 2 and 10")]
        public void SetFieldSizeThrowsExpetionIfFieldSizeIsHigherThanHigherBound()
        {
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(11);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Load method cannot accept null values")]
        public void LoadMethodThrowsExeptionIfNullArgumentIsPassed()
        {
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(5);
            testField.InitializeEmptyField();

            testField.LoadMemento(null);
        }        

        [TestMethod]
        public void SaveMethodCannotReturnNull()
        {
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(5);
            testField.InitializeEmptyField();
            testField.PlaceMines();                    

            Assert.IsNotNull(testField.SaveMemento(), "Zero-based playfield backup cannot be null");
        }

        [TestMethod]        
        public void SaveMethodCannotReturnMementoWithNullZeroBaseArray()
        {
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(5);
            testField.InitializeEmptyField();
            testField.PlaceMines();

            MementoField memento = new MementoField();
            memento = testField.SaveMemento();
            
            Assert.IsNotNull(memento.ZeroBasedPlayField, "Zero-based playfield backup cannot be null");
        }

        [TestMethod]       
        public void InitializeEmptyFieldContainsOnlyEmptyCells()
        {
            bool nonEmptyCellTypeFound = false;
            var playField = Playfield.Instance;
            playField.SetFieldSize(5);
            playField.InitializeEmptyField();

            foreach (ICell cell in playField)
            {
                if (cell.CellType != CellType.EmptyCell)
                {
                    nonEmptyCellTypeFound = true;
                }
            }
           
            Assert.IsFalse(nonEmptyCellTypeFound, "Initializedfield must contain only empty cells");
        }



        
    }
}
