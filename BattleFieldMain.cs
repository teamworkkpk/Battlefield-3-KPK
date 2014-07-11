namespace BattleFiled
{
    using System;
    using BattleFiled.SaveLoad;
    class BattleFieldMain
    {
        static Engine GetEngineInstance()
        {
            return new Engine();
        }

        static void Main()
        {
            Engine gameEngine = GetEngineInstance();

            gameEngine.Run();
            
        }
    }
}

