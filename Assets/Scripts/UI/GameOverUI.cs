using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : Singleton<GameOverUI>
{
    private Dictionary<ResourceTypeSO, int> resources;
    private Dictionary<EnemiesSO, int> enemies;

    [SerializeField] private TextMeshProUGUI[] resourceSummaryTexts;
    [SerializeField] private TextMeshProUGUI[] enemySummaryTexts;
    [SerializeField] private TextMeshProUGUI elepsedTimeText;

    private Transform backGround;

    private int i,j;

    private void Start()
    {
        backGround = transform.Find("background");

        backGround.Find("restartBtn").GetComponent<Button>().onClick.AddListener(() => { GameSceneManager.Instance.LoadScene(GameSceneManager.Scene.GameScene); });
        backGround.Find("mainmenuBtn").GetComponent<Button>().onClick.AddListener(() => { GameSceneManager.Instance.LoadScene(GameSceneManager.Scene.MainMenuScene); });

        Hide();
    }
    public void UpdateUI()
    {
        resources = SummaryManager.Instance.GetAllResources();
        enemies = SummaryManager.Instance.GetAllDiededEnemies();
        elepsedTimeText.text = "You Survived " + EnemyWaveManager.Instance.GetElapsedTime().ToString("F0") + " Minutes";
        foreach (var enemy in enemies)
        {
            enemySummaryTexts[j++].text = $" : {enemy.Value}";
        }

        foreach (var resource in resources)
        {
            resourceSummaryTexts[i++].text = $" : {resource.Value}";
        }

        Show();
    }
    private void Hide()
    {
        backGround.gameObject.SetActive(false);
    }
    private void Show()
    {
        backGround.gameObject.SetActive(true);
    }
}
