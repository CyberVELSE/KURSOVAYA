using UnityEngine;

public class CardSpawn : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn; // ������ �������, ������� �� ����� ��������
    [SerializeField] private Vector3[] spawnPositions; // ������ ��������� ��������� ��� ������ �����
    [SerializeField] private Vector3[] targetPositions; // ������ ������� ��������� ��� ������ �����
    [SerializeField] private float delayBetweenCards = 1f; // �������� ����� ���������� ������ �����
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

        // ������� ������ �� ������� �����������
        GameObject spawnedCard = Instantiate(objectToSpawn, spawnPositions[i], Quaternion.identity);
        SetCardAttributes(spawnedCard, attributes); // ����������� �������� ������ �����
        spawnedCard.GetComponent<CardMovement>().SetCardNumber(i);

        // �������� ��������� CardMovement �� ��������� �����
        CardMovement cardMovement = spawnedCard.GetComponent<CardMovement>();

        // ��������� ���������� �������� �������� � �����
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
        // ���������, ��� � ��� ���� ����������� ���������� ��������� � ������� �������
        if (spawnPositions.Length != targetPositions.Length || spawnPositions.Length != cardAttributes.Length)
        {
            Debug.LogError("Invalid setup: Spawn positions, target positions, card rotations, and card attributes should have the same length.");
            return;
        }

        // �������� �� ������ ������� � ������� ����� �� ���
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            // ������� ������ �� ������� �����������
            GameObject spawnedCard = Instantiate(objectToSpawn, spawnPositions[i], Quaternion.identity);
            SetCardAttributes(spawnedCard, cardAttributes[i]); // ����������� �������� ������ �����
            spawnedCard.GetComponent<CardMovement>().SetCardNumber(i); 

            // �������� ��������� CardMovement �� ��������� �����
            CardMovement cardMovement = spawnedCard.GetComponent<CardMovement>();

            // ��������� ���������� �������� �������� � �����
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