using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UltimateButton : MonoBehaviour
{
    public TableManager tableManager; // ������ �� TableManager

    public UnityEvent onClick; // �������, ������� ����� ���������� ��� ������� �� ������




    // �����, ���������� ��� ������� �� ������ �����
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) // ���������, ���� �� ������ ����� ������ ����
        {
            // �������� ������� ��� ������� �� ������
            onClick.Invoke();

            if (tableManager != null)
            {
                // �������� ����� ��� ���������� ����� � ��������
                tableManager.UseUltimate();
            }
            else
            {
                Debug.LogError("TableManager is not assigned!");
            }
        }
    }
}
