using System;
using System.Linq;

namespace LiteralMaze
{
    class AvoidDuplicatesDeduction : Deduction
    {
        public override DeductionInfo? Deduce(bool?[][] known, string maze)
        {
            for (var i = 0; i < known.Length; i++)
                if (known[i].All(b => b != null))
                    for (var j = 0; j < known.Length; j++)
                        if (j != i && known[j].Count(b => b == null) == 1 && Enumerable.Range(0, 4).All(dir => known[j][dir] == null || known[j][dir].Value == known[i][dir].Value))
                        {
                            var dir = Array.FindIndex(known[j], b => b == null);
                            return new DeductionInfo(j, dir, !known[i][dir].Value);
                        }
            return null;
        }
    }
}
