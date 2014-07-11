namespace BattleFiled.SaveLoad
{
    using System;
    using BattleFiled.Cells;

    public class MementoField
    {
        private Cell[] zeroBasedPlayField;
        
        private int fieldDimension;        

        public Cell[] ZeroBasedPlayField
        {
            get { return this.zeroBasedPlayField; }
            set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Serialized field cannot be null!");
                }
                this.zeroBasedPlayField = value;
            }
        }

        public int FieldDimension
        {
            get { return this.fieldDimension; }
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Field dimensions cannot be 0");
                }

                this.fieldDimension = value;
            }
        }
    }
}
