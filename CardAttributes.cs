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


    //// ������� ������ ��������������, ���� ��� ����������
    //public void ClearData()
    //{
    //    cardName = "";
    //    description = "";
    //    artwork = null;
    //    energeCost = 0;
    //    health = 0;
    //    attack = 0;
    //}

    //// ����� ��� ���������� ����� � ����
    //public void ApplyDamage(GameObject target)
    //{
    //    // ���������, �������� �� ���� ���������
    //    FlagmanAttributes flagship = target.GetComponent<FlagmanAttributes>();
    //    if (flagship != null)
    //    {
    //        // ��������� ���� � ��������
    //        flagship.TakeDamage(attack);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Target is not a flagship!");
    //    }
    //}
}
