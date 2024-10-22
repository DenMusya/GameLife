using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TextMeshProUGUI scoreTextPlayer1;
    [SerializeField] private TextMeshProUGUI scoreTextPlayer2;
    
    public void End(Vector2Int playersScore)
    {
        endGamePanel.SetActive(true);
        scoreTextPlayer1.text = "- " + (playersScore.x + ScoreBoard.GetRemainingPlayerSquares(PlayerType.Player1)).ToString() + " points";
        scoreTextPlayer2.text = "- " + (playersScore.y + ScoreBoard.GetRemainingPlayerSquares(PlayerType.Player2)).ToString() + " points";
    }
}
