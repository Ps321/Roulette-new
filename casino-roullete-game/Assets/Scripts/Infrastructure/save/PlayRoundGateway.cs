using System;
using UniRx;
using UnityEngine;
using Commands;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using ViewModel;
using System.Collections;
using Managers;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Random=UnityEngine.Random;

namespace Infrastructure
{
    public class PlayRoundGateway : IRound
    {
        public int randomNumber { get; set; }

        public IObservable<Unit> PlayTurn()
        {
            string keyWithLowestValue = ButtonDict.myDictionary.OrderBy(kvp => kvp.Value).FirstOrDefault().Key;
            Debug.Log(keyWithLowestValue+"myarr");
            
            randomNumber  = int.Parse(keyWithLowestValue);
            int[] myarr=new int[36];
            int index=0;
             
            for(int i=0;i<36;i++){
                myarr[i]=-1;
            }
            
            for(int i=0;i<36;i++){
                if(!ButtonDict.myDictionary.ContainsKey(i.ToString())){
                    myarr[index]=i;
                }
            }
         /* randomNumber=Random.Range(0,2);
          
            while(myarr[randomNumber]==-1){
                randomNumber=Random.Range(0,myarr.Length-1);
            }
            randomNumber=myarr[randomNumber];
            Debug.Log(randomNumber+"myarr");*/
            
            
            return Observable.Return(Unit.Default)
                    .Do(_ => Debug.Log($"Generating number {randomNumber} for the roullete game round!"));
        }
    }
}

