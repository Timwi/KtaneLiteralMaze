using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class literalMazeScript : MonoBehaviour {

    public KMAudio Audio;

    public KMSelectable[] Grid;
    public TextMesh[] Letters;
    public SpriteRenderer[] SpriteSlots;
    public Sprite[] WallSprites;

    private MazeGenerator mazeGenerator;
    string mazeString;
    int[] cellPositions = { 10, 12, 14, 16, 28, 30, 32, 34, 46, 48, 50, 52, 64, 66, 68, 70 };
    int[] wallVectors = { -9, 1, 9, -1 };
    List<string> cellWalls = new List<string> { };
    List<string> distinctWalls = new List<string> { };
    int attempts = 1;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;

    void Awake () {
        moduleId = moduleIdCounter++;

        foreach (KMSelectable Cell in Grid) {
            Cell.OnInteract += delegate () { CellPress(Cell); return false; };
        }
    }

    void Start () {
        mazeGenerator = new MazeGenerator(4);
        tryagain:
        mazeString = mazeGenerator.GenerateMaze();
        int deadEnds = 0;
        for (int p = 0; p < 16; p++) {
            string theseWalls = "";
            for (int v = 0; v < 4; v++) {
                if (mazeString[cellPositions[p] + wallVectors[v]].ToString() == "█") {
                    theseWalls += "NESW"[v];
                }
            }
            if (theseWalls.Length == 3) { deadEnds++; }
            cellWalls.Add(theseWalls);
            if (!distinctWalls.Any(a => a == theseWalls)) {
                distinctWalls.Add(theseWalls);
                Letters[p].text = "abcdefghijklmnop"[distinctWalls.Count() - 1].ToString();
            } else {
                Letters[p].text = "abcdefghijklmnop"[distinctWalls.IndexOf(theseWalls)].ToString();
            }
        }
        if (deadEnds == 2) {
            attempts++;
            Debug.Log("snake=" + cellWalls.Join(","));
            cellWalls.Clear();
            distinctWalls.Clear();
            goto tryagain;
        }
        Debug.Log("attempts: " + attempts);
        LogMaze();
        Debug.Log(cellWalls.Join(","));
    }

    void CellPress(KMSelectable Cell) {
        for (int Q = 0; Q < 16; Q++) {
            if (Grid[Q] == Cell) {
                Debug.Log(Q);
            }
        }
    }

    private void LogMaze()
    {
        var maze = new string[9];
        for (int i = 0; i < maze.Length; i++)
            maze[i] = mazeString.Substring(i * 9, 9);
        Debug.Log(maze.Join(","));
        /*
        for (int i = 0; i < maze.Length; i++)
            Debug.LogFormat("[Literal Maze #{0}] {1}", moduleId, maze[i]);
        */
    }
}
