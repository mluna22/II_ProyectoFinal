using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayTextController : MonoBehaviour
{
    public string sceneName = "";
    void OnPointerClick() {
        GameObject.Find("Player").transform.position = new Vector3(0f, 7f, -7f);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
