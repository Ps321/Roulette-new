using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Components;

public class Timer : MonoBehaviourPunCallbacks
{
    public float timerValue = 60.0f;
    public bool enabletimer=false;

    public GameObject a;

    [PunRPC]
    void UpdateTimer(float value)
    {
        timerValue = value;
        Debug.Log(timerValue);
    }

void Start()
    {
        // Initialize the timer value for all clients
        photonView.RPC("UpdateTimer", RpcTarget.All, timerValue);

        // Start the countdown for the local player only
        if (photonView.IsMine)
        {
            enabled=true;
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(enabled==true){
            Countdown();
       

        if(timerValue <=0){
            a.GetComponent<GamePlayInput>().OnClick();
            enabled=false;
        }
         }
    }
    void Countdown()
    {
        if (timerValue > 0)
        {
            timerValue -= Time.deltaTime;
            photonView.RPC("UpdateTimer", RpcTarget.All, timerValue);
        }
    }
}
