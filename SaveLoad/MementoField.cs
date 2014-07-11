namespace BattleFiled.SaveLoad
{
    using System;

    public class MementoField
    {
        private ICell[,] playFieldBackup;

        public ICell PlayFieldBackup { get; set; }
    }
}
