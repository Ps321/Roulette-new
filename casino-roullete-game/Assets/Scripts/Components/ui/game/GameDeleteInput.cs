using System.Collections;
using System.Collections.Generic;
using Commands;
using UnityEngine;
using ViewModel;

namespace Components
{
    public class GameDeleteInput : MonoBehaviour
    {
        public CharacterTable characterTable;
        public GameCmdFactory gameCmdFactory;
        public MagnetDestroyerDisplay magnetDestroyerDisplay;

        public Animator aa;
    
    
     public Animator table;
     public GameObject roulette;

        public void OnClick()
        {
            if(ButtonDict.buttonenable){
            if(ButtonDict.betok==false){

            ButtonDict.first=0;
            int amount=0;
           foreach (KeyValuePair<string, int> kvp in ButtonDict.myDictionary)
{
    amount+=kvp.Value;
}
            characterTable.characterMoney.characterMoney.Value+=amount;
             ButtonDict.myDictionary.Clear();
            gameCmdFactory.ResetTableTurn(magnetDestroyerDisplay, characterTable).Execute();
            characterTable.currentTableActive.Value=false;
            aa.SetBool("clicked",false);
            table.SetBool("clicked",false);
            roulette.SetActive(true);
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Chip");

        // Loop through each object and look for a Button component
        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
        characterTable.currentTableActive.Value=true;
        }
    }
    }
}
}