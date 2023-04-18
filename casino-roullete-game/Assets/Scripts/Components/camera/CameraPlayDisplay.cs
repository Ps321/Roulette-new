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

        public bool rotate=false;
        public Text t;
        public CharacterTable characterTable;
        public Animator mainCameraAnimator;
        public Animator rouletteAnimator;
        public GameObject gg;

        public AudioSource audio;
        public GameObject actualtable;
                public GameObject notactualtable;
        public Text winnerText;
        public bool spin=false;

        void Start()
        {
            characterTable.OnRound
                .Subscribe(AnimateMainCamera)
                .AddTo(this);
        }

        public void AnimateMainCamera(bool isRound)
        {

            Debug.Log(spin+"yeeeeeeeeeeeeee");
            ButtonDict.wheelanim=spin;
            
            if(isRound==true){
                 if(spin){
                rouletteAnimator.SetBool("spin", isRound);
                 }
                gg.SetActive(true);
               actualtable.SetActive(false);
               audio.Stop();
               notactualtable.SetActive(true);
            }
            else{
                winnerText.text= ButtonDict.winnerval.ToString();
               
                    rouletteAnimator.SetBool("spin", isRound);
               
                gg.SetActive(false);
                              actualtable.SetActive(true);
                              audio.Play();
               notactualtable.SetActive(false);
            }
            
           // mainCameraAnimator.SetBool("Play", isRound);
        
        }

        public void setval(){
            rotate=!rotate;
            if(rotate){
                spin=true;
                t.text="Wheel Zoom Off";
            }
            else{
                spin=false;
                t.text="Wheel Zoom On";
            }
        }

    }
}
