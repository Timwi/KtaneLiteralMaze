namespace LiteralMaze
{
    internal struct DeductionInfo
    {
        public int Letter { get; private set; }
        public int Direction { get; private set; }
        public bool IsWall { get; private set; }

        public DeductionInfo(int letter, int dir, bool wall)
        {
            Letter = letter;
            Direction = dir;
            IsWall = wall;
        }
    }
}
