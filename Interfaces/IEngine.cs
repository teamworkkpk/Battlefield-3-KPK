// <copyright file="ConsoleView.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.Interfaces
{
    using System;
    using System.Linq;

    /// <summary>
    /// Functionality to start and stop some process
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Stops a process
        /// </summary>
        void Stop();

        /// <summary>
        /// Starts a process
        /// </summary>
        void Start();
    }
}
