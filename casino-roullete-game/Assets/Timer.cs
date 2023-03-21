using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Components;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviourPunCallbacks
{
    public float timerValue = 60.0f;

    public Text textval;
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
       
       /* if(timerValue==30){
            Application.Quit();
        }*/
         textval.text="00:"+ Mathf.Round(timerValue).ToString();
        if(timerValue <=0){
            a.GetComponent<GamePlayInput>().OnClick();
            timerValue=80;
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

    public void mainlobby(){
        SceneManager.LoadScene(1);
    }
}
