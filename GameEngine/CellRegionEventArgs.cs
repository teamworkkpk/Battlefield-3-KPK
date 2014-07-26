// <copyright file="CellRegionEventArgs.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.GameEngine
{
    using System;

    /// <summary>
    /// CellRegionEventArgs will handle event for group of cells.
    /// </summary>
    public class CellRegionEventArgs : EventArgs
    {
        /// <summary>
        /// Start X coordinate of the region.
        /// </summary>
        private int regionStartX;

        /// <summary>
        /// Start Y coordinate of the region.
        /// </summary>
        private int regionStartY;

        /// <summary>
        /// End X coordinate of the region.
        /// </summary>
        private int regionEndX;

        /// <summary>
        /// End Y coordinate of the region.
        /// </summary>
        private int regionEndY;

        /// <summary>
        /// Initializes a new instanace of the CellRegionEventArgs class.
        /// </summary>
        /// </summary>
        /// <param name="regionStartX">Start X coordinate of the region.</param>
        /// <param name="regionStartY">Start Y coordinate of the region.</param>
        /// <param name="regionEndX">End X coordinate of the region.</param>
        /// <param name="regionEndY">End Y coordinate of the region.</param>
        public CellRegionEventArgs(int regionStartX, int regionStartY, int regionEndX, int regionEndY)
        {
            this.RegionStartX = regionStartX;
            this.RegionStartY = regionStartY;
            this.RegionEndX = regionEndX;
            this.RegionEndY = regionEndY;
        }

        /// <summary>
        /// Gets the RegionStartX.
        /// </summary>
        /// /// <value>Integer regionStartX.</value>
        public int RegionStartX
        {
            get
            {
                return this.regionStartX;
            }

            private set 
            { 
                if (value < 0)
                {
                    throw new ArgumentNullException("Cannot set region start x position.");
                }

                this.regionStartX = value; 
            }
        }

        /// <summary>
        /// Gets the RegionStartY.
        /// </summary>
        /// /// <value>Integer regionStartY.</value>
        public int RegionStartY
        {
            get
            {
                return this.regionStartY;
            }

            private set 
            { 
                if (value < 0)
                {
                    throw new ArgumentNullException("Cannot set region start y position.");
                }

                this.regionStartY = value; 
            }
        }

        /// <summary>
        /// Gets the RegionEndX.
        /// </summary>
        /// /// <value>Integer regionEndX.</value>
        public int RegionEndX
        {
            get
            {
                return this.regionEndX;
            }

            private set 
            { 
                if (value < 0)
                {
                    throw new ArgumentNullException("Cannot set region End x position.");
                }

                this.regionEndX = value; 
            }
        }

        /// <summary>
        /// Gets the RegionEndY.
        /// </summary>
        /// /// <value>Integer regionEndY.</value>
        public int RegionEndY
        {
            get
            {
                return this.regionEndY;
            }

            private set 
            { 
                if (value < 0)
                {
                    throw new ArgumentNullException("Cannot set region End y position.");
                }

                this.regionEndY = value; 
            }
        }
    }
}