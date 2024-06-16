using UnityEngine;

public class CardSpawn : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn; // Префаб объекта, который мы будем спавнить
    [SerializeField] private Vector3[] spawnPositions; // Массив начальных координат для каждой карты
    [SerializeField] private Vector3[] targetPositions; // Массив целевых координат для каждой карты
    [SerializeField] private float delayBetweenCards = 1f; // Задержка между появлением каждой карты
    [SerializeField] private CardAttributes[] cardAttributes;

    public bool[] ocupied = new bool[4];

    void Start()
    {
        for(int i = 0; i < ocupied.Length; i++)
        { 
            ocupied[i] = false;
        }
        Spawn(spawnPositions, targetPositions, cardAttributes);
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

    public bool Spawn(CardAttributes attributes)
    {
        bool success = false;
        int i = 0;

        for(int j = 0; j < ocupied.Length; j++)
        {
            if (!ocupied[j])
            {
                success = true;
                i = j;
                break;
            }
        }

        if (!success)
            return false;

        // Создаем объект на текущих координатах
        GameObject spawnedCard = Instantiate(objectToSpawn, spawnPositions[i], Quaternion.identity);
        SetCardAttributes(spawnedCard, attributes); // Присваиваем атрибуты каждой карте
        spawnedCard.GetComponent<CardMovement>().SetCardNumber(i);

        // Получаем компонент CardMovement из созданной карты
        CardMovement cardMovement = spawnedCard.GetComponent<CardMovement>();

        // Применяем уникальное значение вращения к карте
        spawnedCard.transform.eulerAngles = Vector3.zero;
        ocupied[i] = true;

        HoverEffect hoverEffect = spawnedCard.GetComponent<HoverEffect>();
        if (hoverEffect != null)
        {
            hoverEffect.targetIndex = i;
            hoverEffect.SetTargetPosition(targetPositions[i]);
        }
        else
        {
            Debug.LogError("HoverEffect script is missing on the spawned card object.");
        }

        return true;
    }

    private void Spawn(Vector3[] spawnPositions, Vector3[] targetPostions, CardAttributes[] cardAttributes)
{
        // Проверяем, что у нас есть достаточное количество начальных и целевых позиций
        if (spawnPositions.Length != targetPositions.Length || spawnPositions.Length != cardAttributes.Length)
        {
            Debug.LogError("Invalid setup: Spawn positions, target positions, card rotations, and card attributes should have the same length.");
            return;
        }

        // Проходим по каждой позиции и создаем карту на ней
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            // Создаем объект на текущих координатах
            GameObject spawnedCard = Instantiate(objectToSpawn, spawnPositions[i], Quaternion.identity);
            SetCardAttributes(spawnedCard, cardAttributes[i]); // Присваиваем атрибуты каждой карте
            spawnedCard.GetComponent<CardMovement>().SetCardNumber(i); 

            // Получаем компонент CardMovement из созданной карты
            CardMovement cardMovement = spawnedCard.GetComponent<CardMovement>();

            // Применяем уникальное значение вращения к карте
            spawnedCard.transform.eulerAngles = Vector3.zero;
            ocupied[i] = true;

            HoverEffect hoverEffect = spawnedCard.GetComponent<HoverEffect>();
            if (hoverEffect != null)
            {
                hoverEffect.targetIndex = i;
                hoverEffect.SetTargetPosition(targetPositions[i]);
            }
            else
            {
                Debug.LogError("HoverEffect script is missing on the spawned card object.");
            }
        }
     }
}