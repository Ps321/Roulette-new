using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewModel;
using UniRx;
using System;

namespace Components
{
    public class RoulleteRotateDisplay : MonoBehaviour
    {    
        public GameRoullete gameRoullete;
        public GameObject ballRotator;
        public GameObject wheelRotator;
        public Animator aa;

        void Start()
        {
            gameRoullete.currentSpeed = gameRoullete.defaultSpeed;
            gameRoullete.OnRotate
                .Subscribe(OnRotateRoullete)
                .AddTo(this);
        }

        private void OnRotateRoullete(bool isRotate)
        {
            gameRoullete.currentSpeed = gameRoullete.defaultSpeed;
        }

        void FixedUpdate()
        {
            
            if(gameRoullete.currentSpeed != gameRoullete.defaultSpeed){
                aa.SetBool("diamond",true);
            wheelRotator.transform.Rotate(Vector3.forward * gameRoullete.currentSpeed * Time.deltaTime);
            ballRotator.transform.Rotate(Vector3.back * gameRoullete.currentSpeed * 3 * Time.deltaTime);  
        }
        else{
           aa.SetBool("diamond",false);
        }
        }
    }
}
