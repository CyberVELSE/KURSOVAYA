using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public GameObject flagship; // Ссылка на объект флагмана врага
    public TableManager tableManager; // Ссылка на TableManager

    public GameManager gameManager; // Ссылка на объект GameManager
    public UnityEvent onClick; // Событие, которое будет вызываться при нажатии на кнопку
    private bool isClickable = true; // Переменная для отслеживания кликабельности кнопки

    public int turnCount = 1; // Счетчик ходов игрока
    public GameObject prefab; // Ссылка на префаб торгового корабля, который должен появиться на 3-й ход
    private GameObject spawnedPrefab; // Ссылка на инстанцированный префаб

    [SerializeField] private AudioClip attackButtonSound; //Звук нажатия на кнопку
    [SerializeField] private Text _timeText;



    // Метод, вызываемый при нажатии на кнопку атаки
    private void OnMouseDown()
    {
        if ((isClickable && Input.GetMouseButtonDown(0))) // Проверяем, была ли нажата левая кнопка мыши
        {
            // Вызываем событие при нажатии на кнопку
            onClick.Invoke();
            //Звук нажатия кнопки
            SoundManager.Instance.PlaySoundEffect(attackButtonSound);

            if (gameManager != null && tableManager != null && flagship != null)
            {

                if (turnCount == 0)
                {
                    Destroy(spawnedPrefab);
                    spawnedPrefab = null; // Обнуляем ссылку после удаления
                    isClickable = true;
                }
                else
                {
                    if (turnCount == 3)
                    {
                        // Создаем поворот на 90 градусов по оси Y
                        Quaternion rotation = Quaternion.Euler(0, 90, 0);

                        //На третий ход появляется торговый корабль
                        spawnedPrefab = Instantiate(prefab, new Vector3(0.23f, 34.2f, -17.77f), rotation);
                        spawnedPrefab.GetComponent<TraydingShip>().AddCardsToMarket();
                        turnCount = -1;
                    }
                    // Вызываем метод для применения урона к флагману
                    tableManager.ApplyDamageToFlagship(flagship);
                    GameObject.Find("TitaniumPlayer").GetComponent<FlagmanAttributes>().ChargeUltimate(20); //Пополняем заряд луьтимейта для игрока
                    GameObject.FindObjectOfType<CoinManager>().GetComponent<CoinManager>().AddCoin();
                    // Завершаем ход игрока и передаем ход боту
                    gameManager.EndTurn();
                }
                turnCount++;
            }
            else
            {
                Debug.LogError("TableManager or flagship is not assigned!");
            }
        }
    }

    // Метод для установки значения isClickable
    public void SetClickable(bool clickable)
    {
        isClickable = clickable;
    }

}
