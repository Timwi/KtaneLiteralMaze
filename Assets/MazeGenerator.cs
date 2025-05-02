using System.Collections.Generic;
using System.Linq;
using Rnd = UnityEngine.Random;

public class MazeGenerator
{
    private int _size;
    public MazeGenerator(int size)
    {
        _size = size;
    }
    private bool[][] _visited;
    private char[] _charArr;

    private static readonly int[] cellPositions = { 10, 12, 14, 16, 28, 30, 32, 34, 46, 48, 50, 52, 64, 66, 68, 70 };
    private static readonly int[] wallVectors = { -9, 1, 9, -1 };

    public static string GenerateEncodedMaze()
    {
        var mazeGenerator = new MazeGenerator(4);
        tryagain:
        var mazeString = mazeGenerator.GenerateMaze();
        int deadEnds = 0, attempts = 0;
        var cellWalls = new List<string>();
        var distinctWalls = new List<string>();
        var letters = "";
        for (int p = 0; p < 16; p++)
        {
            string theseWalls = "";
            for (int v = 0; v < 4; v++)
            {
                if (mazeString[cellPositions[p] + wallVectors[v]].ToString() == "█")
                {
                    theseWalls += "NESW"[v];
                }
            }
            if (theseWalls.Length == 3) { deadEnds++; }
            cellWalls.Add(theseWalls);
            if (!distinctWalls.Any(a => a == theseWalls))
            {
                distinctWalls.Add(theseWalls);
                letters += (char) ('a' + distinctWalls.Count - 1);
            }
            else
            {
                letters += (char) ('a' + distinctWalls.IndexOf(theseWalls));
            }
        }
        if (deadEnds == 2)
        {
            attempts++;
            cellWalls.Clear();
            distinctWalls.Clear();
            goto tryagain;
        }
        return letters;
    }

    public string GenerateMaze()
    {
        _visited = new bool[_size][];
        for (int i = 0; i < _visited.Length; i++)
            _visited[i] = new bool[_size];
        _charArr = new char[(_size * 2 + 1) * (_size * 2 + 1)].Select(i => '█').ToArray();
        for (int a = 0; a < _size; a++)
            for (int b = 0; b < _size; b++)
                _charArr[(a * (_size * 2 + 1) * 2) + (b * 2) + _size * 2 + 2] = ' ';
        var x = Rnd.Range(0, _size);
        var y = Rnd.Range(0, _size);
        Generate(x, y);
        return new string(_charArr);
    }

    private void Generate(int x, int y)
    {
        _visited[x][y] = true;
        var arr = Enumerable.Range(0, 4).ToArray().Shuffle();
        var curPos = (x * (_size * 2 + 1) * 2) + (y * 2) + (_size * 2 + 2);
        for (int i = 0; i < 4; i++)
        {
            if (arr[i] == 0)
                if (y != 0 && !_visited[x][y - 1])
                {
                    _charArr[curPos - 1] = ' ';
                    Generate(x, y - 1);
                }
            if (arr[i] == 1)
                if (x != _size - 1 && !_visited[x + 1][y])
                {
                    _charArr[curPos + (_size * 2 + 1)] = ' ';
                    Generate(x + 1, y);
                }
            if (arr[i] == 2)
                if (y != _size - 1 && !_visited[x][y + 1])
                {
                    _charArr[curPos + 1] = ' ';
                    Generate(x, y + 1);
                }
            if (arr[i] == 3)
                if (x != 0 && !_visited[x - 1][y])
                {
                    _charArr[curPos - (_size * 2 + 1)] = ' ';
                    Generate(x - 1, y);
                }
        }
    }
}
