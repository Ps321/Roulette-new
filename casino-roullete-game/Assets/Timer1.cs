using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Components;
using Commands;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ViewModel;
using System;
using System.Linq;
using Managers;

public class Timer1 : MonoBehaviourPunCallbacks
{
    public CharacterTable chartacterTable;
    public int randomNumber { get; set; }
    public float timerValue = 60.0f;
    public Animator aa;
    public GameManager gg;
    public Animator Timer;
    public Animator Betok;

    public Button[] coins;

    public Animator table;
    public GameObject roulette;
    public Text textval;
    public bool enabletimer = false;
    bool betokclicked = false;
    public ButtonDict dd;
    private bool updatemoney = true;

    public GameObject a;

    int generated = 0;
    int lastfiveupdate = 0;

    public Text messages1;

    public GameObject previousBet;
    public GameObject BetOk;



    void Start()
    {
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RequestOwnership();





    }


    void Update()
    {




        if (enabled == true)
        {
            DateTime now = DateTime.Now;
            int seconds = now.Second;
            timerValue = 60 - seconds;



            if (timerValue == 60)
            {
                timerValue = 0;
                enabled = false;
            }
            /* if(timerValue==30){
                 Application.Quit();
             }*/
            textval.text = "0 : " + Mathf.Round(timerValue).ToString();

            if (timerValue <= 45 && lastfiveupdate == 0)
            {
                lastfiveupdate = 1;
                ButtonDict.lastfive = 1;
            }
                if (timerValue <= 0)
            {
                    if(ButtonDict.currentchipvalue==1){
                        coins[0].onClick.Invoke();
                    }
                    if(ButtonDict.currentchipvalue==5){
                        coins[1].onClick.Invoke();
                    }
                    if(ButtonDict.currentchipvalue==10){
                        coins[2].onClick.Invoke();
                    }
                    if(ButtonDict.currentchipvalue==50){
                        coins[3].onClick.Invoke();
                    }
                    if(ButtonDict.currentchipvalue==100){
                        coins[4].onClick.Invoke();
                    }
                    if(ButtonDict.currentchipvalue==500){
                        coins[5].onClick.Invoke();
                    }
                    if(ButtonDict.currentchipvalue==1000){
                        coins[6].onClick.Invoke();
                    }
                    if(ButtonDict.currentchipvalue==5000){
                        coins[7].onClick.Invoke();
                    }
            }
            if (timerValue <= 0)
            {
                generated = 0;
                lastfiveupdate = 0;
                ButtonDict.previousclicked = false;
                StartCoroutine(rulefetch());
                updatemoney = true;


                ButtonDict.buttonenable = true;
                ButtonDict.betok = false;
                StartCoroutine(cleardict());
                a.GetComponent<GamePlayInput>().OnClick();
                ButtonDict.first = 0;
                ButtonDict.first_1 = 0;
                ButtonDict.buttonoffset = 0f;
                chartacterTable.currentTableActive.Value = true;
                timerValue = 60;
                betokclicked = false;

            }
            if (ButtonDict.first == 1 && timerValue >= 10 && betokclicked == false)
            {

                Betok.SetBool("betok", true);

            }
            else
            {
                Betok.SetBool("betok", false);
            }
            if (timerValue <= 15 && timerValue >= 10)
            {
                Timer.SetBool("glow", true);
            }

            if (timerValue <= 5)
            {

               
                if (generated == 0)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        generated = 1;
                        Generation();
                    }

                }
            }

