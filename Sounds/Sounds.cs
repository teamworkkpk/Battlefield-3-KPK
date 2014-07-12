namespace BattleFiled.Sounds
{
    using System;
    using System.Media;
    class Sounds : ISounds
    {
        private string pathToInvalidMoveSound;
        private string pathToDetonatedBombSound;

        public Sounds(string pathToInvalidMoveSound, string pathToDetonatedBombSound)
        {
            this.PathToInvalidMoveSound = pathToInvalidMoveSound;
            this.PathToDetonatedBombSound = pathToDetonatedBombSound;
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

        private void PlaySound(string pathToFile)
        {
            SoundPlayer player = new SoundPlayer(pathToFile);
            player.Play();
        }

        public void PlayInvalidMove()
        {
            PlaySound(this.PathToInvalidMoveSound);
        }

        public void PlayDetonatedBomb()
        {
            PlaySound(this.PathToDetonatedBombSound);
        }
    }
}
