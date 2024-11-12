namespace SuperTicTacToe.Domain
{
    public interface ITicTacToeGame
    {
        IReadOnlyList<ITicTacToeSpace> Spaces { get; }

        GameState GameState { get; }

        IReadOnlyList<int>? WinningSpaces { get; }
    }
}
