namespace BattlefieldTests
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BattleFiled.Sounds;

    [TestClass]
    public class SoundsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cannot set null path.")]
        public void SoundsTestIfInvalidSelectionNullPathCanBeSet()
        {
            string pathToInvalidSelectionSound = "";
            string pathToDetonatedBombSound = "../../Sounds/Resources/boom.wav";
            string pathToPositionChangedSound = "../../Sounds/Resources/move.wav";

            Sounds player = new Sounds(pathToInvalidSelectionSound, pathToDetonatedBombSound, pathToPositionChangedSound);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cannot set null path.")]
        public void SoundsTestIfDetonatedBombNullPathCanBeSet()
        {
            string pathToInvalidSelectionSound = "../../Sounds/Resources/invalid.wav";
            string pathToDetonatedBombSound = string.Empty;
            string pathToPositionChangedSound = "../../Sounds/Resources/move.wav";

            Sounds player = new Sounds(pathToInvalidSelectionSound, pathToDetonatedBombSound, pathToPositionChangedSound);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cannot set null path.")]
        public void SoundsTestIfPositionChangedNullPathCanBeSet()
        {
            string pathToInvalidSelectionSound = "../../Sounds/Resources/invalid.wav";
            string pathToDetonatedBombSound = "../../Sounds/Resources/boom.wav";
            string pathToPositionChangedSound = null;

            Sounds player = new Sounds(pathToInvalidSelectionSound, pathToDetonatedBombSound, pathToPositionChangedSound);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SoundsTestIfSelectionSoundIsFoundAtPath()
        {
            string pathToInvalidSelectionSound = "../../Sounds/Resources/not-valid.wav";
            string pathToDetonatedBombSound = "../../Sounds/Resources/boom.wav";
            string pathToPositionChangedSound = "../../Sounds/Resources/move.wav";

            Sounds player = new Sounds(pathToInvalidSelectionSound, pathToDetonatedBombSound, pathToPositionChangedSound);

            player.PlayInvalidSelection();
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SoundsTestIfBombSoundIsFoundAtPath()
        {
            string pathToInvalidSelectionSound = "../../Sounds/Resources/invalid.wav";
            string pathToDetonatedBombSound = "../../Sounds/Resources/not-valid-boom.wav";
            string pathToPositionChangedSound = "../../Sounds/Resources/move.wav";

            Sounds player = new Sounds(pathToInvalidSelectionSound, pathToDetonatedBombSound, pathToPositionChangedSound);

            player.PlayDetonatedBomb();
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SoundsTestIfPositionSoundIsFoundAtPath()
        {
            string pathToInvalidSelectionSound = "../../Sounds/Resources/invalid.wav";
            string pathToDetonatedBombSound = "../../Sounds/Resources/boom.wav";
            string pathToPositionChangedSound = "../../Sounds/Resources/not-valid-move.wav";

            Sounds player = new Sounds(pathToInvalidSelectionSound, pathToDetonatedBombSound, pathToPositionChangedSound);

            player.PlayPositionChanged();
        }
    }
}
