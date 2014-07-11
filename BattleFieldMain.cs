namespace BattleFiled
{
    using System;

    class BattleFieldMain
    {
        static void Main()
        {
            Engine gameEngine = Engine.Instance;
            gameEngine.Start();            
        }
    }
}

