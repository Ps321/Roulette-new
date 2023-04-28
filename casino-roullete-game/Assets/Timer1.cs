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
        Debug.Log("Lengthhhhhhhhhh1111" + ButtonDict.myDictionary.Count);
        int lowestValue = int.MaxValue;
        int lowestKey = -1;
        randomNumber = -1;
        Debug.Log("Rule value" + ButtonDict.rule);
        if (ButtonDict.rule == 1)
        {
            Dictionary<string, int> dict1 = new Dictionary<string, int>();
            if (ButtonDict.myDictionary.ContainsKey("E1_Eightteen_1"))
            {
                dict1.Add("E1_Eightteen_1", ButtonDict.myDictionary["E1_Eightteen_1"]);
            }
            if (ButtonDict.myDictionary.ContainsKey("E1_Eightteen_2"))
            {
                dict1.Add("E1_Eightteen_2", ButtonDict.myDictionary["E1_Eightteen_2"]);
            }
            if (ButtonDict.myDictionary.ContainsKey("E2_Black"))
            {
                dict1.Add("E2_Black", ButtonDict.myDictionary["E2_Black"]);
            }
            if (ButtonDict.myDictionary.ContainsKey("E2_Red"))
            {
                dict1.Add("E2_Red", ButtonDict.myDictionary["E2_Red"]);
            }
            if (ButtonDict.myDictionary.ContainsKey("E_Even"))
            {
                dict1.Add("E_Even", ButtonDict.myDictionary["E_Even"]);
            }
            if (ButtonDict.myDictionary.ContainsKey("E_Odd"))
            {
                dict1.Add("E_Odd", ButtonDict.myDictionary["E_Odd"]);
            }
            string keyWithLowestValue1 = dict1.OrderBy(kvp => kvp.Value).FirstOrDefault().Key;



            Dictionary<string, int> dict = new Dictionary<string, int>();


            if (ButtonDict.myDictionary.ContainsKey("12_1"))
            {
                dict.Add("12_1", ButtonDict.myDictionary["12_1"]);
            }
            if (ButtonDict.myDictionary.ContainsKey("12_2"))
            {
                dict.Add("12_2", ButtonDict.myDictionary["12_2"]);
            }
            if (ButtonDict.myDictionary.ContainsKey("12_3"))
            {
                dict.Add("12_3", ButtonDict.myDictionary["12_3"]);
            }
            if (ButtonDict.myDictionary.ContainsKey("c1"))
            {
                dict.Add("c1", ButtonDict.myDictionary["c1"]);
            }
            if (ButtonDict.myDictionary.ContainsKey("c2"))
            {
                dict.Add("c2", ButtonDict.myDictionary["c2"]);
            }
            if (ButtonDict.myDictionary.ContainsKey("c3"))
            {
                dict.Add("c3", ButtonDict.myDictionary["c3"]);
            }

            string keyWithLowestValue = dict.OrderBy(kvp => kvp.Value).FirstOrDefault().Key;

            string proceed = "";

            if (keyWithLowestValue != "" && keyWithLowestValue1 != "" && keyWithLowestValue != null && keyWithLowestValue1 != null)
            {
                if (ButtonDict.myDictionary[keyWithLowestValue] * 2 < ButtonDict.myDictionary[keyWithLowestValue1])
                {
                    proceed = keyWithLowestValue;
                }
                else
                {
                    proceed = keyWithLowestValue1;
                }
            }
            else if (keyWithLowestValue != "" && keyWithLowestValue != null)
            {
                proceed = keyWithLowestValue;
            }
            else
            {
                proceed = keyWithLowestValue1;
            }



            if (proceed == "12_1")
            {
                randomNumber = UnityEngine.Random.Range(1, 13);
            }
            if (proceed == "12_2")
            {
                randomNumber = UnityEngine.Random.Range(13, 25);
            }
            if (proceed == "12_3")
            {
                randomNumber = UnityEngine.Random.Range(25, 37);
            }
            if (proceed == "c1")
            {
                int val = UnityEngine.Random.Range(1, 13);
                randomNumber = ButtonDict.c1[val];
            }
            if (proceed == "c2")
            {
                int val = UnityEngine.Random.Range(0, 13);
                randomNumber = ButtonDict.c2[val];
            }
            if (proceed == "c3")
            {
                int val = UnityEngine.Random.Range(0, 13);
                randomNumber = ButtonDict.c3[val];
            }

            if (proceed == "E1_Eightteen_1")
            {
                randomNumber = UnityEngine.Random.Range(1, 19);
            }
            if (proceed == "E1_Eightteen_2")
            {
                randomNumber = UnityEngine.Random.Range(18, 37);
            }
            if (proceed == "E2_Black")
            {
                int val = UnityEngine.Random.Range(0, 18);
                randomNumber = ButtonDict.black[val];
            }
            if (proceed == "E2_Red")
            {
                int val = UnityEngine.Random.Range(0, 18);
                randomNumber = ButtonDict.red[val];
            }
            if (proceed == "E_Even")
            {
                randomNumber = UnityEngine.Random.Range(0, 19) * 2;

            }
            if (proceed == "E_Odd")
            {
                randomNumber = (UnityEngine.Random.Range(0, 19) * 2) + 1;

            }




            if (randomNumber == -1)
            {

                for (int i = 0; i <= 37; i++)
                {
                    string s = i.ToString();
                    if (ButtonDict.myDictionary.ContainsKey(s))
                    {
                        if (ButtonDict.myDictionary[s] < lowestValue)
                        {
                            lowestValue = ButtonDict.myDictionary[i.ToString()];
                            randomNumber = i;
                        }
                    }
                }


            }
        }
        else
        {

            int[] myarr = new int[38];
            int index = 0;
            int c = 0;

            for (int i = 0; i <= 37; i++)
            {
                myarr[i] = -1;
            }

            for (int i = 0; i <= 37; i++)
            {
                if (!ButtonDict.myDictionary.ContainsKey(i.ToString()))
                {
                    myarr[index] = i;
                    index++;
                    c++;
                }
            }

            List<int> list = new List<int>(myarr);




            if (ButtonDict.myDictionary.ContainsKey("12_1"))
            {

                for (int i = 1; i <= 12; i++)
                {
                    if (Array.IndexOf(myarr, i) != -1)
                    {
                        list.RemoveAt(Array.IndexOf(myarr, i));
                        myarr = list.ToArray();
                        i = i - 1;
                    }
                }

            }

            if (ButtonDict.myDictionary.ContainsKey("12_2"))
            {

                for (int i = 13; i <= 24; i++)
                {
                    if (Array.IndexOf(myarr, i) != -1)
                    {
                        list.RemoveAt(Array.IndexOf(myarr, i));
                        myarr = list.ToArray();
                        i = i - 1;
                    }
                }

            }

            if (ButtonDict.myDictionary.ContainsKey("12_3"))
            {

                for (int i = 25; i <= 36; i++)
                {
                    if (Array.IndexOf(myarr, i) != -1)
                    {
                        list.RemoveAt(Array.IndexOf(myarr, i));
                        myarr = list.ToArray();
                        i = i - 1;
                    }
                }

            }
            if (ButtonDict.myDictionary.ContainsKey("c1"))
            {

                for (int i = 0; i < 12; i++)
                {
                    if (Array.IndexOf(myarr, ButtonDict.c1[i]) != -1)
                    {
                        list.RemoveAt(Array.IndexOf(myarr, ButtonDict.c1[i]));
                        myarr = list.ToArray();
                        i = i - 1;
                    }
                }

            }

            if (ButtonDict.myDictionary.ContainsKey("c2"))
            {

                for (int i = 0; i < 12; i++)
                {
                    if (Array.IndexOf(myarr, ButtonDict.c2[i]) != -1)
                    {
                        list.RemoveAt(Array.IndexOf(myarr, ButtonDict.c2[i]));
                        myarr = list.ToArray();
                        i = i - 1;
                    }
                }

            }
            if (ButtonDict.myDictionary.ContainsKey("c3"))
            {

                for (int i = 0; i < 12; i++)
                {
                    if (Array.IndexOf(myarr, ButtonDict.c3[i]) != -1)
                    {
                        list.RemoveAt(Array.IndexOf(myarr, ButtonDict.c3[i]));
                        myarr = list.ToArray();
                        i = i - 1;
                    }
                }

            }
            if (ButtonDict.myDictionary.ContainsKey("E1_Eightteen_1"))
            {

                for (int i = 1; i <= 18; i++)
                {
                    if (Array.IndexOf(myarr, i) != -1)
                    {
                        list.RemoveAt(Array.IndexOf(myarr, i));
                        myarr = list.ToArray();
                        i = i - 1;
                    }
                }

            }
            if (ButtonDict.myDictionary.ContainsKey("E1_Eightteen_2"))
            {

                for (int i = 19; i <= 36; i++)
                {
                    if (Array.IndexOf(myarr, i) != -1)
                    {
                        list.RemoveAt(Array.IndexOf(myarr, i));
                        myarr = list.ToArray();
                        i = i - 1;
                    }
                }

            }



            if (ButtonDict.myDictionary.ContainsKey("E2_Black"))
            {

                for (int i = 0; i < 18; i++)
                {
                    if (Array.IndexOf(myarr, ButtonDict.black[i]) != -1)
                    {
                        list.RemoveAt(Array.IndexOf(myarr, ButtonDict.black[i]));
                        myarr = list.ToArray();
                        i = i - 1;
                    }
                }

            }

            if (ButtonDict.myDictionary.ContainsKey("E2_Red"))
            {

                for (int i = 0; i < 18; i++)
                {
                    if (Array.IndexOf(myarr, ButtonDict.red[i]) != -1)
                    {
                        list.RemoveAt(Array.IndexOf(myarr, ButtonDict.red[i]));
                        myarr = list.ToArray();
                        i = i - 1;
                    }
                }

            }


            if (ButtonDict.myDictionary.ContainsKey("E_Even"))
            {

                for (int i = 2; i <= 36; i = i + 2)
                {
                    if (Array.IndexOf(myarr, i) != -1)
                    {
                        list.RemoveAt(Array.IndexOf(myarr, i));
                        myarr = list.ToArray();
                        i = i - 2;
                    }
                }

            }

            if (ButtonDict.myDictionary.ContainsKey("E_Odd"))
            {

                for (int i = 1; i <= 37; i = i + 2)
                {
                    if (Array.IndexOf(myarr, i) != -1)
                    {
                        list.RemoveAt(Array.IndexOf(myarr, i));
                        myarr = list.ToArray();
                        i = i - 2;
                    }
                }

            }




            Debug.Log("Array: " + string.Join(", ", myarr));
            Debug.Log("List: " + string.Join(", ", list));

            myarr = list.ToArray();
            int count = 0;
            for (int i = 0; i < myarr.Length; i++)
            {
                if (myarr[i] != -1)
                {
                    count++;
                }
            }

            Debug.Log(" not Randomlyyy generatedd" + myarr.Length);
            if (count > 0)
            {

                int aaaa = UnityEngine.Random.Range(0, count);
                randomNumber = myarr[aaaa];
            }
            else
            {


                Debug.Log("bET IS PLACED ON EVERY NUMBER");


                Dictionary<string, int> dict1 = new Dictionary<string, int>();
                if (ButtonDict.myDictionary.ContainsKey("E1_Eightteen_1"))
                {
                    dict1.Add("E1_Eightteen_1", ButtonDict.myDictionary["E1_Eightteen_1"]);
                }
                if (ButtonDict.myDictionary.ContainsKey("E1_Eightteen_2"))
                {
                    dict1.Add("E1_Eightteen_2", ButtonDict.myDictionary["E1_Eightteen_2"]);
                }
                if (ButtonDict.myDictionary.ContainsKey("E2_Black"))
                {
                    dict1.Add("E2_Black", ButtonDict.myDictionary["E2_Black"]);
                }
                if (ButtonDict.myDictionary.ContainsKey("E2_Red"))
                {
                    dict1.Add("E2_Red", ButtonDict.myDictionary["E2_Red"]);
                }
                if (ButtonDict.myDictionary.ContainsKey("E_Even"))
                {
                    dict1.Add("E_Even", ButtonDict.myDictionary["E_Even"]);
                }
                if (ButtonDict.myDictionary.ContainsKey("E_Odd"))
                {
                    dict1.Add("E_Odd", ButtonDict.myDictionary["E_Odd"]);
                }
                string keyWithLowestValue1 = dict1.OrderBy(kvp => kvp.Value).FirstOrDefault().Key;



                Dictionary<string, int> dict = new Dictionary<string, int>();


                if (ButtonDict.myDictionary.ContainsKey("12_1"))
                {
                    dict.Add("12_1", ButtonDict.myDictionary["12_1"]);
                }
                if (ButtonDict.myDictionary.ContainsKey("12_2"))
                {
                    dict.Add("12_2", ButtonDict.myDictionary["12_2"]);
                }
                if (ButtonDict.myDictionary.ContainsKey("12_3"))
                {
                    dict.Add("12_3", ButtonDict.myDictionary["12_3"]);
                }
                if (ButtonDict.myDictionary.ContainsKey("c1"))
                {
                    dict.Add("c1", ButtonDict.myDictionary["c1"]);
                }
                if (ButtonDict.myDictionary.ContainsKey("c2"))
                {
                    dict.Add("c2", ButtonDict.myDictionary["c2"]);
                }
                if (ButtonDict.myDictionary.ContainsKey("c3"))
                {
                    dict.Add("c3", ButtonDict.myDictionary["c3"]);
                }

                string keyWithLowestValue = dict.OrderBy(kvp => kvp.Value).FirstOrDefault().Key;

                string proceed = "";

                if (keyWithLowestValue != "" && keyWithLowestValue1 != "" && keyWithLowestValue != null && keyWithLowestValue1 != null)
                {
                    if (ButtonDict.myDictionary[keyWithLowestValue] * 2 < ButtonDict.myDictionary[keyWithLowestValue1])
                    {
                        proceed = keyWithLowestValue;
                    }
                    else
                    {
                        proceed = keyWithLowestValue1;
                    }
                }
                else if (keyWithLowestValue != "" && keyWithLowestValue != null)
                {
                    proceed = keyWithLowestValue;
                }
                else
                {
                    proceed = keyWithLowestValue1;
                }


                Debug.Log(proceed + "huaaaa");

                if (proceed == "12_1")
                {
                    randomNumber = UnityEngine.Random.Range(1, 13);
                }
                if (proceed == "12_2")
                {
                    randomNumber = UnityEngine.Random.Range(13, 25);
                }
                if (proceed == "12_3")
                {
                    randomNumber = UnityEngine.Random.Range(25, 37);
                }
                if (proceed == "c1")
                {
                    int val = UnityEngine.Random.Range(1, 13);
                    randomNumber = ButtonDict.c1[val];
                }
                if (proceed == "c2")
                {
                    int val = UnityEngine.Random.Range(0, 13);
                    randomNumber = ButtonDict.c2[val];
                }
                if (proceed == "c3")
                {
                    int val = UnityEngine.Random.Range(0, 13);
                    randomNumber = ButtonDict.c3[val];
                }

                if (proceed == "E1_Eightteen_1")
                {
                    randomNumber = UnityEngine.Random.Range(1, 19);
                }
                if (proceed == "E1_Eightteen_2")
                {
                    randomNumber = UnityEngine.Random.Range(18, 37);
                }
                if (proceed == "E2_Black")
                {
                    int val = UnityEngine.Random.Range(0, 18);
                    randomNumber = ButtonDict.black[val];
                }
                if (proceed == "E2_Red")
                {
                    int val = UnityEngine.Random.Range(0, 18);
                    randomNumber = ButtonDict.red[val];
                }
                if (proceed == "E_Even")
                {
                    randomNumber = UnityEngine.Random.Range(0, 19) * 2;

                }
                if (proceed == "E_Odd")
                {
                    randomNumber = (UnityEngine.Random.Range(0, 19) * 2) + 1;

                }




                if (randomNumber == -1)
                {

                    for (int i = 0; i <= 37; i++)
                    {
                        string s = i.ToString();
                        if (ButtonDict.myDictionary.ContainsKey(s))
                        {
                            if (ButtonDict.myDictionary[s] < lowestValue)
                            {
                                lowestValue = ButtonDict.myDictionary[i.ToString()];
                                randomNumber = i;
                            }
                        }
                    }


                }
                else
                {
                    int temprandomnumber = int.MaxValue;
                    for (int i = 0; i <= 37; i++)
                    {
                        string s = i.ToString();
                        if (ButtonDict.myDictionary.ContainsKey(s))
                        {
                            if (ButtonDict.myDictionary[s] < lowestValue)
                            {
                                lowestValue = ButtonDict.myDictionary[i.ToString()];
                                temprandomnumber = i;
                            }
                        }
                    }

                    if (ButtonDict.myDictionary[proceed] * 3 >= (ButtonDict.myDictionary[temprandomnumber.ToString()] * 35))
                    {
                        randomNumber = temprandomnumber;
                    }
                    else
                    {
                        Dictionary<string, int> dict123 = new Dictionary<string, int>();
                        int done=0;
                        if (proceed == "12_1")
                        {
                            done=1;
                            for (int i = 1; i <= 12; i++)
                            {
                                dict123.Add(i.ToString(), i);
                            }
                        }
                        if (proceed == "12_2")
                        {
                            done=1;
                            for (int i = 13; i <= 24; i++)
                            {
                                dict123.Add(i.ToString(), i);
                            }
                        }
                        if (proceed == "12_3")
                        {
                            done=1;
                            for (int i = 25; i <= 36; i++)
                            {
                                dict123.Add(i.ToString(), i);
                            }
                        }

                        if (ButtonDict.myDictionary.ContainsKey("c1"))
                        {
                            done=1;
                            for (int i = 0; i < ButtonDict.c1.Length; i++)
                            {
                                if (dict123.ContainsKey(ButtonDict.c1[i].ToString()))
                                {
                                    dict123.Remove(ButtonDict.c1[i].ToString());
                                }
                            }
                        }
                        if (ButtonDict.myDictionary.ContainsKey("c2"))
                        {
                            done=1;
                            for (int i = 0; i < ButtonDict.c2.Length; i++)
                            {
                                if (dict123.ContainsKey(ButtonDict.c2[i].ToString()))
                                {
                                    dict123.Remove(ButtonDict.c2[i].ToString());
                                }
                            }
                        }
                        if (ButtonDict.myDictionary.ContainsKey("c3"))
                        {
                            done=1;
                            for (int i = 0; i < ButtonDict.c3.Length; i++)
                            {
                                if (dict123.ContainsKey(ButtonDict.c3[i].ToString()))
                                {
                                    dict123.Remove(ButtonDict.c3[i].ToString());
                                }
                            }
                        }
                        if(done==1){
                             randomNumber=dict123.ElementAt(UnityEngine.Random.Range(0, dict123.Count)).Value;
                        }
                       

                    }

                }
            }

        }

        if (randomNumber == -1)
        {
            Debug.Log("Randomlyyy generatedd");
            randomNumber = UnityEngine.Random.Range(0, 37);
        }
        randomNumber=2;
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
