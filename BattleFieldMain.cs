namespace BattleFiled
{
    using System;

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

