namespace BattleFiled.SaveLoad
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using System.Xml;
    using System.Runtime.Serialization;
    using System.Threading;

    /// <summary>
    /// The class is used like interface for saving state of game objects.
    /// The class is used together with MementoField.cs and MementoPlayer.cs
    /// The class is used for serialization of the game state.
    /// </summary>
    public class SaveLoadAPI 
    {
        private const string SavePath = @"..\..\saveGameState.xml";
               
        public MementoField MementoField { get; set; }

        public MementoPlayer MementoPlayer { get; set; }
        
        /// <summary>
        /// Serialize the game state on the hard drive
        /// </summary>
        public void SaveGame()
        {
            using(StreamWriter writer = new StreamWriter(SavePath))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveLoadAPI));

                xmlSerializer.Serialize(writer, this);
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Game successfully saved!");
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Deserialize the game state from the hard drive
        /// </summary>
        public void LoadGame()
        {
            if (!File.Exists(SavePath))
            {
                throw new FileNotFoundException("Error: saveGameState.xml not found!");
            }

            SaveLoadAPI gameState;

            using (StreamReader reader = new StreamReader(SavePath))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveLoadAPI));
                gameState = xmlSerializer.Deserialize(reader) as SaveLoadAPI;
            }

            if (gameState == null)
            {
                throw new ArgumentNullException("Saved state cannot be null");
            }

            //Mementofield can only be initialized if the size of the current playfield equals the size of the saved one.
            //if (gameState.MementoField.FieldDimension != Playfield.Instance.PlayfieldSize)
            //{
            //    throw new InvalidOperationException("Current PlayField size is different than the size of the saved object and it cannot be initialized.");

            //}

            this.MementoField = gameState.MementoField;
            this.MementoPlayer = gameState.MementoPlayer;

            
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Game successfully loaded!");
            Thread.Sleep(500);
        }
    }
}
