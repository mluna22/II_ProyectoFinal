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
    public AudioSource moleHitSound;
    public GameObject floatingTextPrefab;
    void Start()
    {
        score = 0;
        numActiveMoles = 0;
        scoreText.text = "SCORE\n000";
        moles = GameObject.FindGameObjectsWithTag("Mole");
        time = 0f;

        GameObject.Find("Main Camera").transform.LookAt(GameObject.Find("Table").transform);

        easyMoleProb = 1/2f;
        mediumMoleProb = easyMoleProb + 1/4f;
        hardMoleProb = mediumMoleProb + 1/8f;

        foreach (GameObject mole in moles) {
            mole.GetComponent<MoleController>().OnMoleHit += OnMoleHit;
        }
    }

    void Update()
    {
        if (gameTime >= timeLimit) {
            EndGame();
        }
        timerText.text = "TIME\n" + ((int)(timeLimit - gameTime)).ToString("00");
        scoreText.text = "SCORE\n" + score.ToString("000");
        highScoreText.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("highscore").ToString("000");
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

    public void OnMoleHit(int reward, Transform moleTransform) {
        AddScore(reward, moleTransform);
        PlayHitSound();
    }

    public void AddScore(int reward, Transform moleTransform) {
        score += reward;
        // Spawn a floating text at the mole's position to show the reward
        GameObject floatingText = Instantiate(floatingTextPrefab, moleTransform.position + Vector3.up * 1.5f + Vector3.left, Quaternion.identity);
        // Set the text to look at camera
        floatingText.transform.LookAt(GameObject.Find("Main Camera").transform);
        floatingText.transform.Rotate(0, 180, 0);
        floatingText.transform.GetChild(0).GetComponent<TextMeshPro>().text = (reward < 0 ? "" : "+") + reward.ToString();
        floatingText.transform.GetChild(0).GetComponent<TextMeshPro>().color = (reward < 0 ? Color.red : Color.green);
        Destroy(floatingText, 1.5f);
    }

    public void PlayHitSound() {
        moleHitSound.Play();
    }

    void SaveResult() {
        PlayerPrefs.SetInt("score", score); 
        if (PlayerPrefs.HasKey("highscore")) {
            if (score > PlayerPrefs.GetInt("highscore")) {
                PlayerPrefs.SetInt("highscore", score);
            }
        } else {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    void EndGame() {
        SaveResult();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
