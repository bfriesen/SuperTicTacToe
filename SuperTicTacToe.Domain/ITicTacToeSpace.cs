namespace SuperTicTacToe.Domain
{
    public interface ITicTacToeSpace
    {
        int Index { get; }

        SpaceState SpaceState { get; }
    }
}
