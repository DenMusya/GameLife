using UnityEngine;

public class Square
{
    private readonly SpriteRenderer _renderer;
    private readonly Transform _transform;
    private readonly SquareAnimator _animator;
    
    private bool _isActivePrev;
    private bool _isActiveCurrent;
    private bool _isUpdated;
    
    private static Sprite _player1Sprite;
    private static Sprite _player2Sprite;
    
    private static Color _activateColor;
    private static Color _onTargetDeactivateColor;
    private static Color _onTargetActivateColor;
    
    private PlayerType _currentPlayerType;
    private PlayerType _prevPlayerType;
    
    public Square(GameObject gameObject)
    {
        _isActivePrev = false;
        _isActiveCurrent = false;
        
        _activateColor = Color.white;
        _onTargetActivateColor = Color.white;
        _onTargetDeactivateColor = Color.white;

        _activateColor.a = 1f;
        _onTargetActivateColor.a = .6f;
        _onTargetDeactivateColor.a = .3f;
        
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _renderer.enabled = true;
        
        _transform = gameObject.GetComponent<Transform>();
        _transform.localScale = new Vector3(0, 0, 1);

        _animator = gameObject.GetComponent<SquareAnimator>();
        _animator.enabled = false;
        
        _player1Sprite = Resources.Load<Sprite>("Sprites/SteveHead");
        _player2Sprite = Resources.Load<Sprite>("Sprites/ZombieHead");
    }

    public void ImmediateActivation(PlayerType playerType)
    {
        _currentPlayerType = playerType;
        _prevPlayerType = _currentPlayerType;
        
        UpdateSprite();
        
        _isActiveCurrent = true;
        _isActivePrev = true;
        
        _transform.localScale = new Vector3(1, 1, 1);
    }

    public void ImmediateDeactivation()
    {
        _isActiveCurrent = false;
        _isActivePrev = false;
        
        _transform.localScale = new Vector3(0, 0, 1);
    }

    public void Activate(PlayerType playerType)
    {
        _isActiveCurrent = true;
        _currentPlayerType = playerType;
    }
    
    public void Deactivate()
    {
        _isActiveCurrent = false;
    }

    public void Target()
    {
        _currentPlayerType = SquaresManager.GetPlayerType();
        UpdateSprite();
        
        _transform.localScale = new Vector3(1, 1, 1);
        _renderer.color = _isActiveCurrent ? _onTargetActivateColor : _onTargetDeactivateColor;
    }

    public void Untarget()
    {
        _currentPlayerType = _prevPlayerType;
        UpdateSprite();
        
        _transform.localScale = _isActiveCurrent ? new Vector3(1, 1, 1) : new Vector3(0, 0, 1);;
        _renderer.color = _activateColor;
    }
    public bool IsActive()
    {
        return _isActivePrev;
    }

    public void Update()
    {
        var death = _isActivePrev && !_isActiveCurrent;
        var birth = !_isActivePrev && _isActiveCurrent;
        
        _prevPlayerType = _currentPlayerType;
        UpdateSprite();
        
        _isActivePrev = _isActiveCurrent;
        
        if (death)
        {
            if (GameManager.GetTimeToIteration() < 0.3f)
            {
                ImmediateDeactivation();
            } else 
            {
                DeadAnimation();
            }
        }

        if (birth)
        {
            if (GameManager.GetTimeToIteration() < 0.3f)
            {
                ImmediateActivation(_currentPlayerType);
            } else 
            {
                BirthAnimation();
            }
        }
    }

    private void UpdateSprite()
    {
        _renderer.sprite = IsPlayer1() ? _player1Sprite : _player2Sprite;
    }
    private void BirthAnimation()
    {
        _animator.enabled = true;
        _animator.PlayBirthAnimation();
    }
    
    private void DeadAnimation()
    {
        _animator.enabled = true;
        _animator.PlayDeadAnimation();
    }

    private bool IsPlayer1()
    {
        return _currentPlayerType == PlayerType.Player1;
    }

    public PlayerType GetPlayerType()
    {
        return _prevPlayerType;
    }
}
