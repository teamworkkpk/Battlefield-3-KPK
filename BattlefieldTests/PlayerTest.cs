namespace BattlefieldTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BattleFiled;
    using BattleFiled.SaveLoad;

    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        // [ExpectedException(typeof(ArgumentNullException), "Error: Memento player name cannot be null or empty!")]
        public void PlayerIfSetterAcceptNull()
        {
            string name = "Ivan";
            string message = string.Empty;
            Player player = new Player(name);
            Assert.AreEqual(name, player.Name);
            player.MovesCount = 1;
            Assert.AreEqual(1, player.MovesCount);
            try
            {
                player.Name = null;
            }
            catch (ArgumentNullException ex)
            {
                message = ex.ToString();
            }
            StringAssert.Contains(message, "Error");
        }


        [TestMethod]
        public void PlayerIfSetterAcceptEmptyString()
        {
            string name = "Ivan";
            string message = string.Empty;
            Player player = new Player(name);
            Assert.AreEqual(name, player.Name);
            player.MovesCount = 1;
            Assert.AreEqual(1, player.MovesCount);
            try
            {
                player.Name = string.Empty;
            }
            catch (ArgumentNullException ex)
            {
                message = ex.ToString();
            }
            StringAssert.Contains(message, "Error");

        }

        [TestMethod]
        // [ExpectedException(typeof(ArgumentNullException), "Error: Memento player name cannot be null or empty!")]
        public void PlayerIfSetterAcceptEmptyStringSpace()
        {
            string name = "Ivan";
            string message = string.Empty;
            Player player = new Player(name);
            Assert.AreEqual(name, player.Name);
            try
            {
                player.Name = " ";
            }
            catch (ArgumentNullException ex)
            {
                message = ex.ToString();
            }
            StringAssert.Contains(message, "Error");

        }

        [TestMethod]
        //   [ExpectedException(typeof(ArgumentOutOfRangeException), "Error: Memento detonated mines count cannot less than zero!")]
        public void PlayerDetonatedMinesIfSetterAcceptNegativeValue()
        {
            string name = "Ivan";
            string message = string.Empty;
            Player player = new Player(name);
            Assert.AreEqual(name, player.Name);
            try
            {
                player.DetonatedMines = -1;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                message = "Error:" + ex.ToString();
                Console.WriteLine(message);
            }
            StringAssert.Contains(message, "Error");
        }

        [TestMethod]
        //   [ExpectedException(typeof(ArgumentOutOfRangeException), "Error: Memento detonated mines count cannot greater than 100!")]
        public void PlayerDetonatedMinesIfSetterAcceptGreaterThanMaxValue()
        {
            string name = "Ivan";
            string message = string.Empty;
            Player player = new Player(name);
            Assert.AreEqual(name, player.Name);

            try
            {
                player.DetonatedMines = 101;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                message = "Error " + ex.ToString();
            }
            StringAssert.Contains(message, "Error");
        }

        [TestMethod]
        public void PlayerDetonatedMinesSetterTest()
        {
            string name = "Ivan";
            int expected = 10;
            Player player = new Player(name);
            player.DetonatedMines = expected;
            Assert.AreEqual(expected, player.DetonatedMines);
        }

        [TestMethod]
        public void PlayerNameSetterTest()
        {
            string name = "Ivan";
            Player player = new Player(name);
            Assert.AreEqual(name, player.Name);
        }

        [TestMethod]
        public void PlayerMovesLessThanZero()
        {
            string message = string.Empty;
            string name = "Ivan";
            Player player = new Player(name);
            Assert.AreEqual(name, player.Name);
            player.MovesCount = 1;
            Assert.AreEqual(1, player.MovesCount);

            try
            {
                player.MovesCount = -1;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                message = "Error " + ex.ToString();
            }
            StringAssert.Contains(message, "Error");
        }

        [TestMethod]
        public void PlayerMovesGreaterThanZero()
        {
            Player player = new Player("Ivan");
            player.MovesCount = 1;
            Assert.AreEqual(1, player.MovesCount);
        }

        [TestMethod]
        public void PlayerToString()
        {
            Player player = new Player("Ivan");
            player.DetonatedMines = 12;
            player.AddDetonatedMines(1);
            player.AddMove();
            Assert.AreEqual("Player: Ivan, Detonated mines: 13, Moves: 1", player.ToString(), "Equals");
        }

        [TestMethod]
        public void PlayerAddDetonatedMines()
        {
            Player player = new Player("Ivan");
            player.DetonatedMines = 12;
            player.AddDetonatedMines(1);
            Assert.AreEqual(13, player.DetonatedMines);
        }

        [TestMethod]
        public void PlayerAddMove()
        {
            Player player = new Player("Ivan");
            player.AddMove();
            Assert.AreEqual(1, player.MovesCount);
        }

        [TestMethod]
        public void PlayerSaveMemento()
        {
            Player player = new Player("Ivan");
            player.DetonatedMines = 12;
            player.MovesCount = 11;
            MementoPlayer mementoPlayer = player.SaveMemento();

            Assert.AreEqual("Ivan", mementoPlayer.Name);
            Assert.AreEqual(12, mementoPlayer.DetonatedMines);
            Assert.AreEqual(11, mementoPlayer.MovesCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlayerRestoreMemento()
        {
            Player player = new Player("Ivan");
            player.DetonatedMines = 12;
            player.MovesCount = 11;
            MementoPlayer mementoPlayer = new MementoPlayer();

            player.LoadMemento(mementoPlayer);
        }
    }
}
