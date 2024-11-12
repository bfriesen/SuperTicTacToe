namespace SuperTicTacToe.Domain
{
    internal static class GameExtensions
    {
        private const SpaceState _xo = SpaceState.X | SpaceState.O;

        private static readonly IReadOnlyList<IReadOnlyList<SpaceState>> _xMasks =
        [
            [SpaceState.X, SpaceState.X, SpaceState.X, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, SpaceState.X, SpaceState.X, SpaceState.X, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, SpaceState.X, SpaceState.X, SpaceState.X],
            [SpaceState.X, 0, 0, SpaceState.X, 0, 0, SpaceState.X, 0, 0],
            [0, SpaceState.X, 0, 0, SpaceState.X, 0, 0, SpaceState.X, 0],
            [0, 0, SpaceState.X, 0, 0, SpaceState.X, 0, 0, SpaceState.X],
            [SpaceState.X, 0, 0, 0, SpaceState.X, 0, 0, 0, SpaceState.X],
            [0, 0, SpaceState.X, 0, SpaceState.X, 0, SpaceState.X, 0, 0],
        ];

        private static readonly IReadOnlyList<IReadOnlyList<SpaceState>> _oMasks =
        [
            [SpaceState.O, SpaceState.O, SpaceState.O, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, SpaceState.O, SpaceState.O, SpaceState.O, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, SpaceState.O, SpaceState.O, SpaceState.O],
            [SpaceState.O, 0, 0, SpaceState.O, 0, 0, SpaceState.O, 0, 0],
            [0, SpaceState.O, 0, 0, SpaceState.O, 0, 0, SpaceState.O, 0],
            [0, 0, SpaceState.O, 0, 0, SpaceState.O, 0, 0, SpaceState.O],
            [SpaceState.O, 0, 0, 0, SpaceState.O, 0, 0, 0, SpaceState.O],
            [0, 0, SpaceState.O, 0, SpaceState.O, 0, SpaceState.O, 0, 0],
        ];

        private static readonly IReadOnlyList<SpaceState> _catMask = [_xo, _xo, _xo, _xo, _xo, _xo, _xo, _xo, _xo];

        public static GameState GetNextGameState(this ITicTacToeGame game)
        {
            int[]? winnerSpaces = null;
            return game.GetNextGameState(ref winnerSpaces);
        }

        public static GameState GetNextGameState(this ITicTacToeGame game, ref int[]? winnerSpaces)
        {
            if (game.GameState != GameState.InProgress)
                return game.GameState;

            foreach (var xMask in _xMasks)
            {
                if (IsMatch(xMask, game))
                {
                    winnerSpaces = xMask.Select((m, i) => (m, i)).Where(x => x.m != 0).Select(x => x.i).ToArray();
                    return GameState.XWon;
                }
            }

            foreach (var oMask in _oMasks)
            {
                if (IsMatch(oMask, game))
                {
                    winnerSpaces = oMask.Select((m, i) => (m, i)).Where(x => x.m != 0).Select(x => x.i).ToArray();
                    return GameState.OWon;
                }
            }

            if (IsMatch(_catMask, game))
                return GameState.Cat;

            return GameState.InProgress;

            static bool IsMatch(IReadOnlyList<SpaceState> mask, ITicTacToeGame game)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (mask[i] == SpaceState.Open)
                        continue;

                    if ((game.Spaces[i].SpaceState & mask[i]) == 0)
                        return false;
                }

                return true;
            }
        }
    }
}
