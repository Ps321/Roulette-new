using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewModel;
using UniRx;
using Commands;
using System;

namespace Components
{
    public class CameraPlayDisplay : MonoBehaviour
    {
        public CharacterTable characterTable;
        public Animator mainCameraAnimator;
        public Animator rouletteAnimator;
        public GameObject gg;

        void Start()
        {
            characterTable.OnRound
                .Subscribe(AnimateMainCamera)
                .AddTo(this);
        }

        public void AnimateMainCamera(bool isRound)
        {


            if(isRound==true){
                rouletteAnimator.SetBool("spin", isRound);
                gg.SetActive(true);
            }
            else{
                rouletteAnimator.SetBool("spin", isRound);
                gg.SetActive(false);
            }
           // mainCameraAnimator.SetBool("Play", isRound);
        }
    }
}
