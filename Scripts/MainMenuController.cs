using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void ShowInstructionsMenu() {
        Show(instructionsMenu);
        Hide(optionsMenu);
        Hide(mainMenu);
        Hide(levelsMenu);
    }
}
