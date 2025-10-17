using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject TitleScreen;

    [SerializeField]
    GameObject TutoScreens;

    [SerializeField]
    GameObject NightScreen;

    [SerializeField]
    GameObject TutoScreen1;
    [SerializeField]
    GameObject TutoScreen2;
    [SerializeField]
    GameObject TutoScreen3;
    [SerializeField]
    GameObject TutoScreen4;
    [SerializeField]
    GameObject TutoScreen5;

    int iTutoScreenState = 1;

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    public Material gradient_day;

    [SerializeField]
    public Material gradient_night;

    [SerializeField]
    GameObject sun;

    [SerializeField]
    GameObject moon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TutoScreen1.SetActive(false);
        TutoScreen2.SetActive(false);
        TutoScreen3.SetActive(false);
        TutoScreen4.SetActive(false);
        TutoScreen5.SetActive(false);
        TutoScreens.SetActive(false);
        NightScreen.SetActive(false);
        gameManager.GameplayTexts.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_TitleScreen()
    {
        TitleScreen.SetActive(false);
        TutoScreens.SetActive(true);
        TutoScreen1.SetActive(true);
    }

    public void OnClick_TutoScreen()
    {
        switch(iTutoScreenState)
        {
            case 1:
                iTutoScreenState++;
                TutoScreen1.SetActive(false);
                TutoScreen2.SetActive(true);
                break;
            case 2:
                iTutoScreenState++;
                TutoScreen2.SetActive(false);
                TutoScreen3.SetActive(true);
                break;
            case 3:
                iTutoScreenState++;
                TutoScreen3.SetActive(false);
                TutoScreen4.SetActive(true);
                gameManager.GameplayTexts.SetActive(true);
                break;
            case 4:
                iTutoScreenState++;
                TutoScreen4.SetActive(false);
                TutoScreen5.SetActive(true);
                gameManager.bGameStarted = true;
                break;
            case 5:
                TutoScreen5.SetActive(false);
                TutoScreens.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void StartNight()
    {
        gameManager.bPauseGame = true;
        NightScreen.SetActive(true);
        RenderSettings.skybox = gradient_night;
        sun.SetActive(false);
        moon.SetActive(true);
    }

    public void NewDay()
    {
        RenderSettings.skybox = gradient_day;
        sun.SetActive(true);
        moon.SetActive(false);
    }

    public void OnClick_NightButton()
    {
        NightScreen.SetActive(false);
        gameManager.ChangeLevel(gameManager.iCurrentLevel++);
        gameManager.bNewDay = true;
        gameManager.bPauseGame = false;
    }
}
