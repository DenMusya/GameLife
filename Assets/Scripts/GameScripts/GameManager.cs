using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private GameObject squaresManagerObject;
    [SerializeField] private EndGame endGame;
    
    private static Table _table;
    private static SquaresManager _squaresManager;
    private static Camera _camera;
    
    private const int Width = 400;
    private const int Height = 400;  
    private const float CellSize = 1f;
    
    private static float _timeToIteration;
    private static int _brushSize;
    
    private static bool _isRunning;
    private static bool _isFrozenMouseClick;
    
    private static GameMode _gameMode;
    private static bool _isEndGame;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        
        _isRunning = false;
        _isFrozenMouseClick = false;
        _isEndGame = false;
        
        Utils.Instantiate(Width, Height, CellSize);
        
        _table = new Table(Width, Height);
        _table.Render();
        
        _squaresManager = squaresManagerObject.GetComponent<SquaresManager>();
        
        _camera = cameraObject.GetComponent<Camera>();
        
        ScoreBoard.SetPlayer1Score(PlayerPrefs.GetInt("Player1"));
        ScoreBoard.SetPlayer2Score(PlayerPrefs.GetInt("Player2"));
    }
    private void Update()
    {
        Debug.Log("FPS: " + 1f / Time.deltaTime);
        if (!_isEndGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isRunning = !_isRunning;
                _squaresManager.UpdateAfterStop();
            }

            _squaresManager.UpdateOnTrigger();
            ScoreBoard.UpdateScore();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _squaresManager.UpdateAfterStop();
            _isEndGame = true;
            endGame.End(_squaresManager.GetActiveSquaresCount());
        }
    }

    public static void SetGameSpeed(float speed)
    {
        _timeToIteration = speed;
    }

    public static void SetBrushSize(int size)
    {
        _brushSize = size;
    }

    public static void FreezeMouse()
    {
        _isFrozenMouseClick = true;
    }

    public static void UnfreezeMouse()
    {
        _isFrozenMouseClick = false;
    }
    
    public static bool IsFrozenMouse()
    {
        return _isFrozenMouseClick;
    }
    public static bool IsRunning()
    {
        return _isRunning;
    }

    public static Camera GetCamera()
    {
        return _camera;
    }
    
    public static int GetWidth()
    {
        return Width;
    }

    public static int GetHeight()
    {
        return Height;
    }

    public static float GetCellSize()
    {
        return CellSize;
    }

    public static int GetBrushSize()
    {
        return _brushSize;
    }

    public static float GetTimeToIteration()
    {
        return _timeToIteration;
    }

    public static GameMode GetGameMode()
    {
        return _gameMode;
    }
}
