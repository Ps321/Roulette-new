using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ViewModel;

namespace Commands
{
     
    public class ButtonDict : MonoBehaviour
    {
        public CharacterTable characterTable;

        public static int first=0;

        public static int winnerval=0;
        public static float buttonoffset=0.7f;
        public static Button highlightedButton;
        public Text winnertext;
        public static bool updatekro=true;
        public static int[] c3=new int[] { 3,6,9,12,15,18,21,24,27,30,33,36};
        public static int[] c2=new int[] { 2,5,8,11,14,17,20,23,26,29,32,35};
        public static int[] c1=new int[] { 1,4,7,10,13,16,19,22,25,28,31,34};

        public static bool betok=false;
        public static Dictionary<string,int> myDictionary;
        // Start is called before the first frame update
        void Start()
        {
         myDictionary = new Dictionary<string, int>();
        }

        private void Update() {
            if(updatekro==true){
                
                winnertext.text=characterTable.characterMoney.currentWinnerValue.Value.ToString();
                updatekro=false;
            }
        }

        public void Take(){
           characterTable.characterMoney.AddCash1(characterTable.characterMoney.currentWinnerValue.Value);
        }

        public static void Destroykro(GameObject obj){
            Destroy(obj);
        }

        
       
    }
}
