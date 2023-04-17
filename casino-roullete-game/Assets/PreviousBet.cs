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
                    yield return new WaitForSeconds(1.5f);
                }
            }


            foreach (KeyValuePair<string, int> kvp in ButtonDict.previousbet)
            {
                Debug.Log(kvp.Key + " : " + kvp.Value);
            }
        }
    }
}
