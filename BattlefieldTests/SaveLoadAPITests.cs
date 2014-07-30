namespace BattlefieldTests
{
    using System;
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
        public void SaveLoadApiLoadGameTest()
        {
            var saveLoadApi = new SaveLoadAPI();
            saveLoadApi.LoadGame();
        }
    }
}
