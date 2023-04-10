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


            int lowestValue = int.MaxValue;
            int lowestKey = -1;
            randomNumber=-1;
            Debug.Log("Rule value"+ButtonDict.rule);
                if(ButtonDict.rule==1){
               Dictionary<string, int> dict1 = new Dictionary<string, int>();
                 if(ButtonDict.myDictionary.ContainsKey("E1_Eightteen_1")){
                    dict1.Add("E1_Eightteen_1",ButtonDict.myDictionary["E1_Eightteen_1"]);
                }
                 if(ButtonDict.myDictionary.ContainsKey("E1_Eightteen_2")){
                    dict1.Add("E1_Eightteen_2",ButtonDict.myDictionary["E1_Eightteen_2"]);
                }
                 if(ButtonDict.myDictionary.ContainsKey("E2_Black")){
                    dict1.Add("E2_Black",ButtonDict.myDictionary["E2_Black"]);
                }
                 if(ButtonDict.myDictionary.ContainsKey("E2_Red")){
                    dict1.Add("E2_Red",ButtonDict.myDictionary["E2_Red"]);
                }
                 if(ButtonDict.myDictionary.ContainsKey("E_Even")){
                    dict1.Add("E_Even",ButtonDict.myDictionary["E_Even"]);
                }
                 if(ButtonDict.myDictionary.ContainsKey("E_Odd")){
                    dict1.Add("E_Odd",ButtonDict.myDictionary["E_Odd"]);
                }
            string keyWithLowestValue1 = dict1.OrderBy(kvp => kvp.Value).FirstOrDefault().Key;


           
                Dictionary<string, int> dict = new Dictionary<string, int>();


                if(ButtonDict.myDictionary.ContainsKey("12_1")){
                    dict.Add("12_1",ButtonDict.myDictionary["12_1"]);
                }
                if(ButtonDict.myDictionary.ContainsKey("12_2")){
                   dict.Add("12_2",ButtonDict.myDictionary["12_2"]);
                }
                if(ButtonDict.myDictionary.ContainsKey("12_3")){
                   dict.Add("12_3",ButtonDict.myDictionary["12_3"]);
                }
                if(ButtonDict.myDictionary.ContainsKey("c1")){
                    dict.Add("c1",ButtonDict.myDictionary["c1"]);
                }
                 if(ButtonDict.myDictionary.ContainsKey("c2")){
                    dict.Add("c2",ButtonDict.myDictionary["c2"]);
                }
                 if(ButtonDict.myDictionary.ContainsKey("c3")){
                    dict.Add("c3",ButtonDict.myDictionary["c3"]);
                }

                string keyWithLowestValue = dict.OrderBy(kvp => kvp.Value).FirstOrDefault().Key;
          
                string proceed="";
        
        if(keyWithLowestValue!="" && keyWithLowestValue1!=""  && keyWithLowestValue!=null && keyWithLowestValue1!=null ){
            if(ButtonDict.myDictionary[keyWithLowestValue]*2 < ButtonDict.myDictionary[keyWithLowestValue1])
            {
                proceed=keyWithLowestValue;
            }
            else{
                proceed=keyWithLowestValue1;
            }
        }
        else if(keyWithLowestValue!=""  && keyWithLowestValue!=null){
            proceed=keyWithLowestValue;
        }        
        else{
             proceed=keyWithLowestValue1;
        }
        
        
        
           if(proceed=="12_1"){
            randomNumber=UnityEngine.Random.Range(1,13);
           }
           if(proceed=="12_2"){
            randomNumber=UnityEngine.Random.Range(13,25);
           }
           if(proceed=="12_3"){
            randomNumber=UnityEngine.Random.Range(25,37);
           }
           if(proceed=="c1"){
            int val=UnityEngine.Random.Range(1,13);
            randomNumber=ButtonDict.c1[val];
           }
           if(proceed=="c2"){
            int val=UnityEngine.Random.Range(0,13);
            randomNumber=ButtonDict.c2[val];
           }
           if(proceed=="c3"){
            int val=UnityEngine.Random.Range(0,13);
            randomNumber=ButtonDict.c3[val];
           }

           if(proceed=="E1_Eightteen_1"){
            randomNumber=UnityEngine.Random.Range(1,19);
           }
           if(proceed=="E1_Eightteen_2"){
            randomNumber=UnityEngine.Random.Range(18,37);
           }
           if(proceed=="E2_Black"){
             int val=UnityEngine.Random.Range(0,18);
            randomNumber=ButtonDict.black[val];
           }
           if(proceed=="E2_Red"){
            int val=UnityEngine.Random.Range(0,18);
            randomNumber=ButtonDict.red[val];
           }
           if(proceed=="E_Even"){
           randomNumber=UnityEngine.Random.Range(0,19)*2;
           
           }
           if(proceed=="E_Odd"){
            randomNumber=(UnityEngine.Random.Range(0,19)*2)+1;
          
           }
            

            if(randomNumber==-1){

                 for (int i = 0; i <= 37; i++) {
                string s=i.ToString();
                if(ButtonDict.myDictionary.ContainsKey(s)){
               if( ButtonDict.myDictionary[s]<lowestValue){
                lowestValue = ButtonDict.myDictionary[i.ToString()];
                randomNumber = i;
               }
                }
            }


            }
                }
                else{
                  
            int[] myarr=new int[37];
            int index=0;
            int c=0;
             
            for(int i=0;i<37;i++){
                myarr[i]=-1;
            }
            
            for(int i=0;i<37;i++){
                if(!ButtonDict.myDictionary.ContainsKey(i.ToString())){
                    myarr[index]=i;
                    index++;
                    c++;
                }
            }
              Debug.Log(" not Randomlyyy generatedd"+myarr.Length);
                if(myarr.Length>0){
                    int aaaa=UnityEngine.Random.Range(0,c);
                        randomNumber=myarr[aaaa];
                }
            
                }

            if(randomNumber==-1){
                Debug.Log("Randomlyyy generatedd");
                 randomNumber=UnityEngine.Random.Range(0,37);
            }
           /* string keyWithLowestValue = ButtonDict.myDictionary.OrderBy(kvp => kvp.Value).FirstOrDefault().Key;
            Debug.Log(keyWithLowestValue+"myarr");
            
           if(keyWithLowestValue!=""){ 
            
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
           }
           else{
            randomNumber=UnityEngine.Random.Range(0,36);
           }*/
        
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

