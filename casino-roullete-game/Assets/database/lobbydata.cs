using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lobbydata : MonoBehaviour
{
    public Text points;
    public Text username;
    public Text message;

    public Text password;

public Text accno;
    public Text amount;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Lobby());
    }
    public void transfer(){
if(password.text== PlayerPrefs.GetString("password") ){

    if(int.Parse(amount.text)<=int.Parse(points.text)){
        StartCoroutine(TransferPoints());
    }
    else{
    message.text="You dont have enough balance";
                message.color=Color.red;
}

}
else{
    message.text="Wrong password";
                message.color=Color.red;
}

        
    }
    IEnumerator TransferPoints(){
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetString("username"));
        form.AddField("clientid", accno.text);
        form.AddField("amount", amount.text);
        


        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/user_transferpoints.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                message.text="Server Error";
                message.color=Color.red;
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string s= www.downloadHandler.text.Trim();
                if(s=="Error"){
                   message.text="Client does not Exist";
                    message.color=Color.red;
                }
                else{
                    message.text="Request sent successfully";
                    message.color=Color.green;
                }
            }
            SceneManager.LoadScene(2);
        }

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
