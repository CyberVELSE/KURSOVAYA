using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    // Ссылка на текстовое поле, где будет отображаться количество монет
    [SerializeField] private Text coinText;

    // Счетчик монет
    public int coinCount {  get; private set; }

    // Метод для добавления монетки
    public void AddCoin()
    {
        coinCount++; // Увеличиваем количество монеток на 1

        coinText.text = coinCount.ToString();
        Debug.Log("Ведьмаку заплатите чеканной монееетой");
    }

    public void RemoveCoin(int amount)
    {
        coinCount -= amount;
        coinText.text = coinCount.ToString();
    }

    void Start()
    {
        // Находим объект текстового поля по имени и присваиваем переменной coinText
        //coinText.text = coinText.ToString();
        coinCount = 0;
        AddCoin();
    }
}
