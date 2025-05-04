namespace LiteralMaze
{
    internal struct FindWordResult
    {
        public string[] words { get; private set; }
        public char?[] letterAssoc { get; private set; }

        public FindWordResult(string[] words, char?[] letterAssoc)
        {
            this.words = words;
            this.letterAssoc = letterAssoc;
        }
    }
}