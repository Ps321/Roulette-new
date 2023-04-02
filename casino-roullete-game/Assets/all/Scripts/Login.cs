using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{

    [SerializeField] private InputField username;
    [SerializeField] private InputField password;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void login1(){
       StartCoroutine(Login2(username.text,password.text));
    }
 IEnumerator Login2(string username, string password)
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
                //Debug.Log(www.downloadHandler.text);
                string s= www.downloadHandler.text.Trim();
                if(s=="Error"){
                    
                }
                else{
                    Debug.Log(s); //Output 1
                    PlayerPrefs.SetInt("id",int.Parse(s.ToString()));
                    SceneManager.LoadScene(2);
                }
            }
        }
    }

    public void close(){
        Application.Quit();
    }
}
