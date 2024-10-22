using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Player1,
    Player2
}

public enum GameMode
{
    Solo,
    Duo
}
public static class Utils
{
    
    public static readonly int[] Dx = { -1, -1, -1, 1, 1, 1, 0, 0 };
    public static readonly int[] Dy = { -1, 1, 0, -1, 1, 0, 1, -1 };
    
    private static int _width;
    private static int _height;
    private static float _cellSize;

    public static void Instantiate(int width, int height, float cellSize)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
    } 
    
    public static Vector3 GetWorldCellPosition(int x, int y)
    {
        return new Vector3(x, y, 0) * _cellSize;
    }

    public static Vector2Int GetCellCoordinates(Vector3 position)
    {
        return new Vector2Int(Mathf.FloorToInt(position.x / _cellSize), Mathf.FloorToInt(position.y / _cellSize));
    }
    
    public static bool IsAllowableSquare(Vector3 position)
    {
        var x = GetCellCoordinates(position).x;
        var y = GetCellCoordinates(position).y;

        return IsAllowableSquare(x, y);
    }
    
    public static bool IsAllowableSquare(int x, int y)
    {
        return !(x < 0 || x >= _width || y < 0 || y >= _height);
    }
    
    public static SquareConfiguration GetSquaresByBrush(Vector3 center)
    {
        var squares = new List<Vector2Int>();
        
        var x = GetCellCoordinates(center).x;
        var y = GetCellCoordinates(center).y;

        for (var dx = -GameManager.GetBrushSize(); dx <= GameManager.GetBrushSize(); dx++)
        {
            for (var dy = -GameManager.GetBrushSize(); dy <= GameManager.GetBrushSize(); dy++)
            {
                if (Mathf.Abs(dy) + Mathf.Abs(dx) <= GameManager.GetBrushSize())
                {
                    squares.Add(new Vector2Int(x + dx, y + dy));
                }
            }
        }
        
        return new SquareConfiguration(squares, Vector2Int.zero);
    }
}
