using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayTextController : MonoBehaviour
{
    public GameObject lookAt;
    public string sceneName = "";
    void Start() {
        GameObject.Find("Main Camera").transform.LookAt(lookAt.transform);
        GameObject.Find("Player").transform.position = new Vector3(13.77f,2.18f,-6.53f);
    }
    void OnPointerEnter() {}
    void OnPointerExit() {}

    void OnPointerClick() {
        GameObject.Find("Player").transform.position = new Vector3(-27.23f,10.23f,-77.26f);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    
}
