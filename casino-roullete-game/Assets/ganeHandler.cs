using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Commands;

public class ganeHandler : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nextscreen(){
        ButtonDict.loadgame=1;
        SceneManager.LoadScene(3);
    }
    public void logout(){
        SceneManager.LoadScene(1);
    }
    public void reload(){
        SceneManager.LoadScene(2);
    }
}
