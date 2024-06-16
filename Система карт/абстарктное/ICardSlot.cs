using System;

public interface ICardSlot
{
    int Number { get; set; }
    bool IsEmpty { get; }
    ICardTable card { get; }
    Type cardType { get; }
    bool SetCard(ICardTable card);
    void Clear();
}
