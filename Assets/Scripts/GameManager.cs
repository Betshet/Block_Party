using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int iBlocksPlaced = 0;

    [SerializeField]
    public TextMeshProUGUI BlockCounterText;

    [SerializeField]
    public GameObject GameplayTexts;

    [SerializeField]
    TextMeshProUGUI GameOverText;

    [SerializeField]
    List<TextMeshProUGUI> BlockToPlaceCounters;

    [HideInInspector]
    public bool bGameStarted = false;

    [SerializeField]
    public List<int> BlocksToPlaceLevel1;
    [SerializeField]
    public List<int> BlocksToPlaceLevel2;
    [SerializeField]
    public List<int> BlocksToPlaceLevel3;
    [SerializeField]
    public List<int> BlocksToPlaceLevel4;

    [HideInInspector]
    public List<int> CurrentLevel;

    public int iCurrentLevel = 1;
    public bool bPauseGame = false;
    public bool bGameOver = false;
    public bool bNewDay = false;

    private void Awake()
    {
        CurrentLevel = BlocksToPlaceLevel1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOverText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(bGameOver)
            {
                GameOverText.enabled = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (!bGameStarted) return;

        for (int i = 0; i < BlockToPlaceCounters.Count; i++)
        {
            BlockToPlaceCounters[i].text = CurrentLevel[i].ToString();
        }

        BlockCounterText.text = iBlocksPlaced.ToString();
    }

    public void ChangeLevel(int level)
    {
        iCurrentLevel = level;
        print(iCurrentLevel);
        switch (level)
        {
        case 1:
            CurrentLevel = BlocksToPlaceLevel1;
            break;
        case 2:
            CurrentLevel = BlocksToPlaceLevel2;
            break;
        case 3:
            CurrentLevel = BlocksToPlaceLevel3;
            break;
        case 4:
            CurrentLevel = BlocksToPlaceLevel4;
            break;
        default:
            CurrentLevel = BlocksToPlaceLevel1;
            break;
        }
    }

    public void GameOver()
    {
        GameOverText.enabled = true;
        bPauseGame = true;
        bGameOver = true;
    }
}
