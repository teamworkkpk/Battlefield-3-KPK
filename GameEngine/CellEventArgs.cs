namespace BattleFiled.GameEngine
{
    using System;

    public class CellEventArgs : EventArgs
    {
        private ICell target;

        public CellEventArgs(ICell target)
        {
            this.Target = target;
        }

        public ICell Target
        {
            get 
            {
                return this.target;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Cannot set null Target.");
                }
                this.target = value;
            }
        }
    }
}