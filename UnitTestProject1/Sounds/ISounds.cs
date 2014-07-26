namespace BattleFiled.Sounds
{
    using System;
    /// <summary>
    /// Gives functionality for playing sounds
    /// </summary>
    public interface ISounds
    {
        /// <summary>
        /// Sets path to a wave file to be played when invalid selection occurs
        /// </summary>
        string PathToInvalidSelectionSound { get; set; }

        /// <summary>
        /// Sets path to a wave file to be played when a bomb is detonated
        /// </summary>
        string PathToDetonatedBombSound { get; set; }

        /// <summary>
        /// Sets path to a wave file to be played when the position on the field is changed
        /// </summary>
        string PathToPositionChangedSound { get; set; }

        /// <summary>
        /// Plays a sound when the selection is invalid, the method internally implements System.Media <see cref="System.Media.SoundPlayer"/> class
        /// </summary>
        void PlayInvalidSelection();

        /// <summary>
        /// Plays a sound when a bomb is detonated, the method internally implements System.Media <see cref="System.Media.SoundPlayer"/> class
        /// </summary>
        void PlayDetonatedBomb();

        /// <summary>
        /// Plays a sound when the the position on the field has changed, the method internally implements System.Media <see cref="System.Media.SoundPlayer"/> class
        /// </summary>
        void PlayPositionChanged();
    }
}
