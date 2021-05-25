using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour
{
    public GameObject localCam;
    public GameObject mipmapCam;
    public GameObject mipmapPoint;
    private GameObject spawnPoint;
    void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint").gameObject;
        if (!photonView.isMine)
        {
            localCam.SetActive(false);
            mipmapCam.SetActive(false);
            mipmapPoint.SetActive(false);

            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

            foreach(MonoBehaviour script in scripts)
            {
                if (script is NetworkPlayer) continue;
                else if (script is PhotonView) continue;
                script.enabled = false;
            }
        }
    }

    public void ResetPlayer()
    {
        if (photonView.isMine)
        {
            Rigidbody rb = photonView.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            photonView.gameObject.transform.position = spawnPoint.transform.position;
            photonView.gameObject.transform.rotation = spawnPoint.transform.rotation;
        }
    }
}
