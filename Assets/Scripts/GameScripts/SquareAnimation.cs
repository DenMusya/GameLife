using UnityEngine;

public class SquareAnimation
{
    private readonly bool _isDeadAnimation;
    private readonly Transform _squareTransform;
    private float _animationTime; 
    
    private float _currentElapsedTime;
    
    public SquareAnimation(bool isDeadAnimation, Transform squareTransform)
    {
        _animationTime = GameManager.GetTimeToIteration() / 2f;
        _isDeadAnimation = isDeadAnimation;
        _squareTransform = squareTransform;
    }

    //returns true if animation is ended
    public bool Update(float elapsedTime)
    {
        _animationTime = GameManager.GetTimeToIteration() / 3f;
        _currentElapsedTime += elapsedTime;
        
        var percentComplete = _currentElapsedTime / _animationTime;

        if (_isDeadAnimation)
        {
            if (percentComplete >= 1f)
            {
                _squareTransform.localScale = new Vector3(0f, 0f, 1f);
                Reset();
                return true;
            }
            
            _squareTransform.localScale = new Vector3(1f - percentComplete, 1f - percentComplete, 1f);
            return false;
        }
        
        if (percentComplete >= 1f)
        {
            _squareTransform.localScale = new Vector3(1f, 1f, 1f);
            Reset();
            return true;
        }
        
        _squareTransform.localScale = new Vector3(percentComplete, percentComplete, 1f);
        return false;
    }
    
    private void Reset()
    {
        _currentElapsedTime = 0f;
    }
    
}
