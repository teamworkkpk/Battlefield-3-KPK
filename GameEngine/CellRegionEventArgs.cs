namespace BattleFiled.GameEngine
{
    using System;

    public class CellRegionEventArgs : EventArgs
    {
        private int regionStartX;
        private int regionStartY;
        private int regionEndX;
        private int regionEndY;

        public CellRegionEventArgs(int regionStartX, int regionStartY, int regionEndX, int regionEndY)
        {
            this.RegionStartX = regionStartX;
            this.RegionStartY = regionStartY;
            this.RegionEndX = regionEndX;
            this.RegionEndY = regionEndY;
        }

        public int RegionStartX
        {
            get
            {
                return regionStartX;
            }
            private set 
            { 
                if (value < 0)
                {
                    throw new ArgumentNullException("Cannot set region start x position.");
                }
                regionStartX = value; 
            }
        }
        public int RegionStartY
        {
            get
            {
                return regionStartY;
            }
            private set 
            { 
                if (value < 0)
                {
                    throw new ArgumentNullException("Cannot set region start y position.");
                }
                regionStartY = value; 
            }
        }
        public int RegionEndX
        {
            get
            {
                return regionEndX;
            }
            private set 
            { 
                if (value < 0)
                {
                    throw new ArgumentNullException("Cannot set region End x position.");
                }
                regionEndX = value; 
            }
        }
        public int RegionEndY
        {
            get
            {
                return regionEndY;
            }
            private set 
            { 
                if (value < 0)
                {
                    throw new ArgumentNullException("Cannot set region End y position.");
                }
                regionEndY = value; 
            }
        }
    }
}