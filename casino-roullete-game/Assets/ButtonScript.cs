using System.Collections.Generic;
using Components;
using UnityEngine;
using UnityEngine.UI;
using ViewModel;

public class ButtonScript : MonoBehaviour
{
    public GameObject objectToInstantiate;
    public CharacterTable chartacterTable;
    private Button btn ;

    public Sprite[] obj;
    public GameObject ab;
    ChipSelectInput c;
    public Dictionary<string, string> myDictionary;
    
    void Start()
    {

        btn = GetComponent<Button>();
        btn.onClick.AddListener(InstantiateObject);
         c=ab.GetComponent<ChipSelectInput>();
         myDictionary = new Dictionary<string, string>();
    }

    void InstantiateObject()
    {
        GameObject gg=Instantiate(objectToInstantiate,btn.transform.position,Quaternion.identity);
         gg.GetComponent<SpriteRenderer>().sprite=obj[2];
        if(c.currentchipvalue==10){
            gg.GetComponent<SpriteRenderer>().sprite=obj[2];
        }
        else if(c.currentchipvalue==1){
            gg.GetComponent<SpriteRenderer>().sprite=obj[0];
        }
        else if(c.currentchipvalue==5){
            gg.GetComponent<SpriteRenderer>().sprite=obj[1];
        }
        else if(c.currentchipvalue==50){
            gg.GetComponent<SpriteRenderer>().sprite=obj[3];
        }
        else if(c.currentchipvalue==100){
            gg.GetComponent<SpriteRenderer>().sprite=obj[4];
        }
        else if(c.currentchipvalue==500){
            gg.GetComponent<SpriteRenderer>().sprite=obj[5];
        }
        else if(c.currentchipvalue==1000){
            gg.GetComponent<SpriteRenderer>().sprite=obj[6];
        }
        else if(c.currentchipvalue==5000){
            gg.GetComponent<SpriteRenderer>().sprite=obj[7];
        }
        
    }
}
