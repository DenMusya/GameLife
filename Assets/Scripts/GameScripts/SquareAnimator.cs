using UnityEngine;

public class SquareAnimator : MonoBehaviour
{

    [SerializeField]private GameObject squareToAnimate;
    
    private const float FrameRate = 60f;
    private const float AnimationSpeed = 1f;
    private const float AnimationTime = 1f / FrameRate;
    
    private float _elapsedTime;
    
    private SquareAnimation _deadAnimation;
    private SquareAnimation _birthAnimation;

    private bool _isDead = false;
    private bool _isBirth = false;

    private void Start()
    {
        _deadAnimation = new SquareAnimation(true, squareToAnimate.GetComponent<Transform>());
        _birthAnimation = new SquareAnimation(false, squareToAnimate.GetComponent<Transform>());
    }
    
    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        
        if (_isDead && _elapsedTime * AnimationSpeed >= AnimationTime)
        {
            if (_deadAnimation.Update(_elapsedTime * AnimationSpeed))
            {
                AnimationEnded();
            }
            _elapsedTime = 0f;
        }

        if (_isBirth && _elapsedTime * AnimationSpeed >= AnimationTime)
        {
            if (_birthAnimation.Update(_elapsedTime * AnimationSpeed))
            {
                AnimationEnded();
            }
            _elapsedTime = 0f;
        }
    }

    public void PlayDeadAnimation()
    {
        _isDead = true;
        _elapsedTime = 0;
    }

    public void PlayBirthAnimation()
    {
        _isBirth = true;
        _elapsedTime = 0;
    }

    private void AnimationEnded()
    {
        _isBirth = false;
        _isDead = false;
        
        this.enabled = false;
    }
}
