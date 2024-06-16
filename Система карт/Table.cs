using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, ITable
{
    public List<int> LineDamages => CalculateDamage();

    public TableType TableType { get; private set; }

    public int capacity { get; private set; }

    private List<ICardSlot> _slots;

    public Table(TableType tableType, int capacity)
    {
        this.TableType = tableType;
        this.capacity = capacity;

        _slots = new List<ICardSlot>(capacity);

        for(int i = 0; i < capacity; i++)
        {
            _slots.Add(new CardSlot());
        }
    }

    private List<int> CalculateDamage()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(List<int> LineDamages)
    {
        throw new System.NotImplementedException();
    }

    public bool TryToPlaceCard(ICardTable card, int slotNumber)
    {
        if (slotNumber > capacity || slotNumber < 0)
        {
            Debug.Log($"Invalid card slot number: {slotNumber}");
            return false;
        }

        if (card == null)
        {
            Debug.Log($"Failed to place card, can't operate with null values");
            return false;
        }

        return _slots[slotNumber].SetCard(card);
    }
    
}
