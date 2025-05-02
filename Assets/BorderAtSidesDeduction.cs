namespace LiteralMaze
{
    class BorderAtSidesDeduction : Deduction
    {
        public override DeductionInfo? Deduce(bool?[][] known, string maze)
        {
            // Top and bottom edges
            for (var x = 0; x < 4; x++)
            {
                if (known[maze[x] - 'a'][0] == null)
                    return new DeductionInfo(maze[x] - 'a', 0, true);
                if (known[maze[x + 4 * (4 - 1)] - 'a'][2] == null)
                    return new DeductionInfo(maze[x + 4 * (4 - 1)] - 'a', 2, true);
            }
            // Left and right edges
            for (var y = 0; y < 4; y++)
            {
                if (known[maze[y * 4] - 'a'][3] == null)
                    return new DeductionInfo(maze[y * 4] - 'a', 3, true);
                if (known[maze[y * 4 + 4 - 1] - 'a'][1] == null)
                    return new DeductionInfo(maze[y * 4 + 4 - 1] - 'a', 1, true);
            }
            return null;
        }
    }
}
