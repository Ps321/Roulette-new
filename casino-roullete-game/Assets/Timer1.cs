using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;
using Components;
using Commands;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ViewModel;
using System;

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
    bool betokclicked=false;
     public ButtonDict dd;
     private bool updatemoney=true;

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
           DateTime now = DateTime.Now;
        int seconds = now.Second;
        timerValue = 60 - seconds;
        
        
        
        if(timerValue==60)
        {
            timerValue=0;
            enabled=false;
        }
       /* if(timerValue==30){
            Application.Quit();
        }*/
         textval.text="0 : "+ Mathf.Round(timerValue).ToString();
        if(timerValue <=0){
           StartCoroutine(rulefetch());
           updatemoney=true;
            if(ButtonDict.myDictionary.Count!=0){
             if(ButtonDict.previousbet.Count!=0){
                ButtonDict.previousbet.Clear();
             }
                ButtonDict.previousbet=new Dictionary<string, int>(ButtonDict.myDictionary);;
                 
               
            }else{
                ButtonDict.previousbet.Clear();
            }
           
            
             ButtonDict.buttonenable=true;
            ButtonDict.betok=false;
            StartCoroutine(cleardict());
            a.GetComponent<GamePlayInput>().OnClick();
             ButtonDict.first=0;
             ButtonDict.first_1=0;
             ButtonDict.buttonoffset=0f;
              chartacterTable.currentTableActive.Value=true;
            timerValue=60;
            betokclicked=false;
          
        }
        if(ButtonDict.first==1 && timerValue>=10 && betokclicked==false){
            
            Betok.SetBool("betok",true);
           
        }
        else{
            Betok.SetBool("betok",false);
        }
        if(timerValue<=15 &&  timerValue>=10){
                Timer.SetBool("glow",true);
        }

        if(timerValue<=10){

            updatemoney=true;
             
            ButtonDict.buttonenable=false;
            ButtonDict.winnernumber=1;

            GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
        
        foreach (GameObject button in buttons)
        {
            Animator animator = button.GetComponent<Animator>();
            
            if (animator != null)
            {
                animator.SetBool("win", false);
            }
        }
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
        if(enabled==false){
            StartCoroutine(enableit());
        }
        messages();
         }


         if(ButtonDict.winloss==true){
          //  StartCoroutine(winloss(ButtonDict.paymentWin,ButtonDict.paymentLost));
         }


            /*Update money*/

           


    }



    








    IEnumerator enableit(){
        yield return new WaitForSeconds(1);
        enabled=true;
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

     IEnumerator rulefetch()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("id"));
        


        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/rulefetch.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string s= www.downloadHandler.text.Trim();
                if(s=="Error"){
                   Debug.Log("error");
                }
                else{
                   ButtonDict.rule=int.Parse(s);
                }
            }
        }
    }


        IEnumerator cleardict(){
            yield return  new WaitForSeconds(5);
         
            ButtonDict.myDictionary.Clear();
         
        }
    public void NoMoreBet(){
        ButtonDict.betok=true;
        chartacterTable.currentTableActive.Value=false;
        betokclicked=true;
        Betok.SetBool("betok",false);
        aa.SetBool("clicked",false);
            table.SetBool("clicked",false);
            roulette.SetActive(true);
    }

    public void mainlobby(){
        SceneManager.LoadScene(2);
    }
}
