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
    public GameObject g;

    public Text t;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(servercheck());
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
                    t.text="Invalid Credentials";
                    t.color=Color.red;
                }
                else{
                    Debug.Log(s); //Output 1
                    PlayerPrefs.SetInt("id",int.Parse(s.ToString()));
                    PlayerPrefs.SetString("username",username);
                     PlayerPrefs.SetString("password",password);
                    SceneManager.LoadScene(2);
                }
            }
        }
    }
    IEnumerator servercheck()
    {
        WWWForm form = new WWWForm();
        

        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/servercheck.php", form))
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
                    Debug.Log(s);
                   if(s=="0"){
                    g.SetActive(true);

                   }
                   else{
                    g.SetActive(false);
                   }
                }
            }
        }
    }

    public void close(){
        Application.Quit();
    }
}
