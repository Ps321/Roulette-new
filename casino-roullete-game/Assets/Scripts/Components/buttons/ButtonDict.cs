using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Commands
{
     
    public class ButtonDict : MonoBehaviour
    {

        public static int first=0;
        public static Button highlightedButton;
        public static Dictionary<string,int> myDictionary;
        // Start is called before the first frame update
        void Start()
        {
         myDictionary = new Dictionary<string, int>();
        }

        
       
    }
}
