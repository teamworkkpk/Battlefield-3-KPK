namespace BattleFiled.Sounds
{
    using System;
    using System.IO;
    using System.Media;
    /// <summary>
    /// Contains methods for playing sounds.
    /// </summary>
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

        /// <summary>
        /// Sets path to a wave file to be played when invalid selection occurs
        /// </summary>
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

        /// <summary>
        /// Sets path to a wave file to be played when a bomb is detonated
        /// </summary>
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

        /// <summary>
        /// Sets path to a wave file to be played when the position on the field is changed
        /// </summary>
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

        /// <summary>
        /// Gets new instance of <see cref="System.Media.SoundPlayer"/> class to play the sound at <see cref="string pathToFile"/>.
        /// </summary>
        /// <param name="pathToFile">>Sets path at which <see cref="System.Media.SoundPlayer"/> instance searches for the file</param>
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

        /// <summary>
        /// Plays a sound when the selection is invalid, the method internally initializes <see cref="System.Media.SoundPlayer"/> class
        /// </summary>
        public void PlayInvalidSelection()
        {
            PlaySound(this.PathToInvalidSelectionSound);
        }

        /// <summary>
        /// Plays a sound when a bomb is detonated, the method internally initializes <see cref="System.Media.SoundPlayer"/> class
        /// </summary>
        public void PlayDetonatedBomb()
        {
            PlaySound(this.PathToDetonatedBombSound);
        }

        /// <summary>
        /// Plays a sound when the the position on the field has changed, the method internally initializes <see cref="System.Media.SoundPlayer"/> class
        /// </summary>
        public void PlayPositionChanged()
        {
            PlaySound(this.PathToPositionChangedSound);
        }
    }
}
