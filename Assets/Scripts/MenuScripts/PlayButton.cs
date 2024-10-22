using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void Play()
    {
        if (!CellsCount.IsDigits())
        {
            return;
        }
        PlayerPrefs.SetInt("Player1", CellsCount.GetCellsCountPlayer1());
        PlayerPrefs.SetInt("Player2", CellsCount.GetCellsCountPlayer2());
        
        SceneManager.LoadScene("Game");
    }
}
