using System;
using UnityEngine;

public class CardSlot : MonoBehaviour, ICardSlot
{
    public bool IsEmpty => card == null;

    public ICardTable card { get; private set; }

    public Type cardType => card.type;

    public int Number { get; set; }

    public void Clear()
    {
        if(IsEmpty) 
            return;

        card = null;
        Debug.Log($"Slot {this.Number} has benn cleared");
    }

    public bool SetCard(ICardTable card)
    {
        if (!IsEmpty)
        {
            Debug.Log("Can't place card in occupied slot");
            return false;
        }

        this.card = card;
        Debug.Log($"Card {card.type} has been placed in slot {this.Number}");
        return true;
    }
}
