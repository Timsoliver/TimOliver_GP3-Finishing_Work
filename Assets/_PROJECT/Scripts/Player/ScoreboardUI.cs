using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreboardUI : MonoBehaviour
{
    [Header("UI Texts")] 
    public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;

    public void UpdateScores(Dictionary<string, int> scores)
    {
        if (scores.ContainsKey("Player 1"))
            player1Text.text = "Player 1: " + scores["Player 1"];
        if (scores.ContainsKey("Player 2"))
            player2Text.text = "Player 2: " +scores["Player 2"];
    }

}
