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
    public Animator Timer;
     public Animator Betok;
    
    
     public Animator table;
     public GameObject roulette;
    public Text textval;
    public bool enabletimer=false;
     public ButtonDict dd;

    public GameObject a;

    public Text messages1;

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

   
    void Update()
    {

        if(enabled==true){
            Countdown();
       
       /* if(timerValue==30){
            Application.Quit();
        }*/
         textval.text="0 : "+ Mathf.Round(timerValue).ToString();
        if(timerValue <=0){
            StartCoroutine(cleardict());
            a.GetComponent<GamePlayInput>().OnClick();
             ButtonDict.first=0;
              chartacterTable.currentTableActive.Value=true;
            timerValue=60;
          
        }
        if(ButtonDict.first==1 && timerValue>=10){
            Betok.SetBool("betok",true);
        }
        else{
            Betok.SetBool("betok",false);
        }
        if(timerValue<=15 &&  timerValue>=10){
                Timer.SetBool("glow",true);
        }

        if(timerValue<=10){
            
            Timer.SetBool("glow",false);
            foreach (KeyValuePair<string, int> pair in ButtonDict.myDictionary)
            {
              //  Debug.Log(pair.Key + ": " + pair.Value);
            }
            chartacterTable.currentTableActive.Value=false;
            aa.SetBool("clicked",false);
            table.SetBool("clicked",false);
            roulette.SetActive(true);

        }
        messages();
         }
    }

    void messages(){
        if(ButtonDict.first==0){
            messages1.text="Please Bet to start Game. MinimumBet=1";
        }
        if(ButtonDict.first==1){
            messages1.text="Bet Now";
        }
        if(timerValue<=10){
           messages1.text="Timer Up";
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
