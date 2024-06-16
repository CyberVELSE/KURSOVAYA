using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    public int targetIndex = 0;

    private bool isHovering = false;
    private Vector3 targetPosition;
    [SerializeField] private float hoverHeight = 0.1f; // ������ ������� ������� ��� ��������� �������

    private void OnMouseEnter() // �����, ���������� ��� ��������� ������� ���� �� ������
    {
        isHovering = true;
    }

    private void OnMouseExit() // �����, ���������� ��� ����� ������� ���� � �������
    {
        isHovering = false;
    }

    private void Update()
    {
        if (!GetComponent<CardMovement>().isPlaced)
        {
            if (isHovering && !GetComponent<Grab>().isReady) // ���� ������ ������ � ������ �� ��������� �� ����
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition + Vector3.up * hoverHeight, Time.deltaTime * 5f);
            }
            else if (!GetComponent<Grab>().isReady)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
            }
        }
    }


    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }
}