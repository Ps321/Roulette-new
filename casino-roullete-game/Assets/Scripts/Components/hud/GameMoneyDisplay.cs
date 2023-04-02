using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using UniRx;
using Commands;
using ViewModel;
using System;
using Controllers;

namespace Components
{
    public class GameMoneyDisplay : MonoBehaviour
    {
        public CharacterTable characterTable;
        public Text moneyLabel;
        public Text betLabel;


        public void Start()
        {
            characterTable.characterMoney.characterBet
                .Subscribe(OnChangeBet)
                .AddTo(this);

            characterTable.characterMoney.characterMoney
                .Subscribe(OnChangeMoney)
                .AddTo(this);
        }

        private void OnChangeBet(int value)
        {
            betLabel.text = value.ToString();
            StartCoroutine(insertpoints(value));
        }

        private void OnChangeMoney(int value)
        {
            moneyLabel.text = value.ToString();
        }

        IEnumerator insertpoints(int value){
            if(ButtonDict.loadedfirst==1){
            WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("id"));
        form.AddField("points", characterTable.characterMoney.getmoney());
        


        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/pointsupdate.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("update points");            }
        }
        }
    }
}
}