using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private RectTransform mainMenu;

    [SerializeField]
    private RectTransform optionsMenu;

    [SerializeField]
    private RectTransform levelsMenu;

    [SerializeField]
    private RectTransform instructionsMenu;

    public int levelCount;

    private static void Show(Component component) {
        component.gameObject.SetActive(true);
    }

    private static void Hide(Component component) {
        component.gameObject.SetActive(false);
    }

    private void Start() {
        ShowMainMenu();
    }

    public void StartGame(int i) {
        Scenes.LoadOtherScene(i);
    }

    public void ExitGame() {
        Scenes.ExitGame();
    }

    public void ShowMainMenu() {
        Show(mainMenu);
        Hide(optionsMenu);
        Hide(levelsMenu);
        Hide(instructionsMenu);
    }

    public void ShowOptionsMenu()
    {
        Show(optionsMenu);
        Hide(levelsMenu);
        Hide(mainMenu);
        Hide(instructionsMenu);
    }

    public void ShowLevelsMenu() {
        Show(levelsMenu);
        Hide(optionsMenu);
        Hide(mainMenu);
        Hide(instructionsMenu);
        SetRecordTime();
    }

    public void ShowInstructionsMenu() {
        Show(instructionsMenu);
        Hide(optionsMenu);
        Hide(mainMenu);
        Hide(levelsMenu);
    }

    public void SetRecordTime()
    {
        for(int i = 1; i <= levelCount; i++)
        {
            if(PlayerPrefs.HasKey("Level" + i))
            {
                continue;
            }
            Text text = GameObject.Find("Record" + i).gameObject.GetComponent<Text>();
            text.text = FormatTime(PlayerPrefs.GetFloat("Track" + i));
        }
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
