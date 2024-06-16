//������� ������ CardSpawn


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CardSpawn : MonoBehaviour
//{
//    [SerializeField] private GameObject objectToSpawn; // ������ �������, ������� �� ����� ��������
//    [SerializeField] private Vector3[] spawnPositions; // ������ ��������� ��������� ��� ������ �����
//    [SerializeField] private Vector3[] targetPositions; // ������ ������� ��������� ��� ������ �����
//    [SerializeField] private float delayBetweenCards = 1f; // �������� ����� ���������� ������ �����
//    [SerializeField] private CardAttributes[] cardAttributes;



//    void Start()
//    {
//        // ���������, ��� � ��� ���� ����������� ���������� ��������� � ������� �������
//        if (spawnPositions.Length != targetPositions.Length || spawnPositions.Length != cardAttributes.Length)
//        {
//            Debug.LogError("Invalid setup: Spawn positions, target positions, and card attributes should have the same length.");
//            return;
//        }

//        // �������� �� ������ ������� � ������� ����� �� ���
//        for (int i = 0; i < spawnPositions.Length; i++)
//        {
//            // ������� ������ �� ������� �����������
//            GameObject spawnedCard = Instantiate(objectToSpawn, spawnPositions[i], Quaternion.identity);
//            SetCardAttributes(spawnedCard, cardAttributes[i]); // ����������� �������� ������ �����

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
//            // ��������� ��������, ������� ���������� ������ ����� ��������� ��������
//            //StartCoroutine(MoveObjectAfterDelay(spawnedCard, targetPositions[i], delayBetweenCards * i));
//        }

//    }

//    private void SetCardAttributes(GameObject cardObject, CardAttributes attributes)
//    {
//        CardDisplay cardDisplay = cardObject.GetComponentInChildren<CardDisplay>(); // �������� ������ ���������� ������
//        if (cardDisplay != null)
//        {
//            cardDisplay.SetAttributes(attributes); // ����������� �������� �����
//        }
//        else
//        {
//            Debug.LogError("CardDisplay script is missing on the card object.");
//        }
//    }

//    IEnumerator MoveObjectAfterDelay(GameObject obj, Vector3 target, float delay)
//    {
//        // ���� ��������� ���������� ������
//        yield return new WaitForSeconds(delay);
//        // ���������� ������ �� ������� ����������
//        obj.transform.position = target;
//    }
//}
