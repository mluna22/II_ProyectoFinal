using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TextMesh scoreText;
    public int score;
    private float easyMoleProb;
    private float mediumMoleProb;
    private float hardMoleProb;
    private GameObject[] moles;
    public float interval = 1f;
    private float time;
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
    }

    void Update()
    {
        scoreText.text = "SCORE\n" + score.ToString("000");
        time += Time.deltaTime;
        
        numActiveMoles = 0;
        foreach (GameObject mole in moles) {
            if (mole.GetComponent<MoleController>().IsMoleActive()) {
                numActiveMoles++;
            }
        }
        
        if (numActiveMoles < moles.Length && time > interval) {
            time = 0f;
            GameObject mole = SelectMole();
            MoleType moleType = SelectMoleType();
            Debug.Log(moleType);
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
        while (moles[index].GetComponent<MoleController>().IsMoleActive()) {
            index = Random.Range(0, moles.Length);
        }
        return moles[index];
    }
}
