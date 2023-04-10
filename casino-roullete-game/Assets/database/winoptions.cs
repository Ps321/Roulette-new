using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Commands;

public class winoptions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ButtonDict.rulefetch==0){
            ButtonDict.rulefetch=1;
            StartCoroutine(Lobby());
        }
    }

     IEnumerator Lobby()
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
}
