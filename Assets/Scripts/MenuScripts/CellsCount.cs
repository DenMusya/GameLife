using System.Linq;
using TMPro;
using UnityEngine;

public class CellsCount : MonoBehaviour
{
    private static TextMeshProUGUI _player1Text;
    private static TextMeshProUGUI _player2Text;

    private const int UpperBound = 100000;
    private const int LowerBound = 0;
    
    private void Start()
    {
        var player1 = transform.Find("Player1/InputField (TMP)/Text Area/Text");
        var player2 = transform.Find("Player2/InputField (TMP)/Text Area/Text");
        
        _player1Text = player1.GetComponent<TextMeshProUGUI>();
        _player2Text = player2.GetComponent<TextMeshProUGUI>();
    }

    public static bool IsDigits()
    {
        var str1 = _player1Text.text.Remove(_player1Text.text.Length - 1);
        var str2 = _player2Text.text.Remove(_player2Text.text.Length - 1);
        
        return str1.Length >= 1 && str2.Length >= 1 && str1.All(char.IsDigit) &&
               int.Parse(str1) <= UpperBound &&
               int.Parse(str1) >= LowerBound &&
               str2.All(char.IsDigit) && int.Parse(str2) <= UpperBound &&
               int.Parse(str2) >= LowerBound;
    }

    public static int GetCellsCountPlayer1()
    {
        return int.Parse(_player1Text.text.Remove(_player1Text.text.Length - 1));
    }
    
    public static int GetCellsCountPlayer2()
    {
        return int.Parse(_player2Text.text.Remove(_player2Text.text.Length - 1));
    }
}
