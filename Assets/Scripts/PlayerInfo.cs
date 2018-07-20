public static class PlayerInfo
{
    public enum GameModel
    {
        ManMachine,
        DoubleMan,
        Net,
    }

    public enum AILevel
    {
        Primary,
        Intermediate,
        Senior,
    }

    public static GameModel gameModel;
    public static AILevel aiLevel;
    public static bool isPlayerFirst;
}
