namespace LiteralMaze
{
    internal struct CellTransition
    {
        public int Cell { get; private set; }
        public int Direction { get; private set; }

        public CellTransition(int cell, int dir)
        {
            Cell = cell;
            Direction = dir;
        }
    }
}
