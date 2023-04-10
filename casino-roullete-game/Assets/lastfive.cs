using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commands;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;

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
                for(int i =0;i<=4;i++){
                    if(a[i]=="37"){
                        a[i]="00";
                    }
                }
                t[0].text=a[4];
                t[0].color=chckcolor(int.Parse(a[4]));
                t[1].text=a[3];
                t[1].color=chckcolor(int.Parse(a[3]));
                t[2].text=a[2];
                t[2].color=chckcolor(int.Parse(a[2]));
                t[3].text=a[1];
                t[3].color=chckcolor(int.Parse(a[1]));
                t[4].text=a[0];
                t[4].color=chckcolor(int.Parse(a[0]));
                Debug.Log("Score has been fetched from database");
                StartCoroutine(storeround());
             }
        }

        

    }

    IEnumerator storeround(){
         WWWForm form = new WWWForm();
            
            
                form.AddField("paymentwin", ButtonDict.paymentWin);
                form.AddField("paymentlost", ButtonDict.paymentLost);
                 form.AddField("playerid", PlayerPrefs.GetInt("id"));
            
       
        
        


        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/storewinloss.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("update points");        
                
           }
        }
    }
    public Color chckcolor(int num){
        if(ButtonDict.red.Contains(num)){
                   return Color.red;
                }
                else if(ButtonDict.black.Contains(num)){
                    return Color.white;
                }else{
                    return Color.green;
                }
                
    }
}
