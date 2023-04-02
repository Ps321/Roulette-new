using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class lobbydata : MonoBehaviour
{
    public Text points;
    public Text username;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Lobby());
    }

    // Update is called once per frame
   IEnumerator Lobby()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("id"));
        


        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/lobby.php", form))
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
                   string[] a= s.Split(',');
                   username.text=a[0];
                   points.text=a[1];
                }
            }
        }
    }

}
