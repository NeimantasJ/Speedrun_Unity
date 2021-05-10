using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
	private PauseController pauseController;

	private bool isWater(GameObject obj) {
		if(obj.CompareTag("Water")) {
			return true;
		}
		return false;
	}

    public void OnCollisionEnter(Collision collision) {
        if(isWater(collision.gameObject)) {
			pauseController.ShowCrashPanel();
		}
    }

    public void Start()
    {
		pauseController = GameObject.Find("Canvas").gameObject.GetComponent<PauseController>();
	}
}
