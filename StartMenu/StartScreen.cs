namespace BattleFiled.StartMenu
{
    using BattleFiled.Interfaces;
    using System;
    using System.IO;
    
    public class StartScreen
    {
        private const string gameLogoPath = @"..\..\StartMenu\gameLogo.txt";
        private static StartScreen startScreenInstance;
        private const int CONSOLE_PADDING_LEFT = 10;
        private const int CONSOLE_PADDING_TOP = 8;

        protected StartScreen()
        {            
        }

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

        public bool IsStartGameChosen { get; set; }

        public bool IsQuitGameChosen { get; set; }

        public bool IsLoadGameChosen { get; set; }

        public void SetChoise(ConsoleKey pressedKey)
        {
            //ConsoleKey pressedKey;
            bool keyHandled = false;

            while (!keyHandled)
            {
                RenderStartUpScreen();
                
                keyHandled = this.HandleD1Key(pressedKey) || this.HandleD2Key(pressedKey) || this.HandleD3Key(pressedKey);
                
                if(!keyHandled)
                {
                    pressedKey = Console.ReadKey().Key;
                }
            }
            
        }

        private bool HandleD1Key(ConsoleKey key)
        {
            if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
            {
                IsStartGameChosen = true;
                Console.Clear();
                return true;
            }
            return false;
        }

        private bool HandleD2Key(ConsoleKey key)
        {
            if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)
            {
                IsLoadGameChosen = true;
                Console.Clear();
                return true;
            }
            return false;
        }

        private bool HandleD3Key(ConsoleKey key)
        {
            if (key == ConsoleKey.D3 || key == ConsoleKey.NumPad3)
            {
                IsQuitGameChosen = true;
                Console.Clear();                
                return true;
            }
            return false;
        }

        private void RenderStartUpScreen()
        {
            string gameLogo = LoadGameLogo(gameLogoPath);
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
