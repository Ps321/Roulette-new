using UnityEngine;
using Components;
using ViewModel;
using TMPro;

namespace Commands
{
    public class ButtonTableInteractable : MonoBehaviour, IInteractableButton
    {
        public GameCmdFactory gameCmdFactory;
        public GameObject empty;

        public Sprite[] gg;
        public GameObject c;
        ChipSelectInput c1;
        private float previousz=-999;
        private float zoffset=-0.1f;

        private void Start() {
            c1=c.GetComponent<ChipSelectInput>();
        }

        public void InstantiateChip(CharacterTable characterTable, ButtonTable buttonData)
        {

            GameObject[] gg1=GameObject.FindGameObjectsWithTag("Chip");

            foreach(GameObject previouschip in gg1){
                if(previouschip.GetComponent<ChipGame>().chipname==buttonData.name){
                   if(previouschip.transform.childCount>0){ 
                    
                    previouschip.GetComponent<SpriteRenderer>().enabled=false;
                   previouschip.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled=false;
                 } //  Destroy(previouschip);
                }
            }

            GameObject chipInstance = Instantiate(characterTable.chipPrefab);
            chipInstance.GetComponent<ChipGame>().chipname=buttonData.name;
            if(previousz==-999){
                previousz=chipInstance.transform.position.z;
              
            }
            else{
                chipInstance.transform.position=new Vector3(chipInstance.transform.position.x,chipInstance.transform.position.y,chipInstance.transform.position.z+zoffset);
                zoffset=zoffset-1.0f;
               

            }
            buttonData.currentOffset.y=0;
            
           if(c1.currentchipvalue==10){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
          }
        else if(c1.currentchipvalue==1){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+c1.currentchipvalue).ToString();
        }
        else if(c1.currentchipvalue==5){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
         
        }
        else if(c1.currentchipvalue==50){
           chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
         
        }
        else if(c1.currentchipvalue==100){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
         
        }
        else if(c1.currentchipvalue==500){
             chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
         
        }
        else if(c1.currentchipvalue==1000){
             chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
         
        }
        else if(c1.currentchipvalue==5000){
             chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
         
        }
        
            chipInstance.SetActive(false);
           
            gameCmdFactory.ButtonTableTurn(characterTable, chipInstance, buttonData).Execute();
        }

        public void InstantiateChip1(CharacterTable characterTable, ButtonTable buttonData)
        {
            GameObject chipInstance = Instantiate(empty);
            chipInstance.GetComponent<ChipGame>().chipname=buttonData.name;
            chipInstance.SetActive(false);
            gameCmdFactory.ButtonTableTurn(characterTable, chipInstance, buttonData).Execute();
        }





        public void InstantiateChip2(CharacterTable characterTable, ButtonTable buttonData)
        {

            GameObject[] gg1=GameObject.FindGameObjectsWithTag("Chip");

            foreach(GameObject previouschip in gg1){
                if(previouschip.GetComponent<ChipGame>().chipname==buttonData.name){
                    previouschip.GetComponent<SpriteRenderer>().enabled=false;
                   previouschip.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled=false;
                  //  Destroy(previouschip);
                }
            }

            GameObject chipInstance = Instantiate(characterTable.chipPrefab);
            chipInstance.GetComponent<ChipGame>().chipname=buttonData.name;
            if(previousz==-999){
                previousz=chipInstance.transform.position.z;
              
            }
            else{
                chipInstance.transform.position=new Vector3(chipInstance.transform.position.x,chipInstance.transform.position.y,chipInstance.transform.position.z+zoffset);
                zoffset=zoffset-1.0f;
               

            }
            buttonData.currentOffset.y=0;
            
           if(c1.currentchipvalue==10){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
          }
        else if(c1.currentchipvalue==1){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+c1.currentchipvalue).ToString();
        }
        else if(c1.currentchipvalue==5){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
         
        }
        else if(c1.currentchipvalue==50){
           chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
         
        }
        else if(c1.currentchipvalue==100){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
         
        }
        else if(c1.currentchipvalue==500){
             chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
         
        }
        else if(c1.currentchipvalue==1000){
             chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
         
        }
        else if(c1.currentchipvalue==5000){
             chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
             chipInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text=(buttonData.currentChipsOnTop+1).ToString();
         
        }
        
            chipInstance.SetActive(false);
           
            gameCmdFactory.ButtonTableTurn(characterTable, chipInstance, buttonData).Execute1();
        }








    }
}
