using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private RectTransform pausePanel;
    [SerializeField]
    private RectTransform crashPanel;
    [SerializeField]
    private RectTransform finishPanel;

    [SerializeField]
    private TimerScript timerScript;
    [SerializeField]
    private Text finishTimeText;

    private static void Show(Component component) {
        component.gameObject.SetActive(true);
    }

    private static void Hide(Component component) {
        component.gameObject.SetActive(false);
    }

    public void RestartLevel() {
        timerScript.RestartTimer();
        Scenes.RestartScene();
    }

    public void GoToScene(int i) {
        //Time.timeScale = 1;
        Scenes.LoadOtherScene(i);
    }

    public void HidePausePanel() {
        //timerScript.PlayTimer();
        Hide(pausePanel);
    }

    public void ShowCrashPanel() {
        timerScript.StopTimer();
        Show(crashPanel);
    }

    public void HideCrashPanel() {
        timerScript.PlayTimer();
        Hide(crashPanel);
    }

    public void ShowFinishPanel(bool showNewRecord) {
        timerScript.StopTimer();
        if(!showNewRecord)
        {
            var newRecord = finishPanel.gameObject.transform.Find("NewRecord").gameObject;
            newRecord.SetActive(false);
        }
        Show(finishPanel);
    }

    public void HideFinishPanel() {
        timerScript.PlayTimer();
        Hide(finishPanel);
    }

    public void SetFinishTime() {
        var currentTime = timerScript.GetTime();
        float minutes = Mathf.Floor(currentTime / 60);
        float seconds = Mathf.RoundToInt(currentTime % 60);

        finishTimeText.text = "Time : " + minutes.ToString("00") + " : " + seconds.ToString("00");
    }

    public bool IsShowingAnything() {
        if(pausePanel.gameObject.activeSelf || finishPanel.gameObject.activeSelf || crashPanel.gameObject.activeSelf) {
            return true;
        }
        return false;
    }

    public void Start() {
        //timerScript.PlayTimer();
        /*HidePausePanel();
        HideCrashPanel();
        HideFinishPanel();*/
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) {
            //timerScript.StopTimer();
            Show(pausePanel);
        }
    }
}
