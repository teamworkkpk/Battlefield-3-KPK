// <copyright file="ConsoleView.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

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

