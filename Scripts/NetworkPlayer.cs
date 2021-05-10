using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour
{
    public GameObject localCam;
    public GameObject mipmapCam;
    public GameObject mipmapPoint;
    void Start()
    {
        if(!photonView.isMine)
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
}
