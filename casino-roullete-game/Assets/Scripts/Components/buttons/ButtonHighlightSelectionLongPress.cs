using UnityEngine;
using ViewModel;
using UnityEngine.UI;

namespace Commands
{
    public class ButtonHighlightSelectionLongPress : MonoBehaviour, ILongPress
    {
        [Tooltip ("Hold duration in seconds")]
        [Range (0.1f, 5f)] public float holdDuration = 0.5f ;
        public Button button;

        private bool isPointerDown = false ;
        private CharacterTable cc;
      
        private bool isLongPressed = false ;
        private float elapsedTime = 0f;
        private GameObject gg;
        
        public void SetPointerDown(bool value)
        {
            isPointerDown = value;
        }
        public void ResetPointer()
        {
            isPointerDown = false;
            isLongPressed = false;
            elapsedTime = 0f ;
        }
        public void LongPressCheck(CharacterTable characterTable, ButtonTable buttonData,GameObject btn)
        {
            if (isPointerDown && !isLongPressed)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= holdDuration)
                {
                    gg=btn;
                    cc=characterTable;
                    isLongPressed = true;
                    elapsedTime = 0f;
                    if (button.interactable)
                    {
                        LongPress(characterTable, buttonData, true);
                    }
                }
            }
        }
    public float timeBetweenFunctionCalls = 0.05f;
        private float timer = 0.0f;
  
  
     private void FixedUpdate() {
            timer += Time.fixedDeltaTime;
            if(isLongPressed && timer >= timeBetweenFunctionCalls){
                timer=0.0f;
                if(cc.characterMoney.characterMoney.Value>=ButtonDict.currentchipvalue){
                    gg.GetComponent<ButtonTableInput>().Click();
                }
                else{
                    Debug.Log("nhi hoga tujhse");
                }
                
            }
        }
        public void LongPress(CharacterTable characterTable, ButtonTable buttonData, bool currentStatus)
        {


            LongPress longPress = new LongPress()
            {
                isPressed = currentStatus,
                values = buttonData.buttonValue
            };

            characterTable.OnPressedButton.OnNext(longPress);
        }
    }
}
