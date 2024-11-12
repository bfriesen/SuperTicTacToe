namespace SuperTicTacToe.Domain
{
    public class TicTacToeGame(int spaceIndex, TicTacToeSpace[] spaces) : ITicTacToeGame, ITicTacToeSpace
    {
        private int[]? _winningSpaces;

        public TicTacToeGame(int spaceIndex)
            : this(spaceIndex, [
                new TicTacToeSpace(0), new TicTacToeSpace(1), new TicTacToeSpace(2),
                new TicTacToeSpace(3), new TicTacToeSpace(4), new TicTacToeSpace(5),
                new TicTacToeSpace(6), new TicTacToeSpace(7), new TicTacToeSpace(8)])
        {
        }

        public IReadOnlyList<TicTacToeSpace> Spaces { get; }  = spaces;

        IReadOnlyList<ITicTacToeSpace> ITicTacToeGame.Spaces => Spaces;

        public int Index { get; } = spaceIndex;

        public GameState GameState { get; private set; }

        public IReadOnlyList<int>? WinningSpaces => _winningSpaces;

        public SpaceState SpaceState =>
            GameState switch
            {
                GameState.XWon => SpaceState.X,
                GameState.OWon => SpaceState.O,
                _ => SpaceState.Open,
            };

        public void Move(Player player, int space, SuperTicTacToeGame superTicTacToeGame)
        {
            if (space < 0 || space >= Spaces.Count)
                throw new ArgumentOutOfRangeException(nameof(space));

            Spaces[space].Move(player, superTicTacToeGame);
            GameState = this.GetNextGameState(ref _winningSpaces);
        }

        public void Undo(int space)
        {
            Spaces[space].Undo();
            GameState = this.GetNextGameState(ref _winningSpaces);
        }
    }
}
