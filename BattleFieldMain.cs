namespace BattleFiled
{
    using System;
    using BattleFiled.GameEngine;
    using BattleFiled.SaveLoad;

    class BattleFieldMain
    {
        static void Main()
        {
            Engine gameEngine = Engine.Instance;
            gameEngine.Start(); 
        }
    }
}

