using TMPro;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private RectTransform _scrollBarRectTransform;

    private const float MaxAnchor = .8f;
    private const float MinAnchor = 0f;

    private const float MaxSpeed = .001f;
    private const float MinSpeed = 5f;
    
    private void Start()
    {
        var gameSpeedText = transform.Find("GameSpeedText").gameObject;
        var handle = transform.Find("GameSpeedScroll/Sliding Area/Handle").gameObject;
        
        _text = gameSpeedText.GetComponent<TextMeshProUGUI>();
        _scrollBarRectTransform = handle.GetComponent<RectTransform>();
    }
    private void Update()
    {
        UpdateScrollbar();
        UpdateText();
    }

    private void UpdateScrollbar()
    {
        var currentAnchor = _scrollBarRectTransform.anchorMin.x;
        
        var percentageScrolled = currentAnchor / (MaxAnchor - MinAnchor);

        var newSpeed = Mathf.Min(MaxSpeed + (MinSpeed - MaxSpeed) * percentageScrolled, MinSpeed);
        
        GameManager.SetGameSpeed(newSpeed);
    }

    private void UpdateText()
    {
        _text.text = "Game Speed: " + GameManager.GetTimeToIteration().ToString("#0.000");
    }
}
