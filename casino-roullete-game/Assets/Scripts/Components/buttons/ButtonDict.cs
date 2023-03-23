using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
     
    public class ButtonDict : MonoBehaviour
    {
         public Dictionary<string,int> myDictionary;
        // Start is called before the first frame update
        void Start()
        {
         myDictionary = new Dictionary<string, int>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
