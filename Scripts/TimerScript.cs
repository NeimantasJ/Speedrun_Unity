using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text timerUI;
    public Text checkpointTimerUI;
    private float currentTime;
    private float pauseStartTime;
    private float pauseEndTime;
    private bool playTime;

    void Start()
    {
        //Time.timeScale = 1;
        playTime = false;
        checkpointTimerUI.enabled = false;
        currentTime = 0;
    }

    void Update()
    {
        if(playTime) {
            currentTime += Time.deltaTime;
            ConvertTime(timerUI);
        }
    }

    public void ShowCheckpointTime() {
        //currentTime = Time.timeSinceLevelLoad;
        ConvertTime(checkpointTimerUI);
        StartCoroutine(EnableCheckpointTimer());
    }

    public void ConvertTime(Text timerUI)
    {
        float minutes = Mathf.Floor(currentTime / 60);
        float seconds = Mathf.RoundToInt(currentTime % 60);

        if (seconds == 60)
        {
            timerUI.text = (minutes + 1).ToString("00") + " : 00";
        }
        else
        {
            timerUI.text = minutes.ToString("00") + " : " + seconds.ToString("00");
        }
    }

    private IEnumerator EnableCheckpointTimer() {
        checkpointTimerUI.enabled = true;
        yield return new WaitForSeconds(2);
        checkpointTimerUI.enabled = false;
    }

    public void StopTimer() {
        //Time.timeScale = 0;
        playTime = false;
        pauseStartTime = currentTime;
    }

    public void PlayTimer() {
        //Time.timeScale = 1;
        playTime = true;
        pauseEndTime = currentTime;
        currentTime = currentTime - (pauseEndTime - pauseStartTime);
    }

    public void RestartTimer() {
        //Time.timeScale = 1;
        PlayTimer();
        checkpointTimerUI.enabled = false;
        currentTime = 0;
    }

    public float GetTime() {
        return currentTime;
    }
}
