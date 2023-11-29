using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TextMesh scoreText;
    public int score;
    void Start()
    {
        score = 0;
        scoreText.text = "SCORE\n000";
    }

    
    void Update()
    {
        scoreText.text = "SCORE\n" + score.ToString("000");
        score++;
        Debug.Log(score.ToString("000"));
    }
}
