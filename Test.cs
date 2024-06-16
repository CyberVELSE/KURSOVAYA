//Рабочая версия CardSpawn


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CardSpawn : MonoBehaviour
//{
//    [SerializeField] private GameObject objectToSpawn; // Префаб объекта, который мы будем спавнить
//    [SerializeField] private Vector3[] spawnPositions; // Массив начальных координат для каждой карты
//    [SerializeField] private Vector3[] targetPositions; // Массив целевых координат для каждой карты
//    [SerializeField] private float delayBetweenCards = 1f; // Задержка между появлением каждой карты
//    [SerializeField] private CardAttributes[] cardAttributes;



//    void Start()
//    {
//        // Проверяем, что у нас есть достаточное количество начальных и целевых позиций
//        if (spawnPositions.Length != targetPositions.Length || spawnPositions.Length != cardAttributes.Length)
//        {
//            Debug.LogError("Invalid setup: Spawn positions, target positions, and card attributes should have the same length.");
//            return;
//        }

//        // Проходим по каждой позиции и создаем карту на ней
//        for (int i = 0; i < spawnPositions.Length; i++)
//        {
//            // Создаем объект на текущих координатах
//            GameObject spawnedCard = Instantiate(objectToSpawn, spawnPositions[i], Quaternion.identity);
//            SetCardAttributes(spawnedCard, cardAttributes[i]); // Присваиваем атрибуты каждой карте

//            HoverEffect hoverEffect = spawnedCard.GetComponent<HoverEffect>();
//            if (hoverEffect != null)
//            {
//                hoverEffect.targetIndex = i;
//                hoverEffect.SetTargetPosition(targetPositions[i]);
//            }
//            else
//            {
//                Debug.LogError("HoverEffect script is missing on the spawned card object.");
//            }
//            // Запускаем корутину, которая переместит объект через указанную задержку
//            //StartCoroutine(MoveObjectAfterDelay(spawnedCard, targetPositions[i], delayBetweenCards * i));
//        }

//    }

//    private void SetCardAttributes(GameObject cardObject, CardAttributes attributes)
//    {
//        CardDisplay cardDisplay = cardObject.GetComponentInChildren<CardDisplay>(); // Получаем скрипт управления картой
//        if (cardDisplay != null)
//        {
//            cardDisplay.SetAttributes(attributes); // Присваиваем атрибуты карте
//        }
//        else
//        {
//            Debug.LogError("CardDisplay script is missing on the card object.");
//        }
//    }

//    IEnumerator MoveObjectAfterDelay(GameObject obj, Vector3 target, float delay)
//    {
//        // Ждем указанное количество секунд
//        yield return new WaitForSeconds(delay);
//        // Перемещаем объект на целевые координаты
//        obj.transform.position = target;
//    }
//}
