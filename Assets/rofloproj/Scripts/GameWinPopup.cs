using System;
using UnityEngine;
using UnityEngine.UI;

public class GameWinPopup : MonoBehaviour
{
    public Text LevelText;
    public Text SavedJelly;
    public Text Multiplier;
    public Text Points;
    public Text BestScore;

    internal void SetTexts(string levelText, string savedJelly, int multiplier, int points, bool isBestScore)
    {
        BestScore.gameObject.SetActive(false);
        LevelText.text = $"LEVEL - {levelText}";
        SavedJelly.text = $"SAVED: {savedJelly}";
        Multiplier.text = $"MULTIPLIER: X{multiplier}";
        Points.text = $"POINTS: {points}";
        if (isBestScore)
        {
            BestScore.gameObject.SetActive(true);
            BestScore.text = $"BEST SCORE!";
        }

    }
}
