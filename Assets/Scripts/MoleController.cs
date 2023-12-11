using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoleController : MonoBehaviour
{
    public bool isActive;
    private float speed = 5f;
    private float timeActive = 0f;
    private float timeLimit = 0f;
    private int reward;
    public float[] speeds;
    public float[] timeLimits;
    public Material[] materials;
    public int[] rewards;
    void Start() {
        isActive = false;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    void Update() {
        if (isActive && transform.localPosition.y < 1f) {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        } else if (!isActive && transform.localPosition.y > 0f) {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        timeActive += Time.deltaTime;
    }

    public void ActivateMole() { 
        isActive = true;
        timeActive = 0f;
        Debug.Log(GetComponent<Renderer>().material);
    }

    public void DeactivateMole() {
        isActive = false;
    }

    public bool IsMoleActive() {
        return isActive;
    }

    void OnPointerEnter() {
        GetComponent<Renderer>().material.color *= 0.75f;
    }

    void OnPointerExit() {
        GetComponent<Renderer>().material.color /= 0.75f;
    }

    public Material GetMaterial(int type) { return materials[type]; }
    public float GetSpeed(int type) { return speeds[type]; }
    public int GetReward(int type) { return rewards[type]; }
    public float GetLimits(int type) { return timeLimits[type]; }

    public void SetMole(MoleType type) {
        Debug.Log((int)type);
        GetComponent<Renderer>().material = GetMaterial((int)type);
        speed = GetSpeed((int)type);
        reward = GetReward((int)type);
        timeLimit = GetLimits((int)type);
    }

}
