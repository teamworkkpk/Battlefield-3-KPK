using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleFiled;

namespace BattleFieldTests
{
    [TestClass]
    public class FieldTests
    {
        [TestMethod]
        public void TestIfFieldWithSizeSixIsSetWithCorrectSize()
        {
            Playfield testField = Playfield.Instance;
            testField.SetFieldSize(6);

            Assert.AreEqual(6, testField.PlayfieldSize, string.Format("Expected result true that field initialized with size 6 returns 6. Received {0}", testField.PlayfieldSize));
        }
    }
}
