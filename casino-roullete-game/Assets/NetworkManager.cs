using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Commands;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject loading;
    void Start()
    {
        // Connect to Photon servers
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // Join a room
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // If no room is available, create a new one
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room " + PhotonNetwork.CurrentRoom.Name);
        gameObject.GetComponent<Timer1>().enabled=true;
        
    }
    private void Update() {
        if(ButtonDict.loadedfirst==1){
            loading.SetActive(false);
        }

        
    }

    
}
