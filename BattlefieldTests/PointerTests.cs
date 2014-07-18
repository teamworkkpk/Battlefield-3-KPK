using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleFiled;

namespace BattlefieldTests
{
    [TestClass]
    public class PointerTests
    {
        [TestMethod]
        public void CheckIfPointerIsInitializedCorrectWithGiveXandY()
        {
            Pointer pointer = new Pointer(5, 11);

            Assert.AreEqual((pointer.X == 5 && pointer.Y == 11), true, string.Format("Expected pointer with X=5 and Y=11. Received pointer with X={0} and Y={1}", pointer.X, pointer.Y));
        }

        [TestMethod]
        public void CheckIfPointerXIsSetCorrectlyAfterPointerInitialization()
        {
            Pointer pointer = new Pointer(5, 11);
            pointer.X = 7;

            Assert.AreEqual(pointer.X == 7 , true, string.Format("Expected pointer with X=7. Received pointer with X={0}", pointer.X));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Pointer x coordinate cannot be less than 0")]
        public void CheckIfPointerThrowsCorrectExeptionWhenIncorrectXIsSetAfterPointerCreation()
        {
            Pointer pointer = new Pointer(-5, 11);
       
        }

        [TestMethod]
        public void CheckIfPointerYIsSetCorrectlyAfterPointerInitialization()
        {
            Pointer pointer = new Pointer(5, 11);
            pointer.Y = 7;

            Assert.AreEqual(pointer.Y == 7, true, string.Format("Expected pointer with Y=7. Received pointer with Y={0}", pointer.Y));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Pointer y coordinate cannot be less than 0")]
        public void CheckIfPointerThrowsCorrectExeptionWhenIncorrectYIsSetAfterPointerCreation()
        {
            Pointer pointer = new Pointer(5, -11);

        }
    }
}
