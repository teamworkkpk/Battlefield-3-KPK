namespace BattleFiled
{
    using System;

    class KeyEventArgs : EventArgs
    {
        private readonly ConsoleKey pressedKey;

        public KeyEventArgs(ConsoleKey pressedKey)
        {
            this.pressedKey = pressedKey;
        }
    }
}
