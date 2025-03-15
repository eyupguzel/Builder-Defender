using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : Singleton<GameSceneManager>
{
    public enum Scene
    {
        GameScene,
        MainMenuScene
    }
    public void LoadScene(Scene scene) => SceneManager.LoadScene(scene.ToString());
}
