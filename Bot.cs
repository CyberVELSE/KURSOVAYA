using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private List<CardAttributes> cardsOnHand = new List<CardAttributes>(); //������ ����, ������� ��� ������ � ����.
    [SerializeField] private GameObject[] _cells = new GameObject[8]; //������ ����� �� ������� ����, ���� ��� ����� ��������� �����
    [SerializeField] private GameObject cardPrefab; //������ �����
    [SerializeField] private GameObject _player; //������ �� ������ ������
    public readonly Dictionary<int, CardAttributes> cardsOnTable = new Dictionary<int, CardAttributes>(); //������� (������, �����)
    public Dictionary<int, int> cardHealth = new Dictionary<int, int>(); //������� (������, �������� �����)
    public readonly Dictionary<int, GameObject> cardPrefabs = new Dictionary<int, GameObject>(); // ������� (������, ������ �����)

    public void MakeMove()
    {
        // ���� ��� ��������� �����, ���������� ���
        if (cardsOnTable.Count == 8)
            return;


        // ��� �������� ��������� ����� �� ���� � ��������� ������ �� ����
        bool hasChosen = false;
        GameObject randomCell = null;
        var randomCard = cardsOnHand[Random.Range(0, cardsOnHand.Count)];

        while (!hasChosen) // ���� ������������, ���� �� ����� ������� ���������� ������
        {
            if (cardsOnTable.Count == 8)
                break;

            int rand = Random.Range(0, _cells.Length);

            if (cardsOnTable.ContainsKey(rand)) // ���� ������ ��� ������ ������, ���������� ������
                continue;
            else
            {
                randomCell = _cells[rand]; // ���� ������ ��������, �������� �� � ������� �� �����
                hasChosen = true;
            }
        }

        if (cardsOnTable.ContainsKey(randomCell.GetComponent<CellNumber>().cellNumber))
        {
            cardsOnTable[randomCell.GetComponent<CellNumber>().cellNumber] = randomCard;
            cardHealth[randomCell.GetComponent<CellNumber>().cellNumber] = randomCard.health;
        }
        else
        {
            cardsOnTable.Add(randomCell.GetComponent<CellNumber>().cellNumber, randomCard);// ��������� ����� �� ����
            cardHealth.Add(randomCell.GetComponent<CellNumber>().cellNumber, randomCard.health); // ��������� �������� �����
        }

        var card = Instantiate(cardPrefab, randomCell.transform.position, Quaternion.identity); //������� ������ �� �����
        SetCardAttributes(card, randomCard); // ������������� �������� �����

        if (cardPrefabs.ContainsKey(randomCell.GetComponent<CellNumber>().cellNumber))
            cardPrefabs[randomCell.GetComponent<CellNumber>().cellNumber] = card;
        else
            cardPrefabs.Add(randomCell.GetComponent<CellNumber>().cellNumber, card);


        Debug.Log("��� ���� ��������");
        ApplyDamageToFlagship(_player); // ��������� ���� � �������� ������
    }

    private void SetCardAttributes(GameObject cardObject, CardAttributes attributes)
    {
        CardDisplay cardDisplay = cardObject.GetComponentInChildren<CardDisplay>(); // �������� ������ ���������� ������
        if (cardDisplay != null)
        {
            cardDisplay.SetAttributes(attributes); // ����������� �������� �����
        }
        else
        {
            Debug.LogError("CardDisplay script is missing on the card object.");
        }
    }

    // ����� ��� ������������� ����������
    private void UseUltimate()
    {
        var flagman = GetComponent<FlagmanAttributes>();

        if (flagman.currentUltimateCharge == 100)
        {
            //���� ������ �� ���� ��������� 5 �����.
            flagman.currentUltimateCharge = 0;
            TableManager tb = GameObject.FindAnyObjectByType<TableManager>();
            int damage = 5; 

            for (int i =0;i<=7;i++)
            {
                if (tb.cardsOnTable.ContainsKey(i))
                {
                    tb.cardHealth[i] -= damage;
                    tb.cardPrefabs[i].GetComponentInChildren<CardDisplay>().UpdateHealth(tb.cardHealth[i]);
                }
            }
            tb.DeleteDestroyedCards(); //����� � ������� ��� ������������� ��������� ��������� � ����.
            Debug.Log("���������!!!");
        }
    }

    // ���� �������������� �� ������ ����, ����������� �� ����.
    public void ApplyDamageToFlagship(GameObject flagship)
    {
        int totalDamage = 0;

        TableManager tb = GameObject.FindAnyObjectByType<TableManager>();

        if (tb == null)
        {
            Debug.Log("�� ������� ����� ��������� TableManager");
            return;
        }

        

        for (int i = 0; i <= 3; i++)
        {

            int damageInLine = (cardsOnTable.TryGetValue(i, out var c) ? cardsOnTable[i].attack : 0) + (cardsOnTable.TryGetValue(i+4, out var c1) ? cardsOnTable[i + 4].attack : 0);

            if (tb.cardsOnTable.ContainsKey(i+4))
            {
                tb.cardHealth[i + 4] -= damageInLine;
                tb.cardPrefabs[i + 4].GetComponentInChildren<CardDisplay>().UpdateHealth(tb.cardHealth[i + 4]);
            }
            else if (tb.cardsOnTable.ContainsKey(i))
            {
                tb.cardHealth[i] -= damageInLine;
                tb.cardPrefabs[i].GetComponentInChildren<CardDisplay>().UpdateHealth(tb.cardHealth[i]);
            }
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

        foreach (var el in cardsOnTable)
        {
            if (cardHealth[el.Key] <= 0)
            {
                cardKeysToDelete.Add(el.Key);
            }
        }

        foreach (var i in cardKeysToDelete)
        {
            GameObject.FindObjectOfType<CoinManager>().GetComponent<CoinManager>().AddCoin();
            cardsOnTable.Remove(i);
            cardHealth.Remove(i);
        }
    }
}

