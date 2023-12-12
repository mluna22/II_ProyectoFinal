using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayTextController : MonoBehaviour
{
    public GameObject camera;
    public string sceneName = "";
    void Start() {
        camera = GameObject.Find("Main Camera");
        camera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    void OnPointerClick() {
        GameObject.Find("Player").transform.position = new Vector3(0f, 7f, -7f);
        camera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
