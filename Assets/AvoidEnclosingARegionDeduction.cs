using System.Collections.Generic;
using System.Linq;

namespace LiteralMaze
{
    class AvoidEnclosingARegionDeduction : Deduction
    {
        private static IEnumerable<CellTransition> FindReachableCells(int fromCell, bool?[][] known, string maze)
        {
            if (fromCell / 4 != 0 && known[maze[fromCell] - 'a'][0] != true)
                yield return new CellTransition(fromCell - 4, dir: 0);
            if (fromCell % 4 != 4 - 1 && known[maze[fromCell] - 'a'][1] != true)
                yield return new CellTransition(fromCell + 1, dir: 1);
            if (fromCell / 4 != 4 - 1 && known[maze[fromCell] - 'a'][2] != true)
                yield return new CellTransition(fromCell + 4, dir: 2);
            if (fromCell % 4 != 0 && known[maze[fromCell] - 'a'][3] != true)
                yield return new CellTransition(fromCell - 1, dir: 3);
        }

        public static int FindReachable(bool?[][] known, string maze, int startingPosition)
        {
            var visited = new HashSet<int> { startingPosition };

            while (visited.Count < 16)
            {
                var discovered = visited.SelectMany(cell => FindReachableCells(cell, known, maze).Select(tup => tup.Cell)).Except(visited).ToArray();
                if (discovered.Length == 0)
                    break;
                visited.UnionWith(discovered);
            }
            return visited.Count;
        }

        public override DeductionInfo? Deduce(bool?[][] known, string maze)
        {
            for (var ltr = 0; ltr < known.Length; ltr++)
                for (var dir = 0; dir < 4; dir++)
                    if (known[ltr][dir] == null)
                    {
                        known[ltr][dir] = true;
                        var count = FindReachable(known, maze, maze.IndexOf((char) (ltr + 'a')));
                        known[ltr][dir] = null;
                        if (count != 16)
                            return new DeductionInfo(ltr, dir, false);
                    }
            return null;
        }
    }
}