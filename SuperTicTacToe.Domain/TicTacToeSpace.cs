namespace SuperTicTacToe.Domain
{
    public class TicTacToeSpace(int index) : ITicTacToeSpace
    {
        public int Index { get; } = index;

        public SpaceState SpaceState { get; private set; }

        public bool IsUnavailable(SuperTicTacToeGame superTicTacToeGame) =>
            superTicTacToeGame.Games[Index].Spaces.All(space => space.SpaceState != SpaceState.Open);

        public void Move(Player player, SuperTicTacToeGame superTicTacToeGame)
        {
            if (!Enum.IsDefined(player))
                throw new ArgumentOutOfRangeException(nameof(player));

            ArgumentNullException.ThrowIfNull(superTicTacToeGame);

            if (SpaceState != SpaceState.Open || IsUnavailable(superTicTacToeGame))
                throw new InvalidOperationException("Space not open.");

            SpaceState = (SpaceState)player;
        }

        public void Undo()
        {
            SpaceState = SpaceState.Open;
        }
    }
}
