using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


namespace toqurqksProductions
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        //CardElements 
        public string cardName;
        public CardClass cardClass;
        public List<CardType> cardType;
        public CharacterType1 characterType1;
        public CharacterType2 characterType2;
        public CharacterType3 characterType3;
        public CharacterType4 characterType4;
        public int Durability;
        public int damage;
        public int CardScore;
        public GameObject prefab;
        public int range;
        public AttackPattern attackPattern;
        public PriorityTarget priorityTarget;


        //CardEffect 아직 미완성
        public string CardEffect;
        
        public enum CardClass
        {
            Bronze,
            Silver,
            Gold
        }


        public enum CardType
        {
            Unit,
            Tower,
            Action
        }
        public enum CharacterType1
        {
             SteamPunk,
             CyberPunk
        }
        public enum CharacterType2
        {
            Inanimate,
            Virtual
        }

        public enum CharacterType3
        {
            Machine,
            Concept
        }

        public enum CharacterType4
        {
            Infantry,
            Pronunciation,
            Weapon,
            Politics,
            Tactic
        }

        public enum AttackPattern
        {
            Single,
            Multitarget,
            Cross,
            Column,
            Row,
            TwoByTwo,
            FourByFour
        }

        public enum PriorityTarget
        {
            Close,
            Far,
            LeastCurrentHealth,
            MostCurrentHealth,
            MostMaxHealth,
            MostDamage
        }




    }

}
