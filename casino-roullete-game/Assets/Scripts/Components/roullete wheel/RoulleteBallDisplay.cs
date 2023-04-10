using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using ViewModel;
using UniRx;
using System.Linq;
using System;

namespace Components
{
    public class RoulleteBallDisplay : MonoBehaviour
    {
        public CharacterTable characterTable;
        public GameRoullete gameRoullete;
        public GameObject[] _anchorNumbers;
        public GameObject sphereContainer;
        public GameObject sphereDefault;

        private GameObject _sphere;
        private Vector3 _ballPosition;
        
        void Start()
        {
            characterTable.OnRound
                .Subscribe(OnRound)
                .AddTo(this);

            gameRoullete.OnNumber
                .Subscribe(SetBallInRoullete)
                .AddTo(this);
        }

        private void OnRound(bool isRound)
        {
            if(isRound)
            {
                DestroyLastSphere();
                sphereDefault.SetActive(true);
            }
        }

        public void SetBallInRoullete(int num)
        {    
            sphereDefault.SetActive(false);
            DestroyLastSphere();

            // Activate ball and position in the indicated number
            IEnumerable<GameObject> anchor = _anchorNumbers.Where(anc => anc.name == $"handler_{num}");
            _ballPosition = anchor.ToArray()[0].gameObject.transform.position;
            
            _sphere = Instantiate(gameRoullete.sphere,_ballPosition,Quaternion.Euler(0,0,0));
            _sphere.transform.position = _ballPosition;
           // _sphere.transform.rotation=Quaternion.identity;
            _sphere.SetActive(true);
            _sphere.transform.SetParent(sphereContainer.transform);
            sphereContainer.GetComponent<Animator>().SetBool("rotate",true);
             StartCoroutine(changevalue());
            Debug.Log($"Roullete positioning ball in number {num}!");
            StartCoroutine(lastfivenumber(num));
        }

            IEnumerator changevalue(){
                yield return new WaitForSeconds(1.5f);
                sphereContainer.GetComponent<Animator>().SetBool("rotate",false);

            }
        IEnumerator lastfivenumber(int value){
           
            WWWForm form = new WWWForm();
            
            
                form.AddField("number", value.ToString());
            
       
        
        


        using (UnityWebRequest www = UnityWebRequest.Post("https://roulettegame.online/lastfive.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("update points");        
                
           }
        }
        }
    
        void DestroyLastSphere()
        {
            if(sphereContainer.transform.childCount > 0)
                Destroy(_sphere);
        }
    }
}

