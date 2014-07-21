using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleFiled.CellViews;
using BattleFiled;
using System.IO;


namespace BattlefieldTests
{
    [TestClass]
    public class CellViewTests
    {
        [TestMethod]
        public void CheckIfConsoleViewIsCreatedCorrect()
        {
            ConsoleView testView = new ConsoleView(5, 5, ConsoleColor.Green, ConsoleColor.Black, '*');
            bool arePropertiesSetCorrectly = (testView.X == 5 && testView.Y == 5 && 
                                              testView.Foreground == ConsoleColor.Green &&
                                                testView.Background == ConsoleColor.Black && testView.Symbol == '*');

            Assert.AreEqual(arePropertiesSetCorrectly, true, string.Format("Expected to receive ConsoleView with x=5, y=5, foreground=green, background=black, symbol=*. Received x={0}, y={1}, foreground={2}, background={3}, symbol={4}", testView.X, testView.Y, testView.Foreground, testView.Background, testView.Symbol));
        }

        [TestMethod]
        public void CheckIfConsoleViewIsDrawCorrectly()
        {
            ConsoleView testView = new ConsoleView(0, 0, ConsoleColor.Black, ConsoleColor.White, '*');

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                testView.Draw();

                string expected =
                    string.Format("*");
                Assert.AreEqual<string>(expected, sw.ToString(), string.Format("Expected ConsoleView to draw \'*\' symbol. Received {0}",sw.ToString()));
            }
           
        }
    }
}
