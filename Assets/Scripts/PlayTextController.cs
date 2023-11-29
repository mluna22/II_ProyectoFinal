using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayTextController : MonoBehaviour
{
    public string sceneName = "";
    void OnPointerClick() {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
