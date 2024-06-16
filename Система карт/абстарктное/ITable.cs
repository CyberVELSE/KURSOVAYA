using System.Collections.Generic;

public interface ITable
{
    List<int> LineDamages { get; }
    TableType TableType { get; }
    int capacity { get;}
    void TakeDamage(List<int> LineDamages);
    bool TryToPlaceCard(ICardTable card, int slotNumber);
}
