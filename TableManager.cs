using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [SerializeField] private CardAttributes cardAttribute;
    [SerializeField] private FlagmanAttributes flagmanAttributes;
    public static TableManager instance; // �������� ���������, ��� TableManager ���� � ����.

    public readonly Dictionary<int, CardAttributes> cardsOnTable = new Dictionary<int, CardAttributes>(); // ������� ��� �������� ���� �� �����
    public Dictionary<int, int> cardHealth = new Dictionary<int, int>();
    public Dictionary<int, GameObject> cardPrefabs = new Dictionary<int, GameObject>();

    [SerializeField] private AudioClip ultimateButtonSound; //���� ������� �� ������

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ����� ��� ���������� ����� �� ���� � ��������� ������
    public void AddCardToTable(int cellIndex, CardAttributes card)
    {
        if (!cardsOnTable.ContainsKey(cellIndex)) // ���������, ���� �� ��� ����� � ���� ������
        {
            cardsOnTable.Add(cellIndex, card); // ��������� ����� � ��������� ������
            cardHealth.Add(cellIndex, card.health);
            Debug.Log($"Card {cardsOnTable[cellIndex].name} added");
        }
        else
        {
            cardsOnTable[cellIndex] = card;
            cardHealth[cellIndex] = card.health;
        }
    }

    // ����� ��� ������������� ���������� ��������
    public void UseUltimate()
    {
        var flagman = flagmanAttributes;

        if (flagman.currentUltimateCharge == 100)
        {

            flagman.currentUltimateCharge = 0;
            Bot tb = GameObject.FindObjectOfType<Bot>();
            int damage = 5; // ���� �� ������ ������ ����������

            for (int i = 0; i <= 7; i++)
            {
                if (tb.cardsOnTable.ContainsKey(i)) // ���������, ���� �� � ������ i �����
                {
                    tb.cardHealth[i] -= damage;
                    tb.cardPrefabs[i].GetComponentInChildren<CardDisplay>().UpdateHealth(tb.cardHealth[i]);
                }
            }
            tb.DeleteDestroyedCards();
            Debug.Log("� ���� ��������������!");

            //���� ������� ������ ����������
            SoundManager.Instance.PlaySoundEffect(ultimateButtonSound);
        }
    }

    // ����� ��� ���������� ����� � ��������
    public void ApplyDamageToFlagship(GameObject flagship)
    {
        int totalDamage = 0; // ����� ����

        Bot tb = flagship.GetComponent<Bot>();

        if (tb == null)
        {
            Debug.Log("�� ������� ����� ��������� Bot");
            return;
        }


        // ��������� ���� �� ���� ������ �� ����� ����
        for (int i = 0; i <= 3; i++)
        {
            // ��������� ���� � ������: ��������� ����� ����� � ������� i � ������� i+4 
            int damageInLine = (cardsOnTable.TryGetValue(i, out var c) ? cardsOnTable[i].attack:0) + (cardsOnTable.TryGetValue(i+4, out var c1) ? cardsOnTable[i+4].attack : 0);

            // ���������, ���� �� ����� �� ������� i
            if (tb.cardsOnTable.ContainsKey(i))
            {
                tb.cardHealth[i] -= damageInLine;
                tb.cardPrefabs[i].GetComponentInChildren<CardDisplay>().UpdateHealth(tb.cardHealth[i]);
            }
            // ���� �� ������� i ��� �����, ��������� ������� i+4
            else if (tb.cardsOnTable.ContainsKey(i+4))
            {
                tb.cardHealth[i+4] -= damageInLine;
                tb.cardPrefabs[i+4].GetComponentInChildren<CardDisplay>().UpdateHealth(tb.cardHealth[i+4]);
            }
            // ���� �� �������� i � i+4 ��� ����, ��������� ���� � ������ �����
            else
                totalDamage += damageInLine;
        }

        tb.DeleteDestroyedCards();


        if (flagship != null)
        {
            // ��������� ���� � ��������
            flagship.GetComponent<FlagmanAttributes>().TakeDamage(totalDamage);

            //�������
            Debug.Log(flagship.GetComponent<FlagmanAttributes>().currentHealth);
        }
        else
        {
            Debug.LogWarning("Flagship object is not assigned!");
        }
    }

    public void DeleteDestroyedCards()
    {
        List<int> cardKeysToDelete = new List<int>();

        // ��������� �������� ������ ����� �� �����
        foreach (var el in cardsOnTable)
        {
            if (cardHealth[el.Key] <= 0)
            {
                cardKeysToDelete.Add(el.Key);
            }
        }

        // ������� ����� �� ��������
        foreach (var i in cardKeysToDelete)
        {
            cardsOnTable.Remove(i);
            cardHealth.Remove(i);
        }
    }
}
