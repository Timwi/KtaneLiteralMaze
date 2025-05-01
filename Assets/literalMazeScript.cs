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
        //tryagain goes here
        mazeString = mazeGenerator.GenerateMaze();
        LogMaze();
    }

    void CellPress(KMSelectable Cell) {
        for (int Q = 0; Q < Grid.Length; Q++) {
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
