namespace BattleFiled.SaveLoad
{
    using System;
    using BattleFiled.Cells;
    
    /// <summary>
    /// Instance of this class saves the state of Field.cs instance.
    /// The class is used together with SaveLoadInterface.cs.
    /// </summary>
    public class MementoField
    {
        private const int MIN_FIELD_SIZE = 2;
        private const int MAX_FIELD_SIZE = 10;

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
                if (value < MIN_FIELD_SIZE || value > MAX_FIELD_SIZE)
                {
                    throw new ArgumentException("Field dimensions cannot be 0");
                }

                this.fieldDimension = value;
            }
        }
    }
}
