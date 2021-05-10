using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSlip : MonoBehaviour
{
	public string roadTag = "Road";
	private string mapTerrain;
	public WheelCollider wheel;

	[SerializeField]
	public WheelDrive wheelDrive;

	private float oldAngle;
	private float oldTorque;

    public void Start() {
		var terrainController = GameObject.Find("Terrain").gameObject.GetComponent<TerrainController>();
		mapTerrain = terrainController.GetTerrainType();
		oldAngle = wheelDrive.maxAngle;
		oldTorque = wheelDrive.maxTorque;
    }

    private bool isRoad(GameObject obj) {
		return obj.CompareTag(roadTag);
	}

	private bool isOther(GameObject obj) {
		if(obj.CompareTag("NotRoad")) {
			return true;
        }
		return false;
	}

	public void ResetToDefault() {
		WheelFrictionCurve wfc;
		wfc = wheel.sidewaysFriction;
		wfc.extremumValue = 1f;
		wheel.sidewaysFriction = wfc;
		wheelDrive.maxAngle = oldAngle;
		wheelDrive.maxTorque = oldTorque;
	}

	public void Change() {
		if(mapTerrain == "Sand") {
			wheelDrive.maxAngle = 15;
			wheelDrive.maxTorque = 150;
		} else if(mapTerrain == "Grass") {
			WheelFrictionCurve wfc;
			wfc = wheel.sidewaysFriction;
			wfc.extremumValue = 0.5f;
			wheelDrive.maxTorque = 600;
			wheel.sidewaysFriction = wfc;
		} else if(mapTerrain == "Snow") {
			WheelFrictionCurve wfc;
			wfc = wheel.sidewaysFriction;
			wfc.extremumValue = 0.25f;
			wheelDrive.maxTorque = 500;
			wheel.sidewaysFriction = wfc;
		}
	}

	public void Update()
	{
		RaycastHit hit;
		float distance = 10f;
		if (Physics.Raycast(wheel.transform.position, Vector3.down, out hit, distance))
		{
			var otherGameObject = hit.collider.gameObject;
			if (otherGameObject != null)
            {
				if (isRoad(otherGameObject))
				{
					ResetToDefault();
				}
				else if (isOther(otherGameObject))
				{
					Change();
				}
			}
		}
	}
}
