using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Commands;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject loading;
    public GameObject Master;
    public GameObject Client;
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
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 });
    }

    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.IsMasterClient){
           
        }else{
           
        }
        Debug.Log("Joined room " + PhotonNetwork.CurrentRoom.Name);
        gameObject.GetComponent<Timer1>().enabled=true;
        
    }
    private void Update() {

        if(PhotonNetwork.IsMasterClient){
            ButtonDict.masterClient=true;
         //  Debug.Log("Masterrr");
        }
        else{
             ButtonDict.masterClient=false;
          // Debug.Log("Clienttttt");
        }
        if(ButtonDict.loadedfirst==1){
            loading.SetActive(false);
        }

        
    }

    
}
