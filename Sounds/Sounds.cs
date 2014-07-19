namespace BattleFiled.Sounds
{
    using System;
    using System.IO;
    using System.Media;
    public class Sounds : ISounds
    {
        private string pathToInvalidSelectionSound;
        private string pathToDetonatedBombSound;
        private string pathToPositionChangedSound;

        public Sounds(string pathToInvalidSelectionSound, string pathToDetonatedBombSound, string pathToPositionChangedSound)
        {
            this.PathToInvalidSelectionSound = pathToInvalidSelectionSound;
            this.PathToDetonatedBombSound = pathToDetonatedBombSound;
            this.PathToPositionChangedSound = pathToPositionChangedSound;
        }
        public string PathToInvalidSelectionSound
        {
            get
            {
                return this.pathToInvalidSelectionSound;
            }

            set
            {
                if (value == null || value == "" || value == string.Empty)
                {
                    throw new ArgumentNullException("Cannot set null path.");
                }

                this.pathToInvalidSelectionSound = value;
            }
        }

        public string PathToDetonatedBombSound
        {
            get
            {
                return this.pathToDetonatedBombSound;
            }

            set
            {
                if (value == null || value == "" || value == string.Empty)
                {
                    throw new ArgumentNullException("Cannot set null path.");
                }

                this.pathToDetonatedBombSound = value;
            }
        }

        public string PathToPositionChangedSound
        {
            get
            {
                return this.pathToPositionChangedSound;
            }

            set
            {
                if (value == null || value == "" || value == string.Empty)
                {
                    throw new ArgumentNullException("Cannot set null path.");
                }

                this.pathToPositionChangedSound = value;
            }
        }

        private void PlaySound(string pathToFile)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(pathToFile);
                player.Play();
            }

            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("File at path: " + pathToFile + " is not found.");
            }
        }

        public void PlayInvalidSelection()
        {
            PlaySound(this.PathToInvalidSelectionSound);
        }

        public void PlayDetonatedBomb()
        {
            PlaySound(this.PathToDetonatedBombSound);
        }

        public void PlayPositionChanged()
        {
            PlaySound(this.PathToPositionChangedSound);
        }
    }
}