            if (timerValue <= 10)
            {
                if (generated == 0)
                {
                    if (ButtonDict.myDictionary.Count != 0)
                    {
                        if (ButtonDict.previousbet.Count != 0)
                        {
                            ButtonDict.previousbet.Clear();
                        }
                        ButtonDict.previousbet = new Dictionary<string, int>(ButtonDict.myDictionary);


                    }
                    else
                    {
                        ButtonDict.previousbet.Clear();
                    }


                    if (!PhotonNetwork.IsMasterClient)
                    {
                        generated = 1;
                        SendNumbers();
                    }

                }
                updatemoney = true;

                ButtonDict.buttonenable = false;
                ButtonDict.winnernumber = 1;

                GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");

                foreach (GameObject button in buttons)
                {
                    Animator animator = button.GetComponent<Animator>();

                    if (animator != null)
                    {
                        animator.SetBool("win", false);
                    }
                }
                Timer.SetBool("glow", false);

                chartacterTable.currentTableActive.Value = false;
                aa.SetBool("clicked", false);
                table.SetBool("clicked", false);
                roulette.SetActive(true);

            }
            if (enabled == false)
            {
                StartCoroutine(enableit());
            }
            messages();
        }


        if (ButtonDict.winloss == true)
        {
            //  StartCoroutine(winloss(ButtonDict.paymentWin,ButtonDict.paymentLost));
        }


        /*Update money*/




    }












    IEnumerator enableit()
    {
        yield return new WaitForSeconds(1);
        enabled = true;
    }
    void messages()
    {
        if (ButtonDict.first == 0)
        {
            BetOk.SetActive(true);
            previousBet.SetActive(false);
            messages1.text = "Please Bet to start Game. MinimumBet=1";
        }
        if (ButtonDict.first == 1)
        {
            if (ButtonDict.previousbet.Count > 0 && ButtonDict.previousclicked == false)
            {
                if (ButtonDict.myDictionary.Count > 0)
                {
                    previousBet.SetActive(false);
                    BetOk.SetActive(true);
                }
                else
                {
                    previousBet.SetActive(true);
                    BetOk.SetActive(false);
                }

            }
            else
            {
                previousBet.SetActive(false);
                BetOk.SetActive(true);
            }
            messages1.text = "Bet Now";
        }
        if (timerValue <= 10)
        {
            BetOk.SetActive(true);
            messages1.text = "Timer Up";
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
                string s = www.downloadHandler.text.Trim();
                if (s == "Error")
                {
                    Debug.Log("error");
                }
                else
                {
                    ButtonDict.rule = int.Parse(s);
                }
            }
        }
    }


    IEnumerator cleardict()
    {
        yield return new WaitForSeconds(5);

        ButtonDict.myDictionary.Clear();

    }
    public void NoMoreBet()
    {
        ButtonDict.betok = true;
        chartacterTable.currentTableActive.Value = false;
        betokclicked = true;
        Betok.SetBool("betok", false);
        aa.SetBool("clicked", false);
        table.SetBool("clicked", false);
        roulette.SetActive(true);
    }

    public void mainlobby()
    {
        ButtonDict.gameclose=1;
        ButtonDict.loadedfirst=0;
        SceneManager.LoadScene(2);
        
    }

    public void SendNumbers()
    {
        SendData(ButtonDict.myDictionary);
    }

    public void Generation()
    {
        
        int lowestValue = int.MaxValue;
        int lowestKey = -1;
        randomNumber = -1;


        

            Dictionary<string, int> dict1 = new Dictionary<string, int>();
            /***Lowesttt rowwwwwww****/////
            
            if (ButtonDict.myDictionary.ContainsKey("E1_Eightteen_1"))
                {
                   for(int i=1;i<=18;i++){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["E1_Eightteen_1"]*2;
                    }else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["E1_Eightteen_1"]*2);
                    }
                   }
                }

            if (ButtonDict.myDictionary.ContainsKey("E1_Eightteen_2"))
                {
                   for(int i=19;i<=36;i++){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["E1_Eightteen_2"]*2;
                    }
                    else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["E1_Eightteen_2"]*2);
                    }
                   }
                }


            if (ButtonDict.myDictionary.ContainsKey("E2_Black"))
                {
                   Debug.Log("Blackkkkkk");
                   for(int i=0;i<18;i++){
                    if(dict1.ContainsKey(ButtonDict.black[i].ToString())){
                        Debug.Log("Blackkkkkk Update");
                        dict1[ButtonDict.black[i].ToString()]+=ButtonDict.myDictionary["E2_Black"]*2;
                    }
                    else{
                         Debug.Log("Blackkkkkk Insert");
                        dict1.Add(ButtonDict.black[i].ToString(),ButtonDict.myDictionary["E2_Black"]*2);
                    }
                   }
                }


                if (ButtonDict.myDictionary.ContainsKey("E2_Red"))
                {
                   for(int i=0;i<18;i++){
                    if(dict1.ContainsKey(ButtonDict.red[i].ToString())){
                        dict1[ButtonDict.red[i].ToString()]+=ButtonDict.myDictionary["E2_Red"]*2;
                    }
                    else{
                        dict1.Add(ButtonDict.red[i].ToString(),ButtonDict.myDictionary["E2_Red"]*2);
                    }
                   }
                }


                if (ButtonDict.myDictionary.ContainsKey("E_Even"))
                {
                   for(int i=2;i<=36;i=i+2){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["E_Even"]*2;
                    }
                    else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["E_Even"]*2);
                    }
                   }
                }

                if (ButtonDict.myDictionary.ContainsKey("E_Odd"))
                {
                   for(int i=1;i<=35;i=i+2){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["E_Odd"]*2;
                    }
                    else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["E_Odd"]*2);
                    }
                   }
                }
               




            /***** 2nd lowest rowww***/
            if (ButtonDict.myDictionary.ContainsKey("12_1"))
                {

                     for(int i=1;i<=12;i++){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["12_1"]*3;
                    }else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["12_1"]*3);
                    }
                   }
                }

                if (ButtonDict.myDictionary.ContainsKey("12_2"))
                {

                     for(int i=13;i<=24;i++){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["12_2"]*3;
                    }else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["12_2"]*3);
                    }
                   }
                }

                 if (ButtonDict.myDictionary.ContainsKey("12_3"))
                {

                     for(int i=25;i<=36;i++){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["12_3"]*3;
                    }else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["12_3"]*3);
                    }
                   }
                }



                /**** Coloums*****/

                if (ButtonDict.myDictionary.ContainsKey("c1"))
                {

                     for(int i=0;i<12;i++){
                    if(dict1.ContainsKey(ButtonDict.c1[i].ToString())){
                        dict1[ButtonDict.c1[i].ToString()]+=ButtonDict.myDictionary["c1"]*3;
                    }else{
                        dict1.Add(ButtonDict.c1[i].ToString(),ButtonDict.myDictionary["c1"]*3);
                    }
                   }
                }

                if (ButtonDict.myDictionary.ContainsKey("c2"))
                {

                     for(int i=0;i<12;i++){
                    if(dict1.ContainsKey(ButtonDict.c2[i].ToString())){
                        dict1[ButtonDict.c2[i].ToString()]+=ButtonDict.myDictionary["c2"]*3;
                    }else{
                        dict1.Add(ButtonDict.c2[i].ToString(),ButtonDict.myDictionary["c2"]*3);
                    }
                   }
                }

                 if (ButtonDict.myDictionary.ContainsKey("c3"))
                {

                     for(int i=0;i<12;i++){
                    if(dict1.ContainsKey(ButtonDict.c3[i].ToString())){
                        dict1[ButtonDict.c3[i].ToString()]+=ButtonDict.myDictionary["c3"]*3;
                    }else{
                        dict1.Add(ButtonDict.c3[i].ToString(),ButtonDict.myDictionary["c3"]*3);
                    }
                   }
                }

                /*****Numbers******/
                     for(int i=0;i<=37;i++){
                         if (ButtonDict.myDictionary.ContainsKey(i.ToString()))
                    {
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary[i.ToString()]*36;
                    }else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary[i.ToString()]*36);
                    }
                   }
                     }


            /***Middle Rowss****/

            for(int i=4;i<=36;i++){
                string s="mrow_"+i;
                    if (ButtonDict.myDictionary.ContainsKey(s))
                    {
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary[s]*18;
                    }else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary[s]*18);
                    }
                    if(dict1.ContainsKey((i-3).ToString())){
                        dict1[(i-3).ToString()]+=ButtonDict.myDictionary[s]*18;
                    }else{
                        dict1.Add((i-3).ToString(),ButtonDict.myDictionary[s]*18);
                    }

                   }
              }





            /***Middle Coloums****/

            for(int i=0;i< ButtonDict.col.Length;i++){
                string s="mcolumn_"+ButtonDict.col[i];
                    if (ButtonDict.myDictionary.ContainsKey(s))
                    {
                    if(dict1.ContainsKey(ButtonDict.col[i].ToString())){
                        dict1[ButtonDict.col[i].ToString()]+=ButtonDict.myDictionary[s]*18;
                    }else{
                        dict1.Add(ButtonDict.col[i].ToString(),ButtonDict.myDictionary[s]*18);
                    }
                    if(dict1.ContainsKey((ButtonDict.col[i]-1).ToString())){
                        dict1[(ButtonDict.col[i]-1).ToString()]+=ButtonDict.myDictionary[s]*18;
                    }else{
                        dict1.Add((ButtonDict.col[i]-1).ToString(),ButtonDict.myDictionary[s]*18);
                    }

                   }
              }


              /***Coloumn with 3 values Coloums****/

            for(int i=0;i< ButtonDict.col3.Length;i++){
                string s="mcolumn_"+ButtonDict.col3[i];
                    if (ButtonDict.myDictionary.ContainsKey(s))
                    {
                    if(dict1.ContainsKey(ButtonDict.col3[i].ToString())){
                        dict1[ButtonDict.col3[i].ToString()]+=ButtonDict.myDictionary[s]*12;
                    }else{
                        dict1.Add(ButtonDict.col3[i].ToString(),ButtonDict.myDictionary[s]*12);
                    }
                    if(dict1.ContainsKey((ButtonDict.col3[i]+1).ToString())){
                        dict1[(ButtonDict.col3[i]+1).ToString()]+=ButtonDict.myDictionary[s]*12;
                    }else{
                        dict1.Add((ButtonDict.col3[i]+1).ToString(),ButtonDict.myDictionary[s]*12);
                    }

                    if(dict1.ContainsKey((ButtonDict.col3[i]+2).ToString())){
                        dict1[(ButtonDict.col3[i]+2).ToString()]+=ButtonDict.myDictionary[s]*12;
                    }else{
                        dict1.Add((ButtonDict.col3[i]+2).ToString(),ButtonDict.myDictionary[s]*12);
                    }

                   }
              }

            /***Middle Center Rows and Coloums****/

            for(int i=0;i< ButtonDict.center.Length;i++){
                string s="mcorner_"+ButtonDict.center[i];
                    if (ButtonDict.myDictionary.ContainsKey(s))
                    {
                    if(dict1.ContainsKey(ButtonDict.center[i].ToString())){
                        dict1[ButtonDict.center[i].ToString()]+=ButtonDict.myDictionary[s]*9;
                    }else{
                        dict1.Add(ButtonDict.center[i].ToString(),ButtonDict.myDictionary[s]*9);
                    }
                    if(dict1.ContainsKey((ButtonDict.center[i]-1).ToString())){
                        dict1[(ButtonDict.center[i]-1).ToString()]+=ButtonDict.myDictionary[s]*9;
                    }else{
                        dict1.Add((ButtonDict.center[i]-1).ToString(),ButtonDict.myDictionary[s]*9);
                    }

                    if(dict1.ContainsKey((ButtonDict.center[i]-3).ToString())){
                        dict1[(ButtonDict.center[i]-3).ToString()]+=ButtonDict.myDictionary[s]*9;
                    }else{
                        dict1.Add((ButtonDict.center[i]-3).ToString(),ButtonDict.myDictionary[s]*9);
                    }

                    if(dict1.ContainsKey((ButtonDict.center[i]-4).ToString())){
                        dict1[(ButtonDict.center[i]-4).ToString()]+=ButtonDict.myDictionary[s]*9;
                    }else{
                        dict1.Add((ButtonDict.center[i]-4).ToString(),ButtonDict.myDictionary[s]*9);
                    }

                   }
              }







        if(ButtonDict.rule==1){
             Debug.Log("------------- Betted Numbers");

         if(dict1.Count>0){
        string lowestValueKey = dict1.OrderBy(kv => kv.Value).First().Key;
        randomNumber=int.Parse(lowestValueKey);

            }else{
                randomNumber= UnityEngine.Random.Range(0, 38);
            }
        }else{
            Debug.Log("-------------Non Betted Numbers");
            int[] myarr = new int[38];
                int index = 0;
                int c = 0;

                for (int i = 0; i <= 37; i++)
                {
                    myarr[i] = -1;
                }

                for (int i = 0; i <= 37; i++)
                {
                    if (!dict1.ContainsKey(i.ToString()))
                    {
                        myarr[index] = i;
                        index++;
                        c++;
                    }
                }

                 int count = 0;
                for (int i = 0; i < myarr.Length; i++)
                {
                    if (myarr[i] != -1)
                    {
                        count++;
                    }
                }

               
                if (count > 0)
                {
                    
                    int aaaa = UnityEngine.Random.Range(0, count);
                    randomNumber = myarr[aaaa];
                }else{
                     if(dict1.Count>0){
        string lowestValueKey = dict1.OrderBy(kv => kv.Value).First().Key;
        randomNumber=int.Parse(lowestValueKey);

            }else{
                randomNumber= UnityEngine.Random.Range(0, 38);
            }
                }


        }

            
            

        
        
        ButtonDict.genratednumber = randomNumber;
      
      
      
      
      
      
        UpdateValue();
    }

    void UpdateValue()
    {

        photonView.RPC("UpdateValueRPC", RpcTarget.AllBuffered, randomNumber);
    }
    public void SendData(Dictionary<string, int> newData)
    {

        photonView.RPC("UpdateMasterDictionary", RpcTarget.MasterClient, newData);
    }

    [PunRPC]
    void UpdateMasterDictionary(Dictionary<string, int> newData)
    {

        foreach (KeyValuePair<string, int> kvp in newData)
        {
            // Check if key already exists, and update or add as necessary
            if (ButtonDict.myDictionary.ContainsKey(kvp.Key))
            {
                ButtonDict.myDictionary[kvp.Key] += kvp.Value;
            }
            else
            {
                ButtonDict.myDictionary.Add(kvp.Key, kvp.Value);
            }
        }

        foreach (KeyValuePair<string, int> kvp in ButtonDict.myDictionary)
        {
            Debug.Log("Key: " + kvp.Key + ", Value: " + kvp.Value);
        }
        Debug.Log("Lengthhhhhhhhhh" + ButtonDict.myDictionary.Count);

    }


    [PunRPC]
    void UpdateValueRPC(int newValue)
    {
        ButtonDict.genratednumber = newValue;
    }


}
