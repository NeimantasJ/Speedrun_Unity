using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MipMap : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private GameObject minimap;

    private GameObject Car;
    private GameObject MipMapCam;
    private GameObject Point;

    // Start is called before the first frame update
    void Start()
    {
        Car = GameObject.Find("SportCar");
        lineRenderer = GetComponent<LineRenderer>();
        minimap = this.gameObject;

        int num_of_point = minimap.transform.childCount;
        lineRenderer.positionCount = num_of_point + 1;

        for(int x = 0; x < num_of_point; x++) {
            lineRenderer.SetPosition(x,
                new Vector3(minimap.transform.GetChild(x).transform.position.x,
                4,
                minimap.transform.GetChild(x).transform.position.z));
        }

        lineRenderer.SetPosition(num_of_point, lineRenderer.GetPosition(0));

        lineRenderer.startWidth = 12f;
        lineRenderer.endWidth = 12f;
    }
}
