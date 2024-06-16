using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private List<CardAttributes> cardsOnHand = new List<CardAttributes>(); //Список карт, которые бот держит в руке.
    [SerializeField] private GameObject[] _cells = new GameObject[8]; //Массив ячеек на игровом поле, куда бот может размещать карты
    [SerializeField] private GameObject cardPrefab; //Префаб карты
    [SerializeField] private GameObject _player; //Ссылка на объект игрока
    public readonly Dictionary<int, CardAttributes> cardsOnTable = new Dictionary<int, CardAttributes>(); //Словарь (ячейка, карта)
    public Dictionary<int, int> cardHealth = new Dictionary<int, int>(); //Словарь (ячейка, здоровье карты)
    public readonly Dictionary<int, GameObject> cardPrefabs = new Dictionary<int, GameObject>(); // Словарь (ячейка, префаб карты)

    public void MakeMove()
    {
        // Если нет свободных ячеек, пропускаем ход
        if (cardsOnTable.Count == 8)
            return;


        // Бот выбирает случайную карту из руки и случайную ячейку на поле
        bool hasChosen = false;
        GameObject randomCell = null;
        var randomCard = cardsOnHand[Random.Range(0, cardsOnHand.Count)];

        while (!hasChosen) // Цикл продолжается, пока не будет выбрана подходящая клетка
        {
            if (cardsOnTable.Count == 8)
                break;

            int rand = Random.Range(0, _cells.Length);

            if (cardsOnTable.ContainsKey(rand)) // Если клетка уже занята картой, продолжаем искать
                continue;
            else
            {
                randomCell = _cells[rand]; // Если клетка свободна, выбираем ее и выходим из цикла
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
            cardsOnTable.Add(randomCell.GetComponent<CellNumber>().cellNumber, randomCard);// Добавляем карту на стол
            cardHealth.Add(randomCell.GetComponent<CellNumber>().cellNumber, randomCard.health); // Добавляем здоровье карты
        }

        var card = Instantiate(cardPrefab, randomCell.transform.position, Quaternion.identity); //Спавним префаб на столе
        SetCardAttributes(card, randomCard); // Устанавливаем атрибуты карты

        if (cardPrefabs.ContainsKey(randomCell.GetComponent<CellNumber>().cellNumber))
            cardPrefabs[randomCell.GetComponent<CellNumber>().cellNumber] = card;
        else
            cardPrefabs.Add(randomCell.GetComponent<CellNumber>().cellNumber, card);


        Debug.Log("Ход бота выполнен");
        ApplyDamageToFlagship(_player); // Применяем урон к флагману игрока
    }

    private void SetCardAttributes(GameObject cardObject, CardAttributes attributes)
    {
        CardDisplay cardDisplay = cardObject.GetComponentInChildren<CardDisplay>(); // Получаем скрипт управления картой
        if (cardDisplay != null)
        {
            cardDisplay.SetAttributes(attributes); // Присваиваем атрибуты карте
        }
        else
        {
            Debug.LogError("CardDisplay script is missing on the card object.");
        }
    }

    // Метод для использования Ультимейта
    private void UseUltimate()
    {
        var flagman = GetComponent<FlagmanAttributes>();

        if (flagman.currentUltimateCharge == 100)
        {
            //Всем картам на поле наносится 5 урона.
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
            tb.DeleteDestroyedCards(); //Карты с нулевым или отрицательным здоровьем удаляются с поля.
            Debug.Log("ЗАВОЕВАТЬ!!!");
        }
    }

    // Урон рассчитывается на основе карт, размещенных на поле.
    public void ApplyDamageToFlagship(GameObject flagship)
    {
        int totalDamage = 0;

        TableManager tb = GameObject.FindAnyObjectByType<TableManager>();

        if (tb == null)
        {
            Debug.Log("Не удалось найти компонент TableManager");
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
            // Применяем урон к флагману
            flagship.GetComponent<FlagmanAttributes>().TakeDamage(totalDamage);

            //удалить
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

