using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Commands;

public class winoptions : MonoBehaviour
{
    // Start is called before the first frame update
    
public AudioSource a;
    // Update is called once per frame
    void Start()
    {
        ButtonDict.audio=a;
        if(ButtonDict.rulefetch==0){
            ButtonDict.rulefetch=1;
            StartCoroutine(rulefetch());
        }
    }

     IEnumerator rulefetch()
    {
        WWWForm form = new WWWForm();
       
        


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
}
