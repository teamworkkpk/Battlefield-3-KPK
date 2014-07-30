// <copyright file="MementoField.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

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
        /// <summary>
        /// Constant for min field size.
        /// </summary>
        private const int MIN_FIELD_SIZE = 2;

        /// <summary>
        /// Constant for max field size.
        /// </summary>
        private const int MAX_FIELD_SIZE = 10;

        /// <summary>
        /// Keeps saved state of the field.
        /// </summary>
        private Cell[] zeroBasedPlayField;

        /// <summary>
        /// Keeps field dimensions.
        /// </summary>
        private int fieldDimension;

        /// <summary>
        /// Gets or sets ZeroBasedPlayfield
        /// The class is used together with SaveLoadInterface.cs.
        /// </summary>
        /// <value>Field cells values.</value>
        public Cell[] ZeroBasedPlayField
        {
            get
            {
                return this.zeroBasedPlayField;
            }

            set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Serialized field cannot be null!");
                }

                this.zeroBasedPlayField = value;
            }
        }

        /// <summary>
        /// Gets or sets field dimension
        /// The class is used together with SaveLoadInterface.cs.
        /// </summary>
        /// <value>Keeps field dimension.</value>
        public int FieldDimension
        {
            get 
            {
                return this.fieldDimension; 
            }

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
