namespace BattleFiled
{
    using System;
    using BattleFiled.GameEngine;

    class BattleFieldMain
    {
        static void Main()
        {
            Engine gameEngine = Engine.Instance;
            gameEngine.Start();                         
        }
    }
}

