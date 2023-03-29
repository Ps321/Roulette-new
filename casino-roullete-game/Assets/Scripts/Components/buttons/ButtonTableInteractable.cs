using UnityEngine;
using Components;
using ViewModel;

namespace Commands
{
    public class ButtonTableInteractable : MonoBehaviour, IInteractableButton
    {
        public GameCmdFactory gameCmdFactory;
        public GameObject empty;

        public Sprite[] gg;
        public GameObject c;
        ChipSelectInput c1;

        private void Start() {
            c1=c.GetComponent<ChipSelectInput>();
        }

        public void InstantiateChip(CharacterTable characterTable, ButtonTable buttonData)
        {
            GameObject chipInstance = Instantiate(characterTable.chipPrefab);
            chipInstance.GetComponent<ChipGame>().chipname=buttonData.name;
            buttonData.currentOffset.y=ButtonDict.buttonoffset;
           if(c1.currentchipvalue==10){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[2];
        }
        else if(c1.currentchipvalue==1){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[0];
        }
        else if(c1.currentchipvalue==5){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[1];
        }
        else if(c1.currentchipvalue==50){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[3];
        }
        else if(c1.currentchipvalue==100){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[4];
        }
        else if(c1.currentchipvalue==500){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[5];
        }
        else if(c1.currentchipvalue==1000){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[6];
        }
        else if(c1.currentchipvalue==5000){
            chipInstance.GetComponent<SpriteRenderer>().sprite=gg[7];
        }
        
            chipInstance.SetActive(false);
           
            gameCmdFactory.ButtonTableTurn(characterTable, chipInstance, buttonData).Execute();
        }

        public void InstantiateChip1(CharacterTable characterTable, ButtonTable buttonData)
        {
            GameObject chipInstance = Instantiate(empty);
            chipInstance.SetActive(false);
            gameCmdFactory.ButtonTableTurn(characterTable, chipInstance, buttonData).Execute();
        }
    }
}
