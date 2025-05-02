namespace LiteralMaze
{
    class AbuttingBordersDeduction : Deduction
    {
        public override DeductionInfo? Deduce(bool?[][] known, string maze)
        {
            // Left/right neighbors
            for (var i = 0; i < maze.Length; i++)
                if (i % 4 != 0)
                {
                    var leftLtr = maze[i - 1] - 'a';
                    var rightLtr = maze[i] - 'a';
                    if (known[leftLtr][1] != null && known[rightLtr][3] == null)
                        return new DeductionInfo(rightLtr, 3, known[leftLtr][1].Value);
                    if (known[leftLtr][1] == null && known[rightLtr][3] != null)
                        return new DeductionInfo(leftLtr, 1, known[rightLtr][3].Value);
                }
            // Above/below neighbors
            for (var i = 4; i < maze.Length; i++)
            {
                var topLtr = maze[i - 4] - 'a';
                var bottomLtr = maze[i] - 'a';
                if (known[topLtr][2] != null && known[bottomLtr][0] == null)
                    return new DeductionInfo(bottomLtr, 0, known[topLtr][2].Value);
                if (known[topLtr][2] == null && known[bottomLtr][0] != null)
                    return new DeductionInfo(topLtr, 2, known[bottomLtr][0].Value);
            }
            return null;
        }
    }
}
