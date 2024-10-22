using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SquareConfiguration
{
    private readonly List<Vector2Int> _squares;
    
    private static readonly float[] SwapAngles = { 0, Mathf.PI / 2f, Mathf.PI, 3f * Mathf.PI / 2f }; 
    private int _currentSwapIndex;
    public SquareConfiguration(string[] squares)
    {
        _squares = new List<Vector2Int>();
        var pivot = new Vector2Int(0, squares.Length / 2);


        foreach (var square in squares)
        {
            pivot.x = Mathf.Max(pivot.x, square.Length / 2);
        }

        for (var y = 0; y < squares.Length; y++)
        {
            for (var x = 0; x < squares[y].Length; x++)
            {
                if (squares[y][x] == '@' || squares[y][x] == 'X')
                {
                    _squares.Add(new Vector2Int(x - pivot.x, pivot.y - y));
                }
            }
        }
        
    }

    public SquareConfiguration(List<Vector2Int> squares, Vector2Int anchor, int currentReverseIndex = 0)
    {
        _squares = new List<Vector2Int>(squares);

        for (var i = 0; i < _squares.Count; i++)
        {
            var angle = SwapAngles[currentReverseIndex];
            
            var newX = Mathf.Round(Mathf.Cos(angle) * _squares[i].x - Mathf.Sin(angle) * squares[i].y);
            var newY = Mathf.Round(Mathf.Sin(angle) * _squares[i].x + Mathf.Cos(angle) * squares[i].y);
            
            _squares[i] = new Vector2Int(Mathf.FloorToInt(newX), Mathf.FloorToInt(newY));
            _squares[i] += anchor;
        }
    }

    public List<Vector2Int> GetSquares()
    {
        return _squares;
    }
}
