using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TextMeshPro scoreText;
    public TextMeshPro highScoreText;
    void Start()
    {
        scoreText.text = "SCORE\n" + PlayerPrefs.GetInt("score").ToString("000");
        highScoreText.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("highscore").ToString("000");
    }
}
