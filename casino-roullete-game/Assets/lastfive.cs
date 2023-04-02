using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commands;
using UnityEngine.Networking;
using UnityEngine.UI;

public class lastfive : MonoBehaviour
{
    public Text[] t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ButtonDict.lastfive==1){
            ButtonDict.lastfive=0;
            StartCoroutine(lastfive1());

        }
    }

    IEnumerator lastfive1(){
         WWWForm form = new WWWForm();
       
       
        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/readlastfive.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string s= www.downloadHandler.text.Trim();   
                string[] a=s.Split(',');
                t[0].text=a[0];
                t[1].text=a[1];
                t[2].text=a[2];
                t[3].text=a[3];
                t[4].text=a[4];
                Debug.Log("Score has been fetched from database");
                
             }
        }

    }
}
