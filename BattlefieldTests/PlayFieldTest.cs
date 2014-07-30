namespace BattleFieldTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BattleFiled;
    using BattleFiled.Cells;
    using BattleFiled.SaveLoad;

    [TestClass]
    public class PlayFieldTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cells array cannot be null")]
        public void InitializeEmptyFieldThrowsExeptionIfCellsIsNull()
        {
            //Engine.StartMenu.IsQuitGameChosen = false;
            //Engine.StartMenu.IsStartGameChosen = true;
            //Engine gameEngine = new Engine();
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

        [TestMethod]
        public void CheckIfPlayfieldCellCanBeSetWithNewValue()
        {
            var playField = Playfield.Instance;
            playField.SetFieldSize(5);
            playField.InitializeEmptyField();
            BombCell bombCell = new BombCell(5);

            playField[0, 0] = bombCell;

            Assert.AreEqual(playField[0, 0].Equals(bombCell), true, string.Format("Exprected that cell in position 0,0 in the field is set as bombcell with size 5. Received {0}", playField[0, 0].Equals(bombCell)));
        }

        [TestMethod]
        public void CheckIfFieldToStringMethodReturnsCorrectString()
        {
            var playField = Playfield.Instance;
            playField.SetFieldSize(5);
            playField.InitializeEmptyField();
            BombCell bombCellOne = new BombCell(1);
            BombCell bombCellTwo = new BombCell(2);
            BombCell bombCellThree = new BombCell(3);
            BombCell bombCellFour = new BombCell(4);
            BombCell bombCellFive = new BombCell(5);

            playField[0, 0] = bombCellOne;
            playField[1, 1] = bombCellTwo;
            playField[2, 2] = bombCellThree;
            playField[3, 3] = bombCellFour;
            playField[4, 4] = bombCellFive;
            string playfieldStringExpected = playField.ToString();

            Assert.AreEqual(playfieldStringExpected.Equals(playField.ToString()), true, string.Format("Exprected that cell in position 0,0 in the field is set as bombcell with size 5. Received {0} ",playfieldStringExpected));
        }

        
    }
}
