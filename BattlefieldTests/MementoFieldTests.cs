namespace BattlefieldTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BattleFiled;
    using BattleFiled.Cells;
    using BattleFiled.SaveLoad;

    [TestClass]
    public class MementoFieldTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "ZeroBasedPlayField cannot be null")]
        public void ZeroBasedPlayfieldThrowsExecptionIfSetterAcceptNull()
        {
            MementoField mementoField = new MementoField();
            mementoField.ZeroBasedPlayField = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Filed dimensions must be between 2 and 10")]
        public void FieldDimensionsSetterThrowExeptionIfArgIsOutOfRange()
        {
            MementoField mementoField = new MementoField();
            mementoField.FieldDimension = 1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Filed dimensions must be between 2 and 10")]
        public void FieldDimensionsSetterThrowExeptionIfArgsIsOutOfRange()
        {
            MementoField mementoField = new MementoField();
            mementoField.FieldDimension = 11;
        }
    }
}
