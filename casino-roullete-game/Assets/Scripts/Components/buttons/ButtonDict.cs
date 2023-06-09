using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using ViewModel;

namespace Commands
{
     
    public class ButtonDict : MonoBehaviour
    {


        public static bool wheelanim=true;

        public static int OnLoadWinner=0;
        public static int OnWinnerUpdate=0;
        public static bool previousclicked=false;
        public CharacterTable characterTable;

        public static bool masterClient=false; 
        public static bool winloss=false;
        public static int paymentWin;
        public static int paymentLost;

        public static int first=0;

        public static int genratednumber=int.MaxValue;
        public static int loadedfirst=0;
        public static int lastfive=0;
        public static int first_1=0;

        public static int rulefetch=0;
        public static AudioSource audio;
        public static int rule=0;
        public static int currentchipvalue=10;
        public static int winnernumber=0;
        public static bool cancelbet=false;

        public static int winnerval=0;
        public static float buttonoffset=0.7f;
        public static Button highlightedButton;

        public static bool buttonenable=true;
        public Text winnertext;

        public static int gameclose=0;
        public static int loadgame=0;
        public static bool updatekro=true;
        public static int[] c3=new int[] { 3,6,9,12,15,18,21,24,27,30,33,36};
        public static int[] c2=new int[] { 2,5,8,11,14,17,20,23,26,29,32,35};
        public static int[] c1=new int[] { 1,4,7,10,13,16,19,22,25,28,31,34};

        public static int[] col=new int[] {2,3,5,6,8,9,11,12,14,15,17,18,20,21,23,24,26,27,29,30,32,33,35,36 };
        public static int[] col3=new int[] {1,4,7,10,13,16,19,22,25,28,31,34 };
        public static int[] center=new int[] {5,6,8,9,11,12,14,15,17,18,20,21,23,24,26,27,29,30,32,33,35,36};
        
        
        
        public static int[] red=new int[] { 1,3,5,7,9,12,14,16,18,19,21,23,25,27,30,32,34,36};

       public static int[] black=new int[] { 2,4,6,8,10,11,13,15,17,20,24,22,26,28,29,31,33,35};   
        public static bool betok=false;
        public static Dictionary<string,int> myDictionary;

        public static Dictionary<string,int> previousbet;
        public static Dictionary<string,int> allnumbers;

        public GameObject Errorscreen;
        
        // Start is called before the first frame update
        void Start()
        {
         myDictionary = new Dictionary<string, int>();
          previousbet = new Dictionary<string, int>();
        }

        private void Update() {
            if(updatekro==true){
                
             //   winnertext.text=characterTable.characterMoney.currentWinnerValue.Value.ToString();
                updatekro=false;
            }
        }

     
        public void Take(){
         
          Debug.Log(ButtonDict.winnerval);
          
             StartCoroutine(takething());
          
         
           
        }

        IEnumerator takething(){
            if(ButtonDict.winnerval>=4000){
                // var startamount=ButtonDict.winnerval-3000;
                // characterTable.characterMoney.AddCash1(startamount);
                // ButtonDict.winnerval-=startamount;
                 for(int i=ButtonDict.winnerval;i>0;i=i-1){
                yield return new WaitForSeconds(0.00001f);
                characterTable.characterMoney.AddCash1(1);
                ButtonDict.winnerval-=1;
            }
            }

            else if(ButtonDict.winnerval>=1000){
                 for(int i=ButtonDict.winnerval;i>0;i=i-1){
                yield return new WaitForSeconds(0.00001f);
                characterTable.characterMoney.AddCash1(1);
                ButtonDict.winnerval-=1;
            }
            }
            else{
                int i;
                 for( i=ButtonDict.winnerval;i>0;i=i-1){
                yield return new WaitForSeconds(0.001f);
                characterTable.characterMoney.AddCash1(1);
                ButtonDict.winnerval-=1;
            }
            
          

            }

           
            characterTable.characterMoney.currentWinnerValue.Value=0;
            ButtonDict.winnerval=0;
            ButtonDict.updatekro=true;
             StartCoroutine(insertpoints());
        }


        public static void Destroykro(GameObject obj){
            Destroy(obj);
        }

        IEnumerator insertpoints(){
            yield return new WaitForSeconds(0.5f);
            WWWForm form = new WWWForm();
       if(ButtonDict.loadedfirst==1){
        form.AddField("id", PlayerPrefs.GetInt("id"));
        form.AddField("points", characterTable.characterMoney.getmoney());
        form.AddField("winnerval", ButtonDict.winnerval);
       }


        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/pointsupdate.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Errorscreen.SetActive(true);
                Debug.Log(www.error);
            }
            else
            {
                Errorscreen.SetActive(false);
                Debug.Log("update points");            }
        }
        }
    }

        
       
    }

