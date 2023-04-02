using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class web : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(GetText());
       // StartCoroutine(Login("demo1","1234"));
    }

    IEnumerator GetText(){
       using(UnityWebRequest www=UnityWebRequest.Get("http://localhost/roulette/test.php")){
        yield return www.Send();

        if(www.isNetworkError || www.isHttpError){
            Debug.Log(www.error);
        }
        else{
            Debug.Log(www.downloadHandler.text);

            byte[] results=www.downloadHandler.data;
        }
       }
    }

    IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);


        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/login1.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
