using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckpointsScript : MonoBehaviour
{
    public GameObject car;
    public string checkpointTag = "Checkpoint";
    private string levelName;
    public GameObject checkpointObject;
    public Transform[] checkpoints;

	public WheelSlip leftWheel;
	public WheelSlip rightWheel;

	private int checkpointCount;
	private int currentCheckpointID;

    public GameObject lastCheckpoint;
	private TimerScript timerScript;
	private PauseController pauseController;

    void Start()
    {
		var activeScene = SceneManager.GetActiveScene();
		levelName = activeScene.name;

		checkpointObject = GameObject.Find("Checkpoints");
		timerScript = GameObject.Find("Timer").gameObject.GetComponent<TimerScript>();
		pauseController = GameObject.Find("Canvas").gameObject.GetComponent<PauseController>();
		currentCheckpointID = 0;
		checkpointCount = checkpointObject.transform.childCount;
        checkpoints = new Transform[checkpointCount];
        for(int i = 0; i < checkpointCount; i++) {
            checkpoints[i] = checkpointObject.transform.GetChild(i);
        }
		lastCheckpoint = checkpoints[0].gameObject;
	}

    private bool IsCheckpoint(GameObject obj) {
		return obj.CompareTag(checkpointTag);
	}

	private bool IsNextCheckpoint(GameObject obj) {
		string nextCheckpointName = "Point" + (currentCheckpointID + 1);
		if(obj.name == nextCheckpointName) {
			return true;
		}
		return false;
	}

	private bool IsLastCheckpoint(GameObject obj) {
		if(lastCheckpoint.name == checkpoints[checkpoints.Length -2].name && obj.name == checkpoints[checkpoints.Length - 1].name) {
			return true;
        }
		return false;
    }

	private bool IsNewRecord()
    {
		if(PlayerPrefs.HasKey(levelName) && PlayerPrefs.GetFloat(levelName) > timerScript.GetTime() || !PlayerPrefs.HasKey(levelName))
        {
			return true;
        }
		return false;
    }

	private void SaveNewRecord()
    {
		Debug.Log(timerScript.GetTime());
		PlayerPrefs.SetFloat(levelName, timerScript.GetTime());
		PlayerPrefs.Save();
    }

	private void OnTriggerEnter(Collider other) {
		if(IsLastCheckpoint(other.gameObject)) {
			PauseCar();
			pauseController.SetFinishTime();
			if (IsNewRecord()) {
				SaveNewRecord();
				pauseController.ShowFinishPanel(true);
			} else {
				pauseController.ShowFinishPanel(false);
			}
		}
        if(IsCheckpoint(other.gameObject) && IsNextCheckpoint(other.gameObject)) {
			var checkpointSound = other.gameObject.GetComponent<AudioSource>();
			checkpointSound.Play();
            lastCheckpoint = other.gameObject;
			currentCheckpointID += 1;
			timerScript.ShowCheckpointTime();
        }
    }

	public void ToLastCheckpoint() {
		leftWheel.ResetToDefault();
		rightWheel.ResetToDefault();

		Transform vehicleTransform = car.transform;
		vehicleTransform.rotation = Quaternion.identity;

		Transform closest = lastCheckpoint.transform;

		vehicleTransform.rotation = closest.rotation;
		vehicleTransform.position = closest.position;

		var vehicleBody = vehicleTransform.gameObject.GetComponent<Rigidbody>();
		car.transform.rotation = closest.rotation * Quaternion.Euler(0, 90, 0);
		vehicleBody.velocity = Vector3.zero;
		vehicleBody.angularVelocity = Vector3.zero;
	}

	public void PauseCar()
    {
		var car = gameObject;
		var rb = car.GetComponent<Rigidbody>();
		rb.angularDrag = 5f;
		rb.drag = 1f;
		var moveScript = car.GetComponent<WheelDrive>();
		moveScript.maxTorque = 0;
		//moveScript.enabled = false;
    }

	public void ResumeCar()
	{
		var car = gameObject;
		var rb = car.GetComponent<Rigidbody>();
		rb.angularDrag = 0.05f;
		rb.drag = 0.1f;
		var moveScript = car.GetComponent<WheelDrive>();
		moveScript.maxTorque = 800;
		//moveScript.enabled = true;
	}

	void Update() {
		if(Input.GetKeyUp(KeyCode.R)) {
			ToLastCheckpoint();
		}
        if (!pauseController.IsShowingAnything()) {
			ResumeCar();
        } else
        {
			PauseCar();
        }
	}
}
