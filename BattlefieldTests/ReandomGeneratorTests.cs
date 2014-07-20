namespace BattlefieldTests
{
    using System;
    using BattleFiled;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    [TestClass]
    public class ReandomGeneratorTests
    {
        [TestMethod]
        public void RandomGeneratorTest()
        {
            int min = 1;
            int max = 10;

            int randomNumber = RandomGenerator.GetRandomNumber(min, max);

            Assert.IsTrue(randomNumber >= min && randomNumber <= max);
        }
    }
}
