using System;
using System.Collections.Generic;
using System.Linq;
using LiteralMaze;
using UnityEngine;

public class literalMazeScript : MonoBehaviour
{
    public KMAudio Audio;
    public KMSelectable[] Grid;
    public TextMesh[] Letters;
    public SpriteRenderer[] SpriteSlots;
    public Sprite[] WallSprites;

    private MazeGenerator mazeGenerator;
    private readonly List<string> cellWalls = new List<string>();
    private readonly List<string> distinctWalls = new List<string>();

    private string mazeString; // 16 lower-case letters (a, b, etc.)
    private int[] solution; // tile number for each letter (starting with a)
    bool disambiguatorRequired;
    int currentWall = -1;
    int placedWalls = 0;

    // Logging
    private static int moduleIdCounter = 1;
    private int moduleId;

    void Awake()
    {
        moduleId = moduleIdCounter++;

        foreach (KMSelectable Cell in Grid)
        {
            Cell.OnInteract += delegate () { CellPress(Cell); return false; };
        }
    }

    private static IEnumerable<bool?[][]> Recurse(bool?[][] known, string maze)
    {
        keepGoing:
        var anyDeduction = false;
        for (var i = 0; i < Deduction.AllDeductions.Length; i++)
        {
            var tup = Deduction.AllDeductions[i].Deduce(known, maze);
            if (tup != null)
            {
                anyDeduction = true;
                //ConsoleUtil.WriteLine($"{allDeductions[i].GetType().Name.Color(allDeductions[i].Color)}: {(char) (tup.Value.letter + 'a')} {"NESW"[tup.Value.dir]} = {tup.Value.wall}", null);
                known[tup.Value.Letter][tup.Value.Direction] = tup.Value.IsWall;
            }
        }
        if (anyDeduction)
            goto keepGoing;

        var firstUnsolvedLtr = Array.FindIndex(known, walls => walls.Any(w => w == null));
        if (firstUnsolvedLtr == -1)
        {
            yield return known;
            yield break;
        }
        var firstUnsolvedDir = Enumerable.Range(0, 4).First(dir => known[firstUnsolvedLtr][dir] == null);
        foreach (var poss in new[] { true, false })
        {
            known[firstUnsolvedLtr][firstUnsolvedDir] = poss;
            foreach (var solution in Recurse(known.Select(k => k.ToArray()).ToArray(), maze))
                yield return solution;
        }
    }

    void Start()
    {
        tryAgain:
        mazeString = MazeGenerator.GenerateEncodedMaze();
        var solutions = Recurse(Enumerable.Range(0, mazeString.Distinct().Count()).Select(_ => new bool?[4]).ToArray(), mazeString).ToArray();
        disambiguatorRequired = solutions.Length != 1;
        if (!disambiguatorRequired)
        {
            solution = solutions[0].Select(bs => (bs[0].Value ? 1 : 0) | (bs[1].Value ? 2 : 0) | (bs[2].Value ? 4 : 0) | (bs[3].Value ? 8 : 0)).ToArray();

            currentWall = solution.PickRandom();
        }
        else
        {
            var tileArrays = Enumerable.Range(0, 15).Select(tile => new bool?[] { (tile & 1) != 0, (tile & 2) != 0, (tile & 4) != 0, (tile & 8) != 0 }).ToArray();
            var tileCounts = tileArrays.Select(tile => solutions.Count(sol => sol.Any(t => t.SequenceEqual(tile)))).ToArray();
            var disambiguatingTiles = Enumerable.Range(0, 15).Where(tile => tileCounts[tile] == 1).ToArray();
            if (disambiguatingTiles.Length == 0)
                goto tryAgain;
            var disambiguator = disambiguatingTiles.PickRandom();

            var applicableSolution = solutions.Single(s => s.Any(t => t.SequenceEqual(tileArrays[disambiguator])));
            solution = applicableSolution.Select(bs => (bs[0].Value ? 1 : 0) | (bs[1].Value ? 2 : 0) | (bs[2].Value ? 4 : 0) | (bs[3].Value ? 8 : 0)).ToArray();

            currentWall = disambiguator;
        }

        SpriteSlots[16].sprite = WallSprites[currentWall];

        // TODO: check if 2 deadends, what we have here doesn't work since it doesn't take into account the number of occurances of each letter in the maze.
        var deadends = new[] { 7, 11, 13, 14 };
        if (solution.Count(deadends.Contains) == 2)
            goto tryAgain;

        //TODO: implement algorithm which attempts to put as many words as possible into the rows of the grid, update the below loop to update the TextMeshes accordingly

        for (int v = 0; v < 16; v++) {
            Letters[v].text = mazeString[v].ToString();
        }

        Debug.LogFormat("<Literal Maze #{0}> Simplified maze: {1} / Disambiguator {2}", moduleId, mazeString, disambiguatorRequired);
        Debug.LogFormat("<Literal Maze #{0}> Tiles: {1}", moduleId, solution.Join(","));
    }

    void CellPress(KMSelectable Cell)
    {
        for (int Q = 0; Q < 16; Q++)
        {
            if (Grid[Q] == Cell)
            {
                Debug.Log(Q);
            }
        }
    }
}
