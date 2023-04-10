using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ViewModel;
using UniRx;
using Commands;
using System;

namespace Components
{
    public class CameraPlayDisplay : MonoBehaviour
    {

        public bool rotate=true;
        public Text t;
        public CharacterTable characterTable;
        public Animator mainCameraAnimator;
        public Animator rouletteAnimator;
        public GameObject gg;
        public GameObject actualtable;
                public GameObject notactualtable;
        public Text winnerText;

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
               actualtable.SetActive(false);
               notactualtable.SetActive(true);
            }
            else{
                winnerText.text= ButtonDict.winnerval.ToString();
                rouletteAnimator.SetBool("spin", isRound);
                gg.SetActive(false);
                              actualtable.SetActive(true);
               notactualtable.SetActive(false);
            }
            
           // mainCameraAnimator.SetBool("Play", isRound);
        
        }

        public void setval(){
            rotate=!rotate;
            if(rotate){
                t.text="Wheel Zoom On";
            }
            else{
                t.text="Wheel Zoom Off";
            }
        }

    }
}
