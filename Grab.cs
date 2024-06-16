using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] private Vector3 ReadyPosition;
    [SerializeField] private Vector3 ReadyRotation;
    public bool isReady;
    private Vector3 originEulerAngles; // ���������� ��� �������� ��������� ���� �������� �������

    private void Start()
    {
        isReady = false;
        originEulerAngles = transform.eulerAngles;
    }

    private void Update()
    {
        if (!GetComponent<CardMovement>().isPlaced) // ��������, ���� ������ �� ���������� �� �����
            Grabbing();

    }

    private void Grabbing()
    {
        // �������� ������� ����� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                transform.position = ReadyPosition;
                transform.eulerAngles = ReadyRotation;
                Camera.main.GetComponent<W_table>().cardSelected = true;
                isReady = true;
            }
        }
        if (isReady)
        {

            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.eulerAngles = originEulerAngles;
                isReady = false;
            }
        }
    }
}