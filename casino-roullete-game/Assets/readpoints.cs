using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class readpoints : MonoBehaviour
{
    public GameObject list;
    public int recieve=0;
    public int reject1=0;
    public int reject2=0;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(Login2());
    }


    IEnumerator Login2()
    {
        foreach (Transform child in gameObject.transform)
{
    Object.Destroy(child.gameObject);
}
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("id"));

    Debug.Log(PlayerPrefs.GetInt("id"));
        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/readaddcoins.php", form))
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
                  Debug.Log("Error");
                }
                else{
                    s=s.Substring(0, s.Length - 1);
                    Debug.Log(s); //Output 1
                    
                    string[] words = s.Split(',');
                        int i=70;
                        foreach (string word in words)
                        {
                           GameObject gg=Instantiate(list,new Vector3(0,70,0),Quaternion.identity,gameObject.transform);
                   gg.GetComponent<RectTransform>().anchoredPosition=new Vector3(0,i,0);
                   GameObject child=gg.transform.Find("Toggle").gameObject.transform.Find("Label").gameObject;
                   child.GetComponent<Text>().text=word;
                   i=i-70;
                        }
                    
                     
                }
            }
        }
    }

    public void updatestatus(){
       
            Toggle[] toggles = GameObject.FindGameObjectsWithTag("recievabletoggle")
    .Select(go => go.GetComponent<Toggle>())
    .Where(toggle => toggle != null)
    .ToArray();
 Debug.Log("aaya");
// Loop through the toggles and display the labels of the ones that are on
foreach (Toggle toggle in toggles)
{
    if (toggle.isOn)
    {
       StartCoroutine(Updatecoins(toggle.GetComponentInChildren<Text>().text));
    }
}
    }
    IEnumerator Updatecoins(string t){
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("id"));
        form.AddField("amount", int.Parse(t));
        form.AddField("status", "Approved");

            Debug.Log(PlayerPrefs.GetInt("id"));
        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/updateaddcoins.php", form))
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
                  Debug.Log("Error");
                }
                else{
                      Debug.Log(s);
                StartCoroutine(Login2());
                     
                }
            }
        }
    }


     public void updatestatus2(){
       
            Toggle[] toggles = GameObject.FindGameObjectsWithTag("recievabletoggle")
    .Select(go => go.GetComponent<Toggle>())
    .Where(toggle => toggle != null)
    .ToArray();
 Debug.Log("aaya");
// Loop through the toggles and display the labels of the ones that are on
foreach (Toggle toggle in toggles)
{
    if (toggle.isOn)
    {
       StartCoroutine(Updatecoins2(toggle.GetComponentInChildren<Text>().text));
    }
}
    }

 IEnumerator Updatecoins2(string t){
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("id"));
        form.AddField("amount", int.Parse(t));
        form.AddField("status", "Rejected by User");

            Debug.Log(PlayerPrefs.GetInt("id"));
        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/updateaddcoinsrejected.php", form))
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
                  Debug.Log("Error");
                }
                else{
                      Debug.Log(s);
                StartCoroutine(Login2());
                     
                }
            }
        }
    }



public void updatetoggle(){
       
            Toggle[] toggles = GameObject.FindGameObjectsWithTag("recievabletoggle")
    .Select(go => go.GetComponent<Toggle>())
    .Where(toggle => toggle != null)
    .ToArray();
 Debug.Log("aaya");
// Loop through the toggles and display the labels of the ones that are on
foreach (Toggle toggle in toggles)
{
    if (!toggle.isOn)
    {
       toggle.isOn=true;
    }
}
    }

    public void setrecieve(){
        recieve=1;
        reject1=0;

    }
    public void setreject(){
        recieve=0;
        reject1=1;
        
    }

    public void ok(){
        if(recieve==1){
            updatestatus();
        }
        else{
            updatestatus2();
        }
    }
}
