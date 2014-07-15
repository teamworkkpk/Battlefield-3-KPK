using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattlefieldTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BattleFiled;
    using BattleFiled.Cells;
    using BattleFiled.SaveLoad;

    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Error: Memento player name cannot be null or empty!")]
        public void PlayerIfSetterAcceptNull() 
        {
            Player player = new Player(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Error: Memento player name cannot be null or empty!")]
        public void PlayerIfSetterAcceptEmptyString()
        {
            Player player = new Player("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Error: Memento player name cannot be null or empty!")]
        public void PlayerIfSetterAcceptEmptyStringSpace()
        {
            Player player = new Player(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Error: Memento detonated mines count cannot less than zero!")]
        public void PlayerDetonatedMinesIfSetterAcceptNegativeValue() 
        {
            Player player = new Player("Ivan");
            player.DetonatedMines = -1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Error: Memento detonated mines count cannot greater than 100!")]
        public void PlayerDetonatedMinesIfSetterAcceptGreaterThanMaxValue()
        {
            Player player = new Player("Ivan");
            player.DetonatedMines = 101;
        }
     
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Error: Memento moves count cannot less than zero!")]
        public void PlayerMovesLessThanZero()
        {
            Player player = new Player("Ivan");
            player.MovesCount = -1;
        }

        [TestMethod]
        public void PlayerToString()
        {
            Player player = new Player("Ivan");
            player.DetonatedMines = 12;
            player.AddMove();
            Assert.AreEqual("Player: Ivan, Detonated mines: 12, Moves: 1", player.ToString(), "Equals");
        }


    }
}
