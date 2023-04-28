using System.Collections.Generic;
using Components;
using UnityEngine;
using UnityEngine.UI;
using ViewModel;
using Commands;

public class ButtonScript : MonoBehaviour
{
    public GameObject objectToInstantiate;
    public GameObject parentobject;
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
        if(ButtonDict.first==0){
            return;
        }
      if(chartacterTable.characterMoney.characterMoney.Value>=ButtonDict.currentchipvalue){
      
      }

        if(chartacterTable.currentTableActive.Value){
        GameObject gg=Instantiate(objectToInstantiate,btn.transform.position,Quaternion.identity,parentobject.transform);
         gg.GetComponent<SpriteRenderer>().sprite=obj[2];
         gg.GetComponent<ChipGame>().chipname=btn.name;
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
        else{
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Chip");

        // Loop through each object and look for a Button component
        foreach (GameObject obj in objectsWithTag)
        {
            string name = obj.GetComponent<ChipGame>().chipname;
           
          //  name=name.Replace("Number_","");
          
            
            if (btn.name==name)
            {
               // ButtonDict.myDictionary.Remove(buttonData.buttonName);
              //  characterTable.characterMoney.AddCash2(buttonData.currentChipsOnTop);
                // Enable the button
                Destroy(obj);
            }
          
        }
        
    }
}
}
