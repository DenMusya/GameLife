using UnityEngine;

public class SquareAnimation
{
    private readonly bool _isDeadAnimation;
    private readonly Transform _squareTransform;
    private const float AnimationTime = .3f; 
    
    private float _currentElapsedTime;
    
    public SquareAnimation(bool isDeadAnimation, Transform squareTransform)
    {
        _isDeadAnimation = isDeadAnimation;
        _squareTransform = squareTransform;
    }

    //returns true if animation is ended
    public bool Update(float elapsedTime)
    {
        _currentElapsedTime += elapsedTime;
        
        var percentComplete = _currentElapsedTime / AnimationTime;

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
