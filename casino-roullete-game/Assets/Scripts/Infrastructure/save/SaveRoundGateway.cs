using System;
using UniRx;
using UnityEngine;
using UnityEditor;
using ViewModel;
using System.Collections;
using Managers;
using UnityEngine.Networking;
using Commands;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Infrastructure
{
    public class SaveRoundGateway : ISaveRound
    {
        private static protected readonly string FILE_NAME = "player";
        public Round roundData {get; set;}

        public IObservable<Unit> RoundSequentialSave(Round roundData)
        {
            return Observable.FromCoroutine<Unit>(observer => SavePlayer(observer, roundData));
        }

        public IObservable<Unit> RoundSequentialLoad()
        {
            return Observable.FromCoroutine<Unit>(observer => LoadPlayer(observer));
        }

        IEnumerator SavePlayer(IObserver<Unit> observer, Round roundData)
        {
            string path = GameManager.Instance.UrlDataPath + FILE_NAME;
            string json = JsonUtility.ToJson(roundData);

            File.WriteAllText(path, json);
            Debug.Log($"Saved data JSON with the table {roundData.idPlayer} with {json}");

            yield return new WaitUntil(() => File.Exists(path));
            
            observer.OnNext(Unit.Default); // push Unit or all buffer result.
            observer.OnCompleted();
        }

        IEnumerator LoadPlayer(IObserver<Unit> observer) 
        {

               ButtonDict.lastfive=1;
            string path = GameManager.Instance.UrlDataPath + FILE_NAME;
            string json = File.ReadAllText(path);

            yield return new WaitUntil(() => json != null);
            
            roundData = JsonUtility.FromJson<Round>(json);
            Debug.Log($"Loaded data JSON with the table {roundData.idPlayer} with {json}");

               WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("id"));
       
        


        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/readpoints.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string s= www.downloadHandler.text.Trim();   
                roundData.playerMoney=  int.Parse(s);
                ButtonDict.loadedfirst=1;   
                Debug.Log("Score has been fetched from database");
                
             }
        }




            
            observer.OnNext(Unit.Default); // push Unit or all buffer result.
            observer.OnCompleted();
        }
    }
}

