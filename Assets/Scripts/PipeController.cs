using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PipeController : MonoBehaviour
{
    enum MoleType {
        Easy,
        Medium,
        Hard,
        Bad
    }
    public bool isActive;
    public GameObject mole;
    public float speed = 5f;
    private float timeActive = 0f;
    void Start() {
        isActive = false;
        mole.transform.localPosition = new Vector3(0, 0, 0);
    }

    void Update() {
        if (isActive && mole.transform.localPosition.y < 1f) {
            mole.transform.Translate(Vector3.up * Time.deltaTime * speed);
        } else if (!isActive && mole.transform.localPosition.y > 0f) {
            mole.transform.Translate(Vector3.down * Time.deltaTime * speed);
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
        Debug.Log("Mouse enter");
    }
}
