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

    public static GameModel gameModel = GameModel.ManMachine;
    public static AILevel aiLevel = AILevel.Senior;
    public static bool isPlayerFirst = true;
}
