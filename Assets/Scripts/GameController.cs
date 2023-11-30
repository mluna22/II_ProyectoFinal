using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    enum MoleType {
        Easy,
        Medium,
        Hard,
        Bad
    }
    public TextMesh scoreText;
    public int score;
    private float easyMoleProb;
    private float mediumMoleProb;
    private float hardMoleProb;
    private GameObject[] pipes;
    public float interval = 1f;
    private float time;
    private int numActivePipes ;

    void Start()
    {
        score = 0;
        numActivePipes = 0;
        scoreText.text = "SCORE\n000";
        pipes = GameObject.FindGameObjectsWithTag("MolePipe");
        time = 0f;

        easyMoleProb = 1/2f;
        mediumMoleProb = easyMoleProb + 1/4f;
        hardMoleProb = mediumMoleProb + 1/8f;
    }

    void Update()
    {
        scoreText.text = "SCORE\n" + score.ToString("000");
        time += Time.deltaTime;
        
        if (time > interval) {
            time = 0f;
            GameObject pipe = SelectPipe();
            MoleType moleType = SelectMoleType();
            pipe.GetComponent<PipeController>().ActivateMole();
            numActivePipes++;
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

    GameObject SelectPipe() {
        int index = Random.Range(0, pipes.Length);
        do {
            index = Random.Range(0, pipes.Length);
        } while (pipes[index].GetComponent<PipeController>().IsMoleActive() && numActivePipes < pipes.Length);
        return pipes[index];
    }
}
