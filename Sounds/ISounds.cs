namespace BattleFiled.Sounds
{
    using System;
    public interface ISounds
    {
        string PathToInvalidSelectionSound { get; set; }

        string PathToDetonatedBombSound { get; set; }

        void PlayInvalidSelection();

        void PlayDetonatedBomb();

        void PlayPositionChanged();
    }
}
