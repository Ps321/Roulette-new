using System.Collections.Generic;
using UnityEngine;
using Components;
using ViewModel;
using UniRx;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace Commands
{
    [RequireComponent (typeof(Button))]
    public class ButtonTableInput : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {

        public Animator animation;
        public Animator table;
        public GameObject roulette;

        public CharacterTable characterTable;
        public ButtonTable buttonData;

        private IStatusButton _statusButton;
        private IReseteableButton _resetableButton;
        private IInteractableButton _interactableButton;
        public ILongPress _longPress;
       public GameObject ab;
       int first=0;
         ChipSelectInput c;
        
         Dictionary<string,int> myDictionary;

         public ButtonDict dd;

        
        
        void Awake() 
        {
            _statusButton = GetComponent<IStatusButton>();    
            _resetableButton = GetComponent<IReseteableButton>();    
            _interactableButton = GetComponent<IInteractableButton>();    
            _longPress = GetComponent<ILongPress>();    
        }
        
        void Start()
        {
           
            _resetableButton.ResetButton(buttonData);

            characterTable.currentTableActive
                .Subscribe(OnActiveButton)
                .AddTo(this);

            c=ab.GetComponent<ChipSelectInput>();
         
        }
        
        private void OnActiveButton(bool isActive)
        {
            _statusButton._isActive = isActive;
            if(_statusButton._isActive) _resetableButton.ResetButton(buttonData);       
        }

        public void Click()
        {
            if (!_statusButton._isActive)
                return;

               

               
                
          
            animation.SetBool("clicked",true);
            table.SetBool("clicked",true);
            roulette.SetActive(false);
            
             if(ButtonDict.first==0){
                    buttonData.currentOffset.y=0f;
                   ButtonDict.first=1;
                  return;
                   
                }
          
            buttonData.currentChipsOnTop= buttonData.currentChipsOnTop+ c.currentchipvalue -1;
            if(ButtonDict.myDictionary.ContainsKey(buttonData.buttonName)){
                
                ButtonDict.myDictionary[buttonData.buttonName]=buttonData.currentChipsOnTop+1;
            }else{
                ButtonDict.myDictionary.Add(buttonData.buttonName,buttonData.currentChipsOnTop+1);
            }


            _interactableButton.InstantiateChip(characterTable, buttonData);
            
        }
        public void Click1()
        {
            if (!_statusButton._isActive)
                return;

            if(ButtonDict.first==0){
                ButtonDict.first=1;
                    return;
                }
           
                animation.SetBool("clicked",true);
                table.SetBool("clicked",true);
                roulette.SetActive(false);
            buttonData.currentChipsOnTop= buttonData.currentChipsOnTop+ c.currentchipvalue -1;
          if(ButtonDict.myDictionary.ContainsKey(buttonData.buttonName)){
                
                ButtonDict.myDictionary[buttonData.buttonName]=buttonData.currentChipsOnTop+1;
            }else{
                ButtonDict.myDictionary.Add(buttonData.buttonName,buttonData.currentChipsOnTop+1);
            }
            _interactableButton.InstantiateChip1(characterTable, buttonData);
        }

        public void OnPointerDown (PointerEventData eventData) 
        {
            _longPress.SetPointerDown(true);   
        }

        private void Update ()
        {
            _longPress.LongPressCheck(characterTable, buttonData);
        }
        
        public  void OnPointerUp (PointerEventData eventData) 
        {
            _longPress.LongPress(characterTable, buttonData, false);
            _longPress.ResetPointer();
            Click();
        }
    }
}
