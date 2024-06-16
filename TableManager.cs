using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [SerializeField] private CardAttributes cardAttribute;
    [SerializeField] private FlagmanAttributes flagmanAttributes;
    public static TableManager instance; // Помогает убедиться, что TableManager один в игре.

    public readonly Dictionary<int, CardAttributes> cardsOnTable = new Dictionary<int, CardAttributes>(); // Словарь для хранения карт на столе
    public Dictionary<int, int> cardHealth = new Dictionary<int, int>();
    public Dictionary<int, GameObject> cardPrefabs = new Dictionary<int, GameObject>();

    [SerializeField] private AudioClip ultimateButtonSound; //Звук нажатия на кнопку

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

    // Метод для добавления карты на стол в указанную ячейку
    public void AddCardToTable(int cellIndex, CardAttributes card)
    {
        if (!cardsOnTable.ContainsKey(cellIndex)) // Проверяем, есть ли уже карта в этой ячейке
        {
            cardsOnTable.Add(cellIndex, card); // Добавляем карту в указанную ячейку
            cardHealth.Add(cellIndex, card.health);
            Debug.Log($"Card {cardsOnTable[cellIndex].name} added");
        }
        else
        {
            cardsOnTable[cellIndex] = card;
            cardHealth[cellIndex] = card.health;
        }
    }

    // Метод для использования ультимейта флагмана
    public void UseUltimate()
    {
        var flagman = flagmanAttributes;

        if (flagman.currentUltimateCharge == 100)
        {

            flagman.currentUltimateCharge = 0;
            Bot tb = GameObject.FindObjectOfType<Bot>();
            int damage = 5; // Урон по каждой ячейке противника

            for (int i = 0; i <= 7; i++)
            {
                if (tb.cardsOnTable.ContainsKey(i)) // Проверяем, есть ли в ячейке i карта
                {
                    tb.cardHealth[i] -= damage;
                    tb.cardPrefabs[i].GetComponentInChildren<CardDisplay>().UpdateHealth(tb.cardHealth[i]);
                }
            }
            tb.DeleteDestroyedCards();
            Debug.Log("Я сама неотвротимость!");

            //Звук нажатия кнопки Ультимейта
            SoundManager.Instance.PlaySoundEffect(ultimateButtonSound);
        }
    }

    // Метод для применения урона к флагману
    public void ApplyDamageToFlagship(GameObject flagship)
    {
        int totalDamage = 0; // Общий урон

        Bot tb = flagship.GetComponent<Bot>();

        if (tb == null)
        {
            Debug.Log("Не удалось найти компонент Bot");
            return;
        }


        // Применяем урон ко всем картам на столе бота
        for (int i = 0; i <= 3; i++)
        {
            // Вычисляем урон в строке: суммируем атаку карты в позиции i и позиции i+4 
            int damageInLine = (cardsOnTable.TryGetValue(i, out var c) ? cardsOnTable[i].attack:0) + (cardsOnTable.TryGetValue(i+4, out var c1) ? cardsOnTable[i+4].attack : 0);

            // Проверяем, есть ли карта на позиции i
            if (tb.cardsOnTable.ContainsKey(i))
            {
                tb.cardHealth[i] -= damageInLine;
                tb.cardPrefabs[i].GetComponentInChildren<CardDisplay>().UpdateHealth(tb.cardHealth[i]);
            }
            // Если на позиции i нет карты, проверяем позицию i+4
            else if (tb.cardsOnTable.ContainsKey(i+4))
            {
                tb.cardHealth[i+4] -= damageInLine;
                tb.cardPrefabs[i+4].GetComponentInChildren<CardDisplay>().UpdateHealth(tb.cardHealth[i+4]);
            }
            // Если на позициях i и i+4 нет карт, добавляем урон к общему урону
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

        // Проверяем здоровье каждой карты на столе
        foreach (var el in cardsOnTable)
        {
            if (cardHealth[el.Key] <= 0)
            {
                cardKeysToDelete.Add(el.Key);
            }
        }

        // Удаляем карты из словарей
        foreach (var i in cardKeysToDelete)
        {
            cardsOnTable.Remove(i);
            cardHealth.Remove(i);
        }
    }
}
