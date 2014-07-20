namespace BattlefieldTests
{
    using System;
    using BattleFiled;
    using BattleFiled.SaveLoad;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SaveLoadAPITests
    {
        [TestMethod]
        public void SaveLoadApiSaveGameTest()
        {
            var saveLoadApi = new SaveLoadAPI();
            saveLoadApi.SaveGame();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SaveLoadApiLoadGameTest()
        {
            var saveLoadApi = new SaveLoadAPI();
            saveLoadApi.LoadGame();
        }
    }
}
