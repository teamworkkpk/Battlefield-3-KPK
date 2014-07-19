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
        public void SoundsTestIfNullPathCanBeSet()
        {
            string pathToInvalidMoveSound = "";
            string pathToDetonatedBombSound = string.Empty;
            string pathToPositionChangedSound = null;

            Sounds player = new Sounds(pathToInvalidMoveSound, pathToDetonatedBombSound, pathToPositionChangedSound);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SoundsTestIfFileIsFoundAtPath()
        {
            string pathToInvalidMoveSound = "../../Sounds/Resources/invalid.wav";
            string pathToDetonatedBombSound = "../../Sounds/Resources/boom.wav";
            string pathToPositionChangedSound = "../../Sounds/Resources/not-valid-file.wav";

            Sounds player = new Sounds(pathToInvalidMoveSound, pathToDetonatedBombSound, pathToPositionChangedSound);

            player.PlayPositionChanged();
        }
    }
}
