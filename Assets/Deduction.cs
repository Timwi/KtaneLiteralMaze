namespace LiteralMaze
{
    abstract class Deduction
    {
        public static readonly Deduction[] AllDeductions = new Deduction[] { new BorderAtSidesDeduction(), new AbuttingBordersDeduction(), new AvoidDuplicatesDeduction(), new AvoidEnclosingARegionDeduction() };
        public abstract DeductionInfo? Deduce(bool?[][] known, string maze);
    }
}
