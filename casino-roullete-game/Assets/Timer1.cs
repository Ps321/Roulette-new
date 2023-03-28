using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Components;
using Commands;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ViewModel;

public class Timer1 : MonoBehaviourPunCallbacks
{
     public CharacterTable chartacterTable;
    public float timerValue = 60.0f;
    public Animator aa;
    
    
     public Animator table;
     public GameObject roulette;
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
            StartCoroutine(cleardict());
            a.GetComponent<GamePlayInput>().OnClick();
             ButtonDict.first=0;
            
            timerValue=60;
        }

        if(timerValue<=10){
            foreach (KeyValuePair<string, int> pair in ButtonDict.myDictionary)
            {
              //  Debug.Log(pair.Key + ": " + pair.Value);
            }
            chartacterTable.currentTableActive.Value=false;
            aa.SetBool("clicked",false);
            table.SetBool("clicked",false);
            roulette.SetActive(true);

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


        IEnumerator cleardict(){
            yield return  new WaitForSeconds(5);
            ButtonDict.myDictionary.Clear();
        }
    public void NoMoreBet(){
        chartacterTable.currentTableActive.Value=false;
    }

    public void mainlobby(){
        SceneManager.LoadScene(1);
    }
}
