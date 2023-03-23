using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Components;
using Commands;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer1 : MonoBehaviourPunCallbacks
{
    public float timerValue = 60.0f;
    

    public Text textval;
    public bool enabletimer=false;
     public ButtonDict dd;

    public GameObject a;

    [PunRPC]
    void UpdateTimer(float value)
    {
        timerValue = value;
       
       
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
            
            foreach (KeyValuePair<string, int> pair in dd.myDictionary)
            {
                Debug.Log(pair.Key + ": " + pair.Value);
            }
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
