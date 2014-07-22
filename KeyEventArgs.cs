namespace BattleFiled
{
    using System;

    public class KeyEventArgs : EventArgs
    {
        private readonly ConsoleKey pressedKey;

        public KeyEventArgs(ConsoleKey pressedKey)
        {
            this.pressedKey = pressedKey;
        }

        public ConsoleKey PressedKey 
        {
            get
            {
                return this.pressedKey;
            }
        }
    }
}