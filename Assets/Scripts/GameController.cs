using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshPro scoreText;
    public TextMeshPro highScoreText;
    public TextMeshPro timerText;
    public int timeLimit = 60;
    public int score;
    private float easyMoleProb;
    private float mediumMoleProb;
    private float hardMoleProb;
    private GameObject[] moles;
    public float interval = 1f;
    private float time;
    private float gameTime;
    private int numActiveMoles;

    void Start()
    {
        score = 0;
        numActiveMoles = 0;
        scoreText.text = "SCORE\n000";
        moles = GameObject.FindGameObjectsWithTag("Mole");
        time = 0f;

        easyMoleProb = 1/2f;
        mediumMoleProb = easyMoleProb + 1/4f;
        hardMoleProb = mediumMoleProb + 1/8f;

        foreach (GameObject mole in moles) {
            mole.GetComponent<MoleController>().OnMoleHit += AddScore;
        }
    }

    void Update()
    {
        if (gameTime >= timeLimit) {
            EndGame();
        }
        timerText.text = "TIME\n" + ((int)(timeLimit - gameTime)).ToString("00");
        scoreText.text = "SCORE\n" + score.ToString("000");
        time += Time.deltaTime;
        gameTime += Time.deltaTime;
        
        numActiveMoles = 0;
        foreach (GameObject mole in moles) {
            if (mole.GetComponent<MoleController>().IsMoleActive()) {
                numActiveMoles++;
            }
        }
        
        if (numActiveMoles < moles.Length && time >= interval) {
            time = 0f;
            GameObject mole = SelectMole();
            MoleType moleType = SelectMoleType();
            mole.GetComponent<MoleController>().SetMole(moleType);
            mole.GetComponent<MoleController>().ActivateMole();
        }
    }

    MoleType SelectMoleType() {
        float prob = Random.Range(0f, 1f);
        if (prob < easyMoleProb) {
            return MoleType.Easy;
        } else if (prob < mediumMoleProb) {
            return MoleType.Medium;
        } else if (prob < hardMoleProb) {
            return MoleType.Hard;
        } else {
            return MoleType.Bad;
        }
    }

    GameObject SelectMole() {
        int index = Random.Range(0, moles.Length);
        while (!moles[index].GetComponent<MoleController>().isDown) {
            index = Random.Range(0, moles.Length);
        }
        return moles[index];
    }

    public void AddScore(int reward) {
        score += reward;
    }

    void SaveResult() {
        if (PlayerPrefs.HasKey("HighScore")) {
            if (score > PlayerPrefs.GetInt("HighScore")) {
                PlayerPrefs.SetInt("HighScore", score);
            }
        } else {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    void EndGame() {
        SaveResult();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
