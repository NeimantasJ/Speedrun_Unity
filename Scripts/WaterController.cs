using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterController : MonoBehaviour
{
	private PauseController pauseController;
	private Text healthText;

	private int health = 10;


	private bool isWater(GameObject obj) {
		if(obj.CompareTag("Water")) {
			return true;
		}
		return false;
	}

	private bool isEnemy(GameObject obj)
	{
		if (obj.CompareTag("Player"))
		{
			return true;
		}
		return false;
	}

	public void OnCollisionEnter(Collision collision) {
        if(isWater(collision.gameObject)) {
			pauseController.ShowCrashPanel();
		}
		if(isEnemy(collision.gameObject) && health > 1)
        {
			health -= 1;
			healthText.text = "Health = " + health;
        } else if(health == 1)
        {
			pauseController.ShowCrashPanel();
			healthText.text = "Health = 0";
		}
    }

    public void Start()
    {
		pauseController = GameObject.Find("Canvas").gameObject.GetComponent<PauseController>();
		healthText = GameObject.Find("Health").gameObject.GetComponent<Text>();
	}
}
