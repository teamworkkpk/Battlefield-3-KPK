namespace BattleFiled.Sounds
{
    using System;
    public interface ISounds
    {
        string PathToInvalidMoveSound { get; set; }

        string PathToDetonatedBombSound { get; set; }

        void PlayInvalidMove();

        void PlayDetonatedBomb();
    }
}
