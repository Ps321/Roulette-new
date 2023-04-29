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
using Random = UnityEngine.Random;

namespace Infrastructure
{
    public class PlayRoundGateway : IRound
    {
        public int randomNumber { get; set; }

        public IObservable<Unit> PlayTurn()
        {

if(ButtonDict.genratednumber!=int.MaxValue){
randomNumber=ButtonDict.genratednumber;
}else{


        int lowestValue = int.MaxValue;
        int lowestKey = -1;
        randomNumber = -1;


        

            Dictionary<string, int> dict1 = new Dictionary<string, int>();
            /***Lowesttt rowwwwwww****/////
            
            if (ButtonDict.myDictionary.ContainsKey("E1_Eightteen_1"))
                {
                   for(int i=1;i<=18;i++){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["E1_Eightteen_1"]*2;
                    }else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["E1_Eightteen_1"]*2);
                    }
                   }
                }

            if (ButtonDict.myDictionary.ContainsKey("E1_Eightteen_2"))
                {
                   for(int i=19;i<=36;i++){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["E1_Eightteen_2"]*2;
                    }
                    else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["E1_Eightteen_2"]*2);
                    }
                   }
                }


            if (ButtonDict.myDictionary.ContainsKey("E2_Black"))
                {
                   Debug.Log("Blackkkkkk");
                   for(int i=0;i<18;i++){
                    if(dict1.ContainsKey(ButtonDict.black[i].ToString())){
                        Debug.Log("Blackkkkkk Update");
                        dict1[ButtonDict.black[i].ToString()]+=ButtonDict.myDictionary["E2_Black"]*2;
                    }
                    else{
                         Debug.Log("Blackkkkkk Insert");
                        dict1.Add(ButtonDict.black[i].ToString(),ButtonDict.myDictionary["E2_Black"]*2);
                    }
                   }
                }


                if (ButtonDict.myDictionary.ContainsKey("E2_Red"))
                {
                   for(int i=0;i<18;i++){
                    if(dict1.ContainsKey(ButtonDict.red[i].ToString())){
                        dict1[ButtonDict.red[i].ToString()]+=ButtonDict.myDictionary["E2_Red"]*2;
                    }
                    else{
                        dict1.Add(ButtonDict.red[i].ToString(),ButtonDict.myDictionary["E2_Red"]*2);
                    }
                   }
                }


                if (ButtonDict.myDictionary.ContainsKey("E_Even"))
                {
                   for(int i=2;i<=36;i=i+2){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["E_Even"]*2;
                    }
                    else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["E_Even"]*2);
                    }
                   }
                }

                if (ButtonDict.myDictionary.ContainsKey("E_Odd"))
                {
                   for(int i=1;i<=35;i=i+2){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["E_Odd"]*2;
                    }
                    else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["E_Odd"]*2);
                    }
                   }
                }
               




            /***** 2nd lowest rowww***/
            if (ButtonDict.myDictionary.ContainsKey("12_1"))
                {

                     for(int i=1;i<=12;i++){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["12_1"]*3;
                    }else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["12_1"]*3);
                    }
                   }
                }

                if (ButtonDict.myDictionary.ContainsKey("12_2"))
                {

                     for(int i=13;i<=24;i++){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["12_2"]*3;
                    }else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["12_2"]*3);
                    }
                   }
                }

                 if (ButtonDict.myDictionary.ContainsKey("12_3"))
                {

                     for(int i=25;i<=36;i++){
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary["12_3"]*3;
                    }else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary["12_3"]*3);
                    }
                   }
                }



                /**** Coloums*****/

                if (ButtonDict.myDictionary.ContainsKey("c1"))
                {

                     for(int i=0;i<12;i++){
                    if(dict1.ContainsKey(ButtonDict.c1[i].ToString())){
                        dict1[ButtonDict.c1[i].ToString()]+=ButtonDict.myDictionary["c1"]*3;
                    }else{
                        dict1.Add(ButtonDict.c1[i].ToString(),ButtonDict.myDictionary["c1"]*3);
                    }
                   }
                }

                if (ButtonDict.myDictionary.ContainsKey("c2"))
                {

                     for(int i=0;i<12;i++){
                    if(dict1.ContainsKey(ButtonDict.c2[i].ToString())){
                        dict1[ButtonDict.c2[i].ToString()]+=ButtonDict.myDictionary["c2"]*3;
                    }else{
                        dict1.Add(ButtonDict.c2[i].ToString(),ButtonDict.myDictionary["c2"]*3);
                    }
                   }
                }

                 if (ButtonDict.myDictionary.ContainsKey("c3"))
                {

                     for(int i=0;i<12;i++){
                    if(dict1.ContainsKey(ButtonDict.c3[i].ToString())){
                        dict1[ButtonDict.c3[i].ToString()]+=ButtonDict.myDictionary["c3"]*3;
                    }else{
                        dict1.Add(ButtonDict.c3[i].ToString(),ButtonDict.myDictionary["c3"]*3);
                    }
                   }
                }

                /*****Numbers******/
                     for(int i=0;i<=37;i++){
                         if (ButtonDict.myDictionary.ContainsKey(i.ToString()))
                    {
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary[i.ToString()]*36;
                    }else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary[i.ToString()]*36);
                    }
                   }
                     }


            /***Middle Rowss****/

            for(int i=4;i<=36;i++){
                string s="mrow_"+i;
                    if (ButtonDict.myDictionary.ContainsKey(s))
                    {
                    if(dict1.ContainsKey(i.ToString())){
                        dict1[i.ToString()]+=ButtonDict.myDictionary[s]*18;
                    }else{
                        dict1.Add(i.ToString(),ButtonDict.myDictionary[s]*18);
                    }
                    if(dict1.ContainsKey((i-3).ToString())){
                        dict1[(i-3).ToString()]+=ButtonDict.myDictionary[s]*18;
                    }else{
                        dict1.Add((i-3).ToString(),ButtonDict.myDictionary[s]*18);
                    }

                   }
              }





            /***Middle Coloums****/

            for(int i=0;i< ButtonDict.col.Length;i++){
                string s="mcolumn_"+ButtonDict.col[i];
                    if (ButtonDict.myDictionary.ContainsKey(s))
                    {
                    if(dict1.ContainsKey(ButtonDict.col[i].ToString())){
                        dict1[ButtonDict.col[i].ToString()]+=ButtonDict.myDictionary[s]*18;
                    }else{
                        dict1.Add(ButtonDict.col[i].ToString(),ButtonDict.myDictionary[s]*18);
                    }
                    if(dict1.ContainsKey((ButtonDict.col[i]-1).ToString())){
                        dict1[(ButtonDict.col[i]-1).ToString()]+=ButtonDict.myDictionary[s]*18;
                    }else{
                        dict1.Add((ButtonDict.col[i]-1).ToString(),ButtonDict.myDictionary[s]*18);
                    }

                   }
              }


              /***Coloumn with 3 values Coloums****/

            for(int i=0;i< ButtonDict.col3.Length;i++){
                string s="mcolumn_"+ButtonDict.col3[i];
                    if (ButtonDict.myDictionary.ContainsKey(s))
                    {
                    if(dict1.ContainsKey(ButtonDict.col3[i].ToString())){
                        dict1[ButtonDict.col3[i].ToString()]+=ButtonDict.myDictionary[s]*12;
                    }else{
                        dict1.Add(ButtonDict.col3[i].ToString(),ButtonDict.myDictionary[s]*12);
                    }
                    if(dict1.ContainsKey((ButtonDict.col3[i]+1).ToString())){
                        dict1[(ButtonDict.col3[i]+1).ToString()]+=ButtonDict.myDictionary[s]*12;
                    }else{
                        dict1.Add((ButtonDict.col3[i]+1).ToString(),ButtonDict.myDictionary[s]*12);
                    }

                    if(dict1.ContainsKey((ButtonDict.col3[i]+2).ToString())){
                        dict1[(ButtonDict.col3[i]+2).ToString()]+=ButtonDict.myDictionary[s]*12;
                    }else{
                        dict1.Add((ButtonDict.col3[i]+2).ToString(),ButtonDict.myDictionary[s]*12);
                    }

                   }
              }

            /***Middle Center Rows and Coloums****/

            for(int i=0;i< ButtonDict.center.Length;i++){
                string s="mcorner_"+ButtonDict.center[i];
                    if (ButtonDict.myDictionary.ContainsKey(s))
                    {
                    if(dict1.ContainsKey(ButtonDict.center[i].ToString())){
                        dict1[ButtonDict.center[i].ToString()]+=ButtonDict.myDictionary[s]*9;
                    }else{
                        dict1.Add(ButtonDict.center[i].ToString(),ButtonDict.myDictionary[s]*9);
                    }
                    if(dict1.ContainsKey((ButtonDict.center[i]-1).ToString())){
                        dict1[(ButtonDict.center[i]-1).ToString()]+=ButtonDict.myDictionary[s]*9;
                    }else{
                        dict1.Add((ButtonDict.center[i]-1).ToString(),ButtonDict.myDictionary[s]*9);
                    }

                    if(dict1.ContainsKey((ButtonDict.center[i]-3).ToString())){
                        dict1[(ButtonDict.center[i]-3).ToString()]+=ButtonDict.myDictionary[s]*9;
                    }else{
                        dict1.Add((ButtonDict.center[i]-3).ToString(),ButtonDict.myDictionary[s]*9);
                    }

                    if(dict1.ContainsKey((ButtonDict.center[i]-4).ToString())){
                        dict1[(ButtonDict.center[i]-4).ToString()]+=ButtonDict.myDictionary[s]*9;
                    }else{
                        dict1.Add((ButtonDict.center[i]-4).ToString(),ButtonDict.myDictionary[s]*9);
                    }

                   }
              }







        if(ButtonDict.rule==1){
          

         if(dict1.Count>0){
        string lowestValueKey = dict1.OrderBy(kv => kv.Value).First().Key;
        randomNumber=int.Parse(lowestValueKey);

            }else{
                randomNumber= UnityEngine.Random.Range(0, 38);
            }
        }else{
            int[] myarr = new int[38];
                int index = 0;
                int c = 0;

                for (int i = 0; i <= 37; i++)
                {
                    myarr[i] = -1;
                }

                for (int i = 0; i <= 37; i++)
                {
                    if (!ButtonDict.myDictionary.ContainsKey(i.ToString()))
                    {
                        myarr[index] = i;
                        index++;
                        c++;
                    }
                }

                 int count = 0;
                for (int i = 0; i < myarr.Length; i++)
                {
                    if (myarr[i] != -1)
                    {
                        count++;
                    }
                }

               
                if (count > 0)
                {
                    
                    int aaaa = UnityEngine.Random.Range(0, count);
                    randomNumber = myarr[aaaa];
                }else{
                     if(dict1.Count>0){
        string lowestValueKey = dict1.OrderBy(kv => kv.Value).First().Key;
        randomNumber=int.Parse(lowestValueKey);

            }else{
                randomNumber= UnityEngine.Random.Range(0, 38);
            }
                }


        }

            
            

        
}
            return Observable.Return(Unit.Default)
                    .Do(_ => Debug.Log($"Generating number {randomNumber} for the roullete game round!"));
        }
    }
}

