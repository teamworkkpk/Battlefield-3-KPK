namespace BattlefieldTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BattleFiled;
    using BattleFiled.GameEngine;

    [TestClass]
    public class PlayFieldChangeEventArgsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Null Playfield provided.")]
        public void PlayFieldChangeEventArgsTestIfAcceptNull()
        {
            string message = string.Empty;

            Playfield playfield = Playfield.Instance;
            PlayfieldChangedEventArgs pl = new PlayfieldChangedEventArgs(null);
        }

        [TestMethod]
        public void PlayFieldChangeEventArgsTestGetPlayFieldSize()
        {
            Playfield playfield = Playfield.Instance;
            playfield.SetFieldSize(10);
            PlayfieldChangedEventArgs pl = new PlayfieldChangedEventArgs(playfield);
            Assert.AreEqual(pl.PlayfieldSize, 10);
        }

        [TestMethod]
        public void PlayFieldChangeEventArgsTestGetNewPlayField()
        {
            Playfield playfield = Playfield.Instance;
            playfield.SetFieldSize(10);
            PlayfieldChangedEventArgs pl = new PlayfieldChangedEventArgs(playfield);
            Assert.AreEqual(pl.NewPlayField, playfield);

        }
    }
}
