namespace BattleFiled
{
    using System;
    using BattleFiled.GameEngine;
    using BattleFiled.SaveLoad;
    using BattleFiled.Cells;

    class BattleFieldMain
    {
        static void Main()
        {
            Engine gameEngine = Engine.Instance;
            gameEngine.Start();             
        }
    }
}

