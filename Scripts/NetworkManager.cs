using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : Photon.PunBehaviour
{
    public GameObject lobbyCam;
    public Transform spawnPoint;
    public RectTransform lobbyUI;
    public Text statusText;

    public const string Version = "1.0";
    public string RoomName = "Level";
    public string playerPrefabName = "Player";

    private void Start()
    {
        if(!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(Version);
        } else
        {
            InitPlayer();
        }
    }

    private void Update()
    {
        statusText.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    public override void OnConnectionFail(DisconnectCause cause)
    {
        Debug.Log("Connection Failed : " + cause.ToString());
    }

    public override void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = false, MaxPlayers = 10 };
        PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        InitPlayer();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("New Player Connected");
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        Debug.Log("Player Disconnected");
    }

    private void InitPlayer()
    {
        lobbyCam.SetActive(false);
        lobbyUI.gameObject.SetActive(false);
        GameObject player = PhotonNetwork.Instantiate(playerPrefabName, spawnPoint.position, spawnPoint.rotation, 0);
        var timerScript = GameObject.Find("Timer").gameObject.GetComponent<TimerScript>();
        timerScript.RestartTimer();
    }
}
