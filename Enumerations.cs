namespace BattleFiled
{
    /// <summary>
    /// Enumeration uses ASCII codes instead of char symbols for the needs of XML Serialization.
    /// To use the correct char symbol, just cast to char
    /// Blown = 'X', Empty = '-', Bomb1 = '1', Bomb2 = '2', Bomb3 = '3', Bomb4 = '4', Bomb5 = '5'
    /// </summary>
    public enum CellView
    {        
        Blown = 88, Empty = 48, Bomb1 = 49, Bomb2 = 50, Bomb3 = 51, Bomb4 = 52, Bomb5 = 53
    }

    public enum Color
    {
        Red, White, Magenda
    }
}
