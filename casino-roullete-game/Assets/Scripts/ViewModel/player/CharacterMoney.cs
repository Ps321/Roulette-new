using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Commands;
using Controllers;

namespace ViewModel
{
    [CreateAssetMenu(fileName = "New Character Money", menuName = "Scriptable/Character Money")]
    public class CharacterMoney : ScriptableObject
    {
        public IntReactiveProperty characterBet = new IntReactiveProperty();
        public IntReactiveProperty characterMoney = new IntReactiveProperty();
        public IntReactiveProperty currentPayment = new IntReactiveProperty();
        public IntReactiveProperty currentWinnerValue = new IntReactiveProperty();

        // Operations in player money
        void AddCash(int cashWinner)
        {
            int aux = characterMoney.Value;
            
            //characterMoney.Value += cashWinner;
           
        }
        public int getmoney(){
            return characterMoney.Value;
        }
         public void AddCash1(int cashWinner)
        {
            int aux = characterMoney.Value;
            
            characterMoney.Value += cashWinner;
            ButtonDict.winnerval=0;
            // currentWinnerValue.Value-=cashWinner;
            ButtonDict.updatekro=true;
           
        }
        public void AddCash2(int cashWinner)
        {
            characterMoney.Value += cashWinner;
        }
        
        void SubstractCash(int cashLost)
        {
            if(cashLost < 0) 
            {
                cashLost = cashLost * -1;
            }

            characterMoney.Value -= cashLost;

            if (characterMoney.Value < 0)
            {
                characterMoney.Value = 0;
            }
        }

        // Operations in player bet
        void AddBet(int betSum)
        {
            int aux = characterBet.Value - betSum;
            characterBet.Value  += betSum;
        }
        void SubstractBet(int betRest)
        {
            int aux = characterBet.Value ;
            characterBet.Value  -= betRest;
        }

        // Public methods
        public bool CheckBetValue(int valueFicha)
        {
            // Check if the bet is possible
            bool aux = true;
            if (valueFicha <= characterMoney.Value  && valueFicha != 0)
            {
                aux = true;
                SubstractCash(valueFicha);
                AddBet(valueFicha);
            }
            else
            {
                aux = false;
            }
            return aux;
        }
        public void DeleteChip(int valueFicha)
        {
            // Delete ficha of the table
            SubstractBet(valueFicha);
            AddCash(valueFicha);
        }
        public void PaymentSystem(int payment)
        {      
            characterBet.Value = 0;

            // If the player win when the round finish game will pay.
            // If not win it will stay with the same money without the bet.
            if(payment > 0){
                AddCash(payment);
                

            ButtonDict.winnerval+=payment;
            ButtonDict.updatekro=true;
            currentWinnerValue.Value=ButtonDict.winnerval;
             }
              Debug.Log($"Character player money is now being refresh with payment {payment}!");
        }
    }
}
