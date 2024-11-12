namespace SuperTicTacToe.Domain
{
    public class SuperTicTacToeGame(TicTacToeGame[] games) : ITicTacToeGame
    {
        private readonly IReadOnlyList<TicTacToeGame> _games = games;
        private int[]? _winningSpaces;

        public SuperTicTacToeGame()
            : this([
                new TicTacToeGame(0), new TicTacToeGame(1), new TicTacToeGame(2),
                new TicTacToeGame(3), new TicTacToeGame(4), new TicTacToeGame(5),
                new TicTacToeGame(6), new TicTacToeGame(7), new TicTacToeGame(8)])
        {
        }

        public IReadOnlyList<TicTacToeGame> Games => _games;

        public IReadOnlyList<ITicTacToeSpace> Spaces => _games;

        public GameState GameState { get; private set; }

        public Player CurrentPlayer { get; private set; } = Player.X;

        public int? CurrentSpace { get; private set; }

        public (int OuterSpace, int InnerSpace)? LastMove { get; private set; }

        public IReadOnlyList<int>? WinningSpaces => _winningSpaces;

        public void Move(int outerSpace, int innerSpace)
        {
            if (outerSpace < 0 || outerSpace >= _games.Count)
                throw new ArgumentOutOfRangeException(nameof(outerSpace));

            if (CurrentSpace.HasValue && outerSpace != CurrentSpace)
                throw new InvalidOperationException("Must play in current space.");

            _games[outerSpace].Move(CurrentPlayer, innerSpace, this);
            GameState = this.GetNextGameState(ref _winningSpaces);

            CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;
            CurrentSpace = innerSpace;
            LastMove = (outerSpace, innerSpace);
        }

        public void Undo()
        {
            if (LastMove is null)
                return;

            Games[LastMove.Value.OuterSpace].Undo(LastMove.Value.InnerSpace);
            GameState = this.GetNextGameState(ref _winningSpaces);

            CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;

            if (Games.All(game => game.Spaces.All(space => space.SpaceState == SpaceState.Open)))
                CurrentSpace = null;
            else
                CurrentSpace = LastMove.Value.OuterSpace;

            LastMove = null;
        }
    }
}
