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
public class PreviousBet : MonoBehaviour
{

    public GameObject[] numbers;
    public GameObject[] row_col;
    public GameObject[] lastrow;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void previous_bet()
    {
        StartCoroutine(betprevious());
        ButtonDict.previousclicked=true;
    }
    IEnumerator betprevious(){
        if (ButtonDict.previousbet.Count != 0)
        {
            int first=0;

            for(int i=0;i<=37;i++){
                
                if(ButtonDict.previousbet.ContainsKey(i.ToString())){
                    numbers[i].GetComponent<ButtonTableInput>().Click2(ButtonDict.previousbet[i.ToString()]);
                }
                if(i==0 && first==0){
                    first=1;
                    i--;
                    yield return new WaitForSeconds(0.0f);
                }
            }

            if(ButtonDict.previousbet.ContainsKey("12_1")){
                    row_col[0].GetComponent<ButtonTableInput>().Click2(ButtonDict.previousbet["12_1"]);
                }

                 if(ButtonDict.previousbet.ContainsKey("12_2")){
                    row_col[1].GetComponent<ButtonTableInput>().Click2(ButtonDict.previousbet["12_2"]);
                }

                 if(ButtonDict.previousbet.ContainsKey("12_3")){
                    row_col[2].GetComponent<ButtonTableInput>().Click2(ButtonDict.previousbet["12_3"]);
                }

                 if(ButtonDict.previousbet.ContainsKey("c1")){
                    row_col[3].GetComponent<ButtonTableInput>().Click2(ButtonDict.previousbet["c1"]);
                }
                 if(ButtonDict.previousbet.ContainsKey("c2")){
                    row_col[4].GetComponent<ButtonTableInput>().Click2(ButtonDict.previousbet["c2"]);
                }
                 if(ButtonDict.previousbet.ContainsKey("c3")){
                    row_col[5].GetComponent<ButtonTableInput>().Click2(ButtonDict.previousbet["c3"]);
                }




                if(ButtonDict.previousbet.ContainsKey("E1_Eightteen_1")){
                    lastrow[0].GetComponent<ButtonTableInput>().Click2(ButtonDict.previousbet["E1_Eightteen_1"]);
                }
                if(ButtonDict.previousbet.ContainsKey("E1_Eightteen_2")){
                    lastrow[1].GetComponent<ButtonTableInput>().Click2(ButtonDict.previousbet["E1_Eightteen_2"]);
                }

                if(ButtonDict.previousbet.ContainsKey("E2_Black")){
                    lastrow[2].GetComponent<ButtonTableInput>().Click2(ButtonDict.previousbet["E2_Black"]);
                }
                if(ButtonDict.previousbet.ContainsKey("E2_Red")){
                    lastrow[3].GetComponent<ButtonTableInput>().Click2(ButtonDict.previousbet["E2_Red"]);
                }

                if(ButtonDict.previousbet.ContainsKey("E_Even")){
                    lastrow[4].GetComponent<ButtonTableInput>().Click2(ButtonDict.previousbet["E_Even"]);
                }
                if(ButtonDict.previousbet.ContainsKey("E_Odd")){
                    lastrow[5].GetComponent<ButtonTableInput>().Click2(ButtonDict.previousbet["E_Odd"]);
                }


            foreach (KeyValuePair<string, int> kvp in ButtonDict.previousbet)
            {
                Debug.Log(kvp.Key + " : " + kvp.Value);
            }
        }
    }
}