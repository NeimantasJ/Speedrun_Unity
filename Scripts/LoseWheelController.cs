using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseWheelController : MonoBehaviour
{
    public Animation leftWheelAnimation;
    public Animation rightWheelAnimation;
    public GameObject leftWheelObject;
    public GameObject rightWheelObject;
    private int collisionCount = 0;
    public string rocksTag = "Rocks";

    private bool isRocks(GameObject obj) {
        return obj.CompareTag(rocksTag);
    }

    public void OnCollisionEnter(Collision collision) {
        if(isRocks(collision.gameObject)) {
            Debug.Log("Collision to rock");
            if(collisionCount == 0) {
                leftWheelAnimation.Play("Loose Wheels");
                StartCoroutine(HideWheel(leftWheelObject));
            } else if(collisionCount == 1) {
                rightWheelAnimation.Play("Loose Wheel Other Side");
                StartCoroutine(HideWheel(rightWheelObject));
            }
            collisionCount++;
        }
    }

    public IEnumerator HideWheel(GameObject wheelObject) {
        yield return new WaitForSeconds(1);
        wheelObject.SetActive(false);
    }
}
