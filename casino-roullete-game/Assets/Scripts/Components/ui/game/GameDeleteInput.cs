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
            gameCmdFactory.ResetTableTurn(magnetDestroyerDisplay, characterTable).Execute();
            characterTable.currentTableActive.Value=false;
            aa.SetBool("clicked",false);
            table.SetBool("clicked",false);
            roulette.SetActive(true);
        }
    }
}
