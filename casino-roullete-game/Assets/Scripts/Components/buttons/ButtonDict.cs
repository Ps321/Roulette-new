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
        public static int first_1=0;
        public static bool cancelbet=false;

        public static int winnerval=0;
        public static float buttonoffset=0.7f;
        public static Button highlightedButton;

        public static bool buttonenable=true;
        public Text winnertext;
        public static bool updatekro=true;
        public static int[] c3=new int[] { 3,6,9,12,15,18,21,24,27,30,33,36};
        public static int[] c2=new int[] { 2,5,8,11,14,17,20,23,26,29,32,35};
        public static int[] c1=new int[] { 1,4,7,10,13,16,19,22,25,28,31,34};

        public static int[] red=new int[] { 1,3,5,7,9,12,14,16,18,19,21,23,25,27,30,32,34,36};

       public static int[] black=new int[] { 2,4,6,8,10,11,13,15,17,20,24,22,26,28,29,31,33,35};   
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
