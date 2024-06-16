using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public bool isPlaced;

    // �������� �������� ��������
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0); //�������� ����� �� �����
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
        // ��������� ������� ����� ������ ����
        if (!isPlaced && cardGrab.isReady && Input.GetMouseButtonDown(0))
        {
            // ������� ��� �� ������ � ����� ������� ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ���������, ����� �� ��� �� ������ � ����� "Cell"
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Cell"))
            {
                GameObject cell = hit.collider.gameObject;
                // �������� ������� ������, �� ������� ���� ����������� �������
                Vector3 targetPosition = hit.collider.transform.position;

                // ���������� ����� � ��������� ������ � ��������� �������� ������ ��� ���� �����
                MoveCardToPosition(targetPosition);
                AddCardToTableDictionary(cell, gameObject.GetComponentInChildren<CardDisplay>().cardAttribute);
                cardGrab.isReady = false;
                isPlaced=true;
                FindObjectOfType<CardSpawn>().ocupied[cardNumber] = false;
            }
        }
    }

    // ����� ��� ����������� ��������� ����� � ��������� �������
    private void MoveCardToPosition(Vector3 targetPosition)
    {
        // ������������� ������� ����� �� ������� �������
        transform.position = targetPosition + offset;

        // ������������� �������� ����� �� �������� ��������
        transform.eulerAngles = targetRotation;
    }

    private void AddCardToTableDictionary(GameObject chosenCell, CardAttributes cardAttributes)
    {
        if (cardAttributes == null || chosenCell == null)
            return;

        // ����� ������� ������� ���� TableManager � ����� � ��������� ��� ���������� TableManager
        var tm = GameObject.FindFirstObjectByType<TableManager>().GetComponent<TableManager>();
        // ��������� ������ ������ �� ���������� ������� ������ (chosenCell)
        int cellNumber = chosenCell.GetComponent<CellNumber>().cellNumber;

        tm.AddCardToTable(cellNumber, cardAttributes);

        if (tm.cardPrefabs.ContainsKey(cellNumber)) // ��������, �������� �� ������� cardPrefabs ���� � ������� ������
            tm.cardPrefabs[cellNumber] = gameObject; // ���� ���� ����������, ���������� �������� (�������� �������) ��� ������� �����
        else
            tm.cardPrefabs.Add(cellNumber, gameObject); // ���� ����� ���, ���������� ������ �������� � �������
    }
}