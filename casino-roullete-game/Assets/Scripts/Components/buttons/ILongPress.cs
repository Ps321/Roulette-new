using ViewModel;
using UnityEngine;

namespace Commands
{
    public  interface ILongPress 
    {
        void SetPointerDown(bool value);
        void LongPressCheck(CharacterTable characterTable, ButtonTable buttonData,GameObject btn);
        void ResetPointer();
        void LongPress(CharacterTable characterTable, ButtonTable buttonData, bool currentStatus);
    }
}
