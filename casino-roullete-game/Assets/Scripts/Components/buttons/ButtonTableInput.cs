using System.Collections.Generic;
using UnityEngine;
using Components;
using ViewModel;
using UniRx;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace Commands
{
    [RequireComponent(typeof(Button))]
    public class ButtonTableInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        public Animator animation;
        public Animator table;
        public GameObject roulette;
        public AudioSource audio;

        public CharacterTable characterTable;
        public ButtonTable buttonData;

        private IStatusButton _statusButton;
        private IReseteableButton _resetableButton;
        private IInteractableButton _interactableButton;
        public ILongPress _longPress;
        public GameObject ab;
        int first = 0;
        ChipSelectInput c;

        Dictionary<string, int> myDictionary;

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
            ButtonDict.first = 0;
            _resetableButton.ResetButton(buttonData);

            characterTable.currentTableActive
                .Subscribe(OnActiveButton)
                .AddTo(this);

            c = ab.GetComponent<ChipSelectInput>();

        }

        private void OnActiveButton(bool isActive)
        {
            _statusButton._isActive = isActive;
            if (_statusButton._isActive) _resetableButton.ResetButton(buttonData);
        }

        public void Click()
        {
            if(!ButtonDict.buttonenable){
                return;
            }
            if (ButtonDict.betok)
            {
                return;
            }
            if (!_statusButton._isActive)
            {

                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Chip");

                // Loop through each object and look for a Button component
                foreach (GameObject obj in objectsWithTag)
                {
                    string name = obj.GetComponent<ChipGame>().chipname;
                    Debug.Log(buttonData.buttonName);
                    //  name=name.Replace("Number_","");


                    if (buttonData.buttonName == name)
                    {
                        ButtonDict.myDictionary.Remove(buttonData.buttonName);
                        characterTable.characterMoney.AddCash2(buttonData.currentChipsOnTop);
                        // Enable the button
                        Destroy(obj);
                    }

                }
                return;
            }




            audio.Play();

            animation.SetBool("clicked", true);
            table.SetBool("clicked", true);
            roulette.SetActive(false);

            if (ButtonDict.first == 0)
            {
                characterTable.currentTableActive.Value = false;
                StartCoroutine(disabletable());
                buttonData.currentOffset.y = 0f;
                ButtonDict.first = 1;
                return;

            }

            buttonData.currentChipsOnTop = buttonData.currentChipsOnTop + c.currentchipvalue - 1;
            if (ButtonDict.myDictionary.ContainsKey(buttonData.buttonName))
            {

                ButtonDict.myDictionary[buttonData.buttonName] = buttonData.currentChipsOnTop + 1;
            }
            else
            {
                ButtonDict.myDictionary.Add(buttonData.buttonName, buttonData.currentChipsOnTop + 1);
            }


            _interactableButton.InstantiateChip(characterTable, buttonData);

        }
        public void Click1()
        {
            if(characterTable.characterMoney.characterMoney.Value<=ButtonDict.currentchipvalue){
            return;
            }
            if (ButtonDict.betok)
            {
                return;
            }
            if (!_statusButton._isActive)
            {
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Chip");

                // Loop through each object and look for a Button component
                foreach (GameObject obj in objectsWithTag)
                {
                    string name = obj.GetComponent<ChipGame>().chipname;
                    Debug.Log(buttonData.buttonName);
                    //  name=name.Replace("Number_","");


                    if (buttonData.buttonName == name)
                    {
                        ButtonDict.myDictionary.Remove(buttonData.buttonName);
                        characterTable.characterMoney.AddCash2(buttonData.currentChipsOnTop);
                        // Enable the button
                        Destroy(obj);
                    }

                }

                return;
            }
             animation.SetBool("clicked", true);
            table.SetBool("clicked", true);
            roulette.SetActive(false);

            if (ButtonDict.first == 0)
            {
                characterTable.currentTableActive.Value = false;
                StartCoroutine(disabletable());
                ButtonDict.first = 1;
                return;
            }

           
            buttonData.currentChipsOnTop = buttonData.currentChipsOnTop + c.currentchipvalue - 1;
            if (ButtonDict.myDictionary.ContainsKey(buttonData.buttonName))
            {

                ButtonDict.myDictionary[buttonData.buttonName] = buttonData.currentChipsOnTop + 1;
            }
            else
            {
                ButtonDict.myDictionary.Add(buttonData.buttonName, buttonData.currentChipsOnTop + 1);
            }
            _interactableButton.InstantiateChip1(characterTable, buttonData);
        }


        public void Click2( int value)
        {
            if(!ButtonDict.buttonenable){
                return;
            }
            if (ButtonDict.betok)
            {
                return;
            }
            if (!_statusButton._isActive)
            {

                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Chip");

                // Loop through each object and look for a Button component
                foreach (GameObject obj in objectsWithTag)
                {
                    string name = obj.GetComponent<ChipGame>().chipname;
                    Debug.Log(buttonData.buttonName);
                    //  name=name.Replace("Number_","");


                    if (buttonData.buttonName == name)
                    {
                        ButtonDict.myDictionary.Remove(buttonData.buttonName);
                        characterTable.characterMoney.AddCash2(buttonData.currentChipsOnTop);
                        // Enable the button
                        Destroy(obj);
                    }

                }
                return;
            }




            audio.Play();

            animation.SetBool("clicked", true);
            table.SetBool("clicked", true);
            roulette.SetActive(false);

            if (ButtonDict.first == 0)
            {
                characterTable.currentTableActive.Value = false;
                StartCoroutine(disabletable());
                buttonData.currentOffset.y = 0f;
                ButtonDict.first = 1;
                return;

            }
            buttonData.currentChipsOnTop=value-c.currentchipvalue;

            buttonData.currentChipsOnTop = buttonData.currentChipsOnTop + c.currentchipvalue - 1;
            if (ButtonDict.myDictionary.ContainsKey(buttonData.buttonName))
            {

                ButtonDict.myDictionary[buttonData.buttonName] = buttonData.currentChipsOnTop + 1;
            }
            else
            {
                ButtonDict.myDictionary.Add(buttonData.buttonName, buttonData.currentChipsOnTop + 1);
            }


            _interactableButton.InstantiateChip2(characterTable, buttonData);

        }


        IEnumerator disabletable()
        {
            yield return new WaitForSeconds(1.5f);
            characterTable.currentTableActive.Value = true;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _longPress.SetPointerDown(true);
        }

        private void Update()
        {
                _longPress.LongPressCheck(characterTable, buttonData,this.gameObject);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _longPress.LongPress(characterTable, buttonData, false);
            _longPress.ResetPointer();
             if(characterTable.characterMoney.characterMoney.Value>=ButtonDict.currentchipvalue){
            Click();
             }
        }
    }
}
