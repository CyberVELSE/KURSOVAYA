using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Turn { Player, Bot }; // Перечисление для отслеживания очереди хода

    public Turn currentTurn; // Переменная для отслеживания текущего хода

    public Bot bot; // Ссылка на объект бота

    public GameObject attackButton; // Ссылка на объект кнопки атаки

    private bool isClickable = true; // Переменная для отслеживания кликабельности кнопки



    void Start()
    {
        // Находим объект AttackButton и получаем ссылку на его компонент AttackButton
        AttackButton attackButton = FindObjectOfType<AttackButton>();

        currentTurn = Turn.Player; // Начинаем с хода игрока

        // Подписываемся на событие onClick
        attackButton.onClick.AddListener(OnAttackButtonClicked);
    }

    public void EndTurn()
    {
        if (currentTurn == Turn.Player)
        {
            currentTurn = Turn.Bot; // Если ходил игрок, переходим к ходу бота
            StartCoroutine(BotTurn()); // Запускаем ход бота
        }
        else
        {
            currentTurn = Turn.Player; // Если ходил бот, переходим к ходу игрока

            AttackButton attackButton = FindObjectOfType<AttackButton>();// Находим объект AttackButton
            attackButton.SetClickable(true); ; // Разблокируем взаимодействие с кнопкой атаки
        }
    }

    // Метод, вызываемый при нажатии на кнопку атаки
    private void OnAttackButtonClicked()
    {
        // Находим объект AttackButton
        AttackButton attackButton = FindObjectOfType<AttackButton>();
        // Блокируем взаимодействие с кнопкой атаки
        attackButton.SetClickable(false);
    }

    IEnumerator BotTurn()
    {

        bot.MakeMove(); // Вызываем функцию хода бота

        yield return new WaitForSeconds(2f); // Задержка для эффекта ожидания
        
        EndTurn(); // Заканчиваем ход после хода бота
    }
}
