using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public bool isPlaced;

    // Заданные значения вращения
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0); //Смещение карты от стола
    private Grab cardGrab;
    private int cardNumber;


    private void Start()
    {
        isPlaced = false;
        cardGrab = GetComponent<Grab>();
    }

    public void SetCardNumber(int cardNumber)
    {
        this.cardNumber = cardNumber;
    }

    void Update()
    {
        // Проверяем нажатие левой кнопки мыши
        if (!isPlaced && cardGrab.isReady && Input.GetMouseButtonDown(0))
        {
            // Создаем луч от камеры к точке нажатия мыши
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверяем, попал ли луч на объект с тегом "Cell"
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Cell"))
            {
                GameObject cell = hit.collider.gameObject;
                // Получаем позицию ячейки, на которую было произведено нажатие
                Vector3 targetPosition = hit.collider.transform.position;

                // Перемещаем карту к выбранной ячейке и применяем вращение только для этой карты
                MoveCardToPosition(targetPosition);
                AddCardToTableDictionary(cell, gameObject.GetComponentInChildren<CardDisplay>().cardAttribute);
                cardGrab.isReady = false;
                isPlaced=true;
                FindObjectOfType<CardSpawn>().ocupied[cardNumber] = false;
            }
        }
    }

    // Метод для перемещения указанной карты к указанной позиции
    private void MoveCardToPosition(Vector3 targetPosition)
    {
        // Устанавливаем позицию карты на целевую позицию
        transform.position = targetPosition + offset;

        // Устанавливаем вращение карты на заданные значения
        transform.eulerAngles = targetRotation;
    }

    private void AddCardToTableDictionary(GameObject chosenCell, CardAttributes cardAttributes)
    {
        if (cardAttributes == null || chosenCell == null)
            return;

        // Поиск первого объекта типа TableManager в сцене и получение его компонента TableManager
        var tm = GameObject.FindFirstObjectByType<TableManager>().GetComponent<TableManager>();
        // Получение номера ячейки от выбранного объекта ячейки (chosenCell)
        int cellNumber = chosenCell.GetComponent<CellNumber>().cellNumber;

        tm.AddCardToTable(cellNumber, cardAttributes);

        if (tm.cardPrefabs.ContainsKey(cellNumber)) // Проверка, содержит ли словарь cardPrefabs ключ с номером ячейки
            tm.cardPrefabs[cellNumber] = gameObject; // Если ключ существует, обновление значения (игрового объекта) для данного ключа
        else
            tm.cardPrefabs.Add(cellNumber, gameObject); // Если ключа нет, добавление нового элемента в словарь
    }
}