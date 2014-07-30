// <copyright file="StartScreen.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.StartMenu
{    
    using System;
    using System.IO;

    /// <summary>
    /// Class instance provides draw method for drawing all cell types on the console.
    /// </summary>
    public class StartScreen
    {
        /// <summary>
        /// Indicates path to the startup logo.
        /// </summary>
        private const string GAME_LOGO_PATH = @"..\..\StartMenu\gameLogo.txt";

        /// <summary>
        /// Console padding left.
        /// </summary>
        private const int CONSOLE_PADDING_LEFT = 10;

        /// <summary>
        /// Console padding top.
        /// </summary>
        private const int CONSOLE_PADDING_TOP = 8;

        /// <summary>
        /// Singleton instance of the class.
        /// </summary>
        private static StartScreen startScreenInstance;

        /// <summary>
        /// Prevents a default instance of the StartScreen class from being created.
        /// </summary>
        private StartScreen()
        {
        }

        /// <summary>
        /// Gets singleton instance of the class.
        /// </summary>
        /// <value>Start screen singleton instance.</value>
        public static StartScreen Instance
        {
            get
            {
                if (startScreenInstance == null)
                {
                    startScreenInstance = new StartScreen();
                }

                return startScreenInstance;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether start game is chosen.
        /// </summary>
        /// <value>Start game state.</value>
        public bool IsStartGameChosen { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether quit game is chosen.
        /// </summary>
        /// <value>Quit game state.</value>
        public bool IsQuitGameChosen { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether load game is chosen.
        /// </summary>
        /// <value>Load game state.</value>
        public bool IsLoadGameChosen { get; set; }

        /// <summary>
        /// Allow the user choosing an option : new game, load game or leave the game.
        /// </summary>
        /// <param name="pressedKey">Console key used for test purposes.</param>
        public void SetChoise(ConsoleKey pressedKey)
        {
            ////ConsoleKey pressedKey;
            bool keyHandled = false;

            ////Added as workaround for unit tests
            if (this.IsStartGameChosen == true || this.IsQuitGameChosen == true || this.IsLoadGameChosen == true)
            {
                return;
            }

            while (!keyHandled)
            {
                this.RenderStartUpScreen();

                keyHandled = this.HandleD1Key(pressedKey) || this.HandleD2Key(pressedKey) || this.HandleD3Key(pressedKey);

                if (!keyHandled)
                {
                    pressedKey = Console.ReadKey().Key;
                }
            }
        }

        /// <summary>
        /// Change the game state to new game.
        /// </summary>
        /// <param name="key">Console key.</param>
        /// <returns>True if the D1 key is pressed.</returns>
        private bool HandleD1Key(ConsoleKey key)
        {
            if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
            {
                this.IsStartGameChosen = true;
                Console.Clear();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Change the game state to load game.
        /// </summary>
        /// <param name="key">Console key.</param>
        /// <returns>True if the D2 key is pressed.</returns>
        private bool HandleD2Key(ConsoleKey key)
        {
            if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)
            {
                this.IsLoadGameChosen = true;
                Console.Clear();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Change the game state to quit game.
        /// </summary>
        /// <param name="key">Console key.</param>
        /// <returns>True if the D3 key is pressed.</returns>
        private bool HandleD3Key(ConsoleKey key)
        {
            if (key == ConsoleKey.D3 || key == ConsoleKey.NumPad3)
            {
                this.IsQuitGameChosen = true;
                Console.Clear();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Renders startup screen.
        /// </summary>
        private void RenderStartUpScreen()
        {
            string gameLogo = this.LoadGameLogo(GAME_LOGO_PATH);
            Console.WriteLine(gameLogo);

            Console.SetCursorPosition(CONSOLE_PADDING_LEFT, CONSOLE_PADDING_TOP);
            int cursorLeft = CONSOLE_PADDING_LEFT;
            int cursorTop = CONSOLE_PADDING_TOP;

            Console.WriteLine("1. StartGame");
            cursorTop++;

            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.WriteLine("2. Load Last Game");
            cursorTop++;

            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.WriteLine("3. Quit");
        }

        /// <summary>
        /// Loads the game logo from text file.
        /// </summary>
        /// <param name="logoPath">String path to the logo file.</param>
        /// <returns>Returns the logo as string.</returns>
        private string LoadGameLogo(string logoPath)
        {
            string logo = string.Empty;

            using (StreamReader reader = new StreamReader(logoPath))
            {
                logo = reader.ReadToEnd();
            }

            return logo;
        }
    }
}
