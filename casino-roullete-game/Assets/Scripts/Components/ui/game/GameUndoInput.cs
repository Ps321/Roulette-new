using System.Collections;
using System.Collections.Generic;
using Commands;
using Components;
using UnityEngine;
using UnityEngine.UI;
using ViewModel;

namespace Components
{
    public class GameUndoInput : MonoBehaviour
    {

        public Sprite[] sprites;
        public CharacterTable characterTable;
        public GameCmdFactory gameCmdFactory;
        public void OnClick() 
        {
            if(ButtonDict.betok == false){
            characterTable.currentTableActive.Value=!characterTable.currentTableActive.Value;
            if(characterTable.currentTableActive.Value){
                ButtonDict.cancelbet=false;
                gameObject.GetComponent<Image>().sprite=sprites[0];
            }
            else{
                ButtonDict.cancelbet=true;
                gameObject.GetComponent<Image>().sprite=sprites[1];
            }
            
            ButtonDict.buttonoffset=0.0f;
            }
           // gameCmdFactory.UndoTableTurn(characterTable).Execute();
        }
    }
}
