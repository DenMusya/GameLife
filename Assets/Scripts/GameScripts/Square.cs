using UnityEngine;

public class Square
{
    private readonly SpriteRenderer _renderer;
    private readonly Transform _transform;
    private readonly SquareAnimator _animator;
    
    private bool _isActivePrev;
    private bool _isActiveCurrent;
    private bool _isUpdated;
    
    
    private static Color _activateColor;
    
    private static Color _onTargetDeactivateColor;
    private static Color _onTargetActivateColor;
    
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
    }

    public void ImmediateActivation()
    {
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

    public void Activate()
    {
        _isActiveCurrent = true;
    }
    
    public void Deactivate()
    {
        _isActiveCurrent = false;
    }

    public void Target()
    {
        _transform.localScale = new Vector3(1, 1, 1);
        _renderer.color = _isActiveCurrent ? _onTargetActivateColor : _onTargetDeactivateColor;
    }

    public void Untarget()
    {
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
        
        _isActivePrev = _isActiveCurrent;
        
        if (death)
        {
            DeadAnimation();
        }

        if (birth)
        {
            BirthAnimation();
        }
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
    
    
}
