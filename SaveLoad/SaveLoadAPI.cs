namespace BattleFiled.SaveLoad
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using System.Runtime.Serialization;

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
            Console.WriteLine("Game successfully saved!");
        }

        /// <summary>
        /// Deserialize the game state from the hard drive
        /// </summary>
        public void LoadGame()
        {   
            SaveLoadAPI gameState;

            using(StreamReader reader = new StreamReader(SavePath))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveLoadAPI));

                gameState = xmlSerializer.Deserialize(reader) as SaveLoadAPI;
            }

            if (gameState == null)
            {
                throw new ArgumentNullException("Saved state cannot be null");
            }

            this.MementoField = gameState.MementoField;
            this.MementoPlayer = gameState.MementoPlayer;
            
            Console.WriteLine("Game successfully loaded!");
        }
        
    }
}
