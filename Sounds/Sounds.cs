namespace BattleFiled.Sounds
{
    using System;
    using System.IO;
    using System.Media;
    public class Sounds : ISounds
    {
        private string pathToInvalidMoveSound;
        private string pathToDetonatedBombSound;
        private string pathToPositionChangedSound;

        public Sounds(string pathToInvalidMoveSound, string pathToDetonatedBombSound, string pathToPositionChangedSound)
        {
            this.PathToInvalidMoveSound = pathToInvalidMoveSound;
            this.PathToDetonatedBombSound = pathToDetonatedBombSound;
            this.PathToPositionChangedSound = pathToPositionChangedSound;
        }
        public string PathToInvalidMoveSound
        {
            get
            {
                return this.pathToInvalidMoveSound;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Cannot set null path.");
                }

                this.pathToInvalidMoveSound = value;
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
                if (value == null)
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
                if (value == null)
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

        public void PlayInvalidMove()
        {
            PlaySound(this.PathToInvalidMoveSound);
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
