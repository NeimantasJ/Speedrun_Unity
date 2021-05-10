using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLights : MonoBehaviour
{
    public GameObject vehicle;
    public GameObject reverseLights;
    public GameObject brakeLights;
    public GameObject frontLights;
    private Rigidbody carRB;
    private Renderer reverseLight;
    private Renderer brakeLight;

    private bool activeFrontLights = true;

    private Vector3 movement;
    private Vector3 prevpos;
    private Vector3 newpos;
    private Vector3 fwd;

    void Start()
    {
        carRB = vehicle.GetComponent<Rigidbody>();
        reverseLight = reverseLights.GetComponent<Renderer>();
        brakeLight = brakeLights.GetComponent<Renderer>();
    }

    void Update() {
        newpos = transform.position;
        movement = (newpos - prevpos);

        if(Vector3.Dot(fwd, movement) < -0.1 && Input.GetKey(KeyCode.S)) {
            reverseLight.enabled = true;
        } else {
            reverseLight.enabled = false;
        }
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.S)) {
            brakeLight.enabled = true;
        } else {
            brakeLight.enabled = false;
        }
        if(activeFrontLights) {
            frontLights.SetActive(true);
        } else {
            frontLights.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.L)) {
            if(activeFrontLights) {
                activeFrontLights = false;
            } else {
                activeFrontLights = true;
            }
        }
    }

    void LateUpdate() {
        prevpos = transform.position;
        fwd = transform.forward;
    }
}
