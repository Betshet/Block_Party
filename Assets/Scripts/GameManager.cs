using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI StartText;

    [HideInInspector]
    public int iBlocksPlaced = 0;

    [SerializeField]
    TextMeshProUGUI BlockCounterText;

    [SerializeField]
    GameObject GameplayTexts;

    [SerializeField]
    List<TextMeshProUGUI> BlockToPlaceCounters;

    bool bGameStarted = false;

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

    private void Awake()
    {
        CurrentLevel = BlocksToPlaceLevel1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BlockCounterText.enabled = false;
        GameplayTexts.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !bGameStarted)
        {
            StartText.enabled = false;
            bGameStarted = true;
            BlockCounterText.enabled = true;
            GameplayTexts.SetActive(true);
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
        }
    }
}
