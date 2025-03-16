using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        transform.Find("PlayButton").GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene(1); });
        transform.Find("QuitButton").GetComponent<Button>().onClick.AddListener(() => { Application.Quit(); });
    }
}
