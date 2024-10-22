using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private static TextMeshProUGUI _scoreTextPlayer1;
    private static TextMeshProUGUI _scoreTextPlayer2;
    
    private static int _player1Squares = 10000;
    private static int _player2Squares = 10000;
    
    private void Start()
    {
        var player1BoardObject = transform.Find("LeftBoard");
        var player2BoardObject = transform.Find("RightBoard");
        
        _scoreTextPlayer1 = player1BoardObject.GetComponent<TextMeshProUGUI>();
        _scoreTextPlayer2 = player2BoardObject.GetComponent<TextMeshProUGUI>();
    }

    public static void UpdateScore()
    {
        _scoreTextPlayer1.text = "Осталось   :" + _player1Squares.ToString();
        _scoreTextPlayer2.text = "Осталось   :" + _player2Squares.ToString();
    }

    
    public static int GetRemainingPlayerSquares(PlayerType playerType)
    {
        return playerType == PlayerType.Player1 ? _player1Squares : _player2Squares;
    }

    public static void DecreaseRemainingPlayerSquares(PlayerType playerType)
    {
        switch (playerType)
        {
            case PlayerType.Player1:
                _player1Squares--;
                break;
            case PlayerType.Player2:
                _player2Squares--;
                break;
            default:
                break;
        }
    }

    public static void IncreaseRemainingPlayerSquares(PlayerType playerType)
    {
        switch (playerType)
        {
            case PlayerType.Player1:
                _player1Squares++;
                break;
            case PlayerType.Player2:
                _player2Squares++;
                break;
            default:
                break;
        }
    }

    public static void SetPlayer1Score(int score)
    {
        _player1Squares = score;
    }
    
    public static void SetPlayer2Score(int score)
    {
        _player2Squares = score;
    }
}
