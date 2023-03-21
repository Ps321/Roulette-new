using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if(username.text=="demo1" && password.text=="1234"){
          SceneManager.LoadScene(2);

        }
        else{
            Debug.Log("Incorrect");
        }
    }

    public void close(){
        Application.Quit();
    }
}
