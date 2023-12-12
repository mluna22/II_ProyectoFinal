using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoleController : MonoBehaviour
{
    public bool isActive;
    public bool isDown;
    private float speed = 5f;
    private float timeActive = 0f;
    private float timeLimit = 0f;
    private int reward;
    public float[] speeds;
    public float[] timeLimits;
    public Material[] materials;
    public int[] rewards;
    public delegate void Message(int points);
    public event Message OnMoleHit;
    void Start() {
        isActive = false;
        isDown = true;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    void Update() {
        if (timeActive > timeLimit) {
            DeactivateMole();
        }
        if (isActive && transform.localPosition.y < 1f) {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        } else if (!isActive && transform.localPosition.y > 0f) {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        if (isActive && transform.localPosition.y >= 1f) {
            isDown = false;
        } else if (!isActive && transform.localPosition.y <= 0f) {
            isDown = true;
        }
        timeActive += Time.deltaTime;
    }

    public void ActivateMole() { 
        isActive = true;
        timeActive = 0f;
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

    void OnPointerClick() {
        if (isActive) {
            DeactivateMole();
            OnMoleHit?.Invoke(reward);
        }
    }

    public Material GetMaterial(int type) { return materials[type]; }
    public float GetSpeed(int type) { return speeds[type]; }
    public int GetReward(int type) { return rewards[type]; }
    public float GetLimits(int type) { return timeLimits[type]; }

    public void SetMole(MoleType type) {
        GetComponent<Renderer>().material = GetMaterial((int)type);
        speed = GetSpeed((int)type);
        reward = GetReward((int)type);
        timeLimit = GetLimits((int)type);
    }

}
