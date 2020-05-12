﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{

    public int sizeX, sizeZ;

    public MazeCell cellPrefab;

    private MazeCell[,] cells;

    public float generationStepDelay;

    public IntVector2 size;

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    public MazeCell GetCell (IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    public void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoordinates));   
    }

    public void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        MazeDirection direction = MazeDirections.RandomValue;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates) && GetCell(coordinates) == null)
        {
            activeCells.Add(CreateCell(coordinates));
        } else
        {
            activeCells.RemoveAt(currentIndex);
        }
    }

    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        IntVector2 coordinates = RandomCoordinates;
        // while (ContainsCoordinates(coordinates) && GetCell(coordinates) == null)
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
            // CreateCell(coordinates);
            // coordinates += MazeDirections.RandomValue.ToIntVector2();
            DoNextGenerationStep(activeCells);
        }
    }

    private MazeCell CreateCell(IntVector2 coordinates)
    {
        //MazeCell
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

}