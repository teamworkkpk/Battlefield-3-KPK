namespace BattleFiled.SaveLoad
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using System.Runtime.Serialization;

    public class SaveLoadInterface 
    {
        private const string SavePath = @"..\..\saveGameState.xml";
               
        public MementoField MementoField { get; set; }

        public MementoPlayer MementoPlayer { get; set; }
        
        public void SaveGame()
        {
            using(StreamWriter writer = new StreamWriter(SavePath))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveLoadInterface));

                xmlSerializer.Serialize(writer, this);
            }
            Console.WriteLine("Game successfully saved!");
        }

        public void LoadGame()
        {   
            SaveLoadInterface gameState;

            using(StreamReader reader = new StreamReader(SavePath))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveLoadInterface));

                gameState = xmlSerializer.Deserialize(reader) as SaveLoadInterface;
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
