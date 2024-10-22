using UnityEngine;
using System.Collections.Generic;

public class SquaresManager : MonoBehaviour
{
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private SquareConfigurationsManager squareConfigurationsManager;
    private Square[,] _squaresArray;
    
    private static float _timer;

    private List<Square> _targetedSquaresList;

    private bool _isTemplateChosen;
    
    private static PlayerType _currentPlayerType;
    
    private void Start()
    {
        _isTemplateChosen = false;
        _squaresArray = new Square[GameManager.GetWidth(), GameManager.GetHeight()];
        _targetedSquaresList = new List<Square>();
        
        for (var i = 0; i < _squaresArray.GetLength(0); i++)
        {
            for (var j = 0; j < _squaresArray.GetLength(1); j++)
            {
                var position = Utils.GetWorldCellPosition(i, j) + new Vector3(GameManager.GetCellSize(), GameManager.GetCellSize()) / 2f;
                _squaresArray[i, j] = new Square(SpawnSquare(position, GameManager.GetCellSize()));
            }
        }
        
    }
    
    private void UpdateSquares()
    {
        for (var x = 0; x < _squaresArray.GetLength(0); ++x)
        {
            for (var y = 0; y < _squaresArray.GetLength(1); ++y)
            {
                var neighboursPlayer1 = CountOfNeighbours(x, y, PlayerType.Player1);
                var neighboursPlayer2 = CountOfNeighbours(x, y, PlayerType.Player2);
                var deltaNeighbours = neighboursPlayer1 - neighboursPlayer2;
                
                if (IsActiveSquare(x, y) && deltaNeighbours != 2 && deltaNeighbours != 3 && deltaNeighbours != -2 && deltaNeighbours != -3)
                {
                    _squaresArray[x, y].Deactivate();
                }

                if (!IsActiveSquare(x, y) && (deltaNeighbours == 3 || deltaNeighbours == -3))
                {
                    _squaresArray[x, y].Activate(deltaNeighbours == 3 ? PlayerType.Player1 : PlayerType.Player2);
                }
            }
        }

        foreach (var square in _squaresArray)
        {
            square.Update();
        }
    }
    
    private void UpdateSquareOnClick(SquareConfiguration squaresToUpdatePositions, bool activate)
    {
        foreach (var squarePositions in squaresToUpdatePositions.GetSquares())
        {
            var x = squarePositions.x;
            var y = squarePositions.y;

            if (!Utils.IsAllowableSquare(x, y)) continue;

            if (activate && !_squaresArray[x, y].IsActive() && ScoreBoard.GetRemainingPlayerSquares(_currentPlayerType) >= 1)
            {
                _squaresArray[x, y].ImmediateActivation(_currentPlayerType);
                ScoreBoard.DecreaseRemainingPlayerSquares(_currentPlayerType);
            }
            else if (!activate && _squaresArray[x, y].IsActive() && _currentPlayerType == _squaresArray[x, y].GetPlayerType())
            {
                _squaresArray[x, y].ImmediateDeactivation();
                ScoreBoard.IncreaseRemainingPlayerSquares(_currentPlayerType);
            }
        }
    }
    
    public void ClearGrid()
    {
        foreach (var square in _squaresArray)
        {
            if (!square.IsActive() || square.GetPlayerType() != _currentPlayerType) {continue;}
            
            ScoreBoard.IncreaseRemainingPlayerSquares(square.GetPlayerType());
            square.ImmediateDeactivation();
            
        }
    }

    private void RandomFillGrid()
    {
        var random = new System.Random();
        foreach (var square in _squaresArray)
        {
            if (random.Next(2) == 0 && !square.IsActive() && ScoreBoard.GetRemainingPlayerSquares(_currentPlayerType) >= 1)
            {
                square.ImmediateActivation(_currentPlayerType);
                ScoreBoard.DecreaseRemainingPlayerSquares(_currentPlayerType);
            }
        }
    }

    private void UpdateTargetSquare(SquareConfiguration squaresToTargetPositions)
    {
        foreach (var square in _targetedSquaresList)
        {
            square.Untarget();
        }
        _targetedSquaresList.Clear();

        foreach (var squarePosition in squaresToTargetPositions.GetSquares())
        {
            var x = squarePosition.x;
            var y = squarePosition.y;

            if (!Utils.IsAllowableSquare(x, y)) continue;

            _squaresArray[x, y].Target();
            _targetedSquaresList.Add(_squaresArray[x, y]);
        }
    }
    private int CountOfNeighbours(int x, int y, PlayerType playerType)
    {
        var neighbours = 0;

        for (var i = 0; i < Utils.Dx.Length; i++)
        {
            neighbours += IsActiveSquare(x + Utils.Dx[i], y + Utils.Dy[i]) && _squaresArray[x + Utils.Dx[i], y + Utils.Dy[i]].GetPlayerType() == playerType ? 1 : 0;
        }
        
        return neighbours;
    } 
    private bool IsActiveSquare(int x, int y)
    {
        return Utils.IsAllowableSquare(x, y) && _squaresArray[x, y].IsActive();
    }

    public void UpdateOnTrigger()
    {
        if (!GameManager.IsRunning())
        {
            var curMousePosition = GameManager.GetCamera().ScreenToWorldPoint(Input.mousePosition);
            var currentConfiguration = !_isTemplateChosen ? Utils.GetSquaresByBrush(curMousePosition) : squareConfigurationsManager.GetTemplate(curMousePosition);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SwapPlayerType();
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                RandomFillGrid();
            }
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                ClearGrid();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SquareConfigurationsManager.ReverseConfiguration();
            }
            
            for (var i = 0; i <= 9; ++i)
            {
                if (!Input.GetKeyDown(i.ToString()))
                {
                    continue;
                }
                
                GameManager.SetBrushSize(i);
                _isTemplateChosen = false;
            }
            
            if (Input.GetMouseButton(0) && !GameManager.IsFrozenMouse())
            {
                UpdateSquareOnClick(currentConfiguration, true);
            }
            
            if (Input.GetMouseButton(1) && !GameManager.IsFrozenMouse())
            {
                UpdateSquareOnClick(currentConfiguration, false);
            }
            
            UpdateTargetSquare(currentConfiguration);
            return;
        }
        
        if (_timer >= GameManager.GetTimeToIteration())
        {
            _timer = 0f;
            UpdateSquares();
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    public void UpdateAfterStop()
    {
        foreach (var square in _targetedSquaresList)
        {
            square.Untarget();
        }
        _targetedSquaresList.Clear();
    }
    private GameObject SpawnSquare(Vector3 position, float squareSize)
    {
        var square = Instantiate(squarePrefab, position, Quaternion.identity);
        square.transform.localScale = new Vector3(squareSize, squareSize, 1f);
        return square;
    }

    public void TemplateChosen()
    {
        _isTemplateChosen = true;
    }

    public static void SwapPlayerType()
    {
        _currentPlayerType = _currentPlayerType == PlayerType.Player1 ? PlayerType.Player2 : PlayerType.Player1;
    }

    public static PlayerType GetPlayerType()
    {
        return _currentPlayerType;
    }

    public Vector2Int GetActiveSquaresCount()
    {
        var count = new Vector2Int(0, 0);
        foreach (var square in _squaresArray)
        {
            if (square.GetPlayerType() == PlayerType.Player1)
            {
                count.x += square.IsActive() ? 1 : 0;
            }
            if (square.GetPlayerType() == PlayerType.Player2)
            {
                count.y += square.IsActive() ? 1 : 0;
            }
        }
        return count;
    }
}
