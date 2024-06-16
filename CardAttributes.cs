using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
[System.Serializable]
public class CardAttributes : ScriptableObject
{
    public string cardName;
    public string description;

    public Sprite artwork;

    public int energeCost;
    public int health;
    public int attack;
    public int cost;


    //// Очищаем другие характеристики, если это необходимо
    //public void ClearData()
    //{
    //    cardName = "";
    //    description = "";
    //    artwork = null;
    //    energeCost = 0;
    //    health = 0;
    //    attack = 0;
    //}

    //// Метод для применения урона к цели
    //public void ApplyDamage(GameObject target)
    //{
    //    // Проверяем, является ли цель флагманом
    //    FlagmanAttributes flagship = target.GetComponent<FlagmanAttributes>();
    //    if (flagship != null)
    //    {
    //        // Применяем урон к флагману
    //        flagship.TakeDamage(attack);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Target is not a flagship!");
    //    }
    //}
}
