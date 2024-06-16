using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UltimateButton : MonoBehaviour
{
    public TableManager tableManager; // Ссылка на TableManager

    public UnityEvent onClick; // Событие, которое будет вызываться при нажатии на кнопку




    // Метод, вызываемый при нажатии на кнопку атаки
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) // Проверяем, была ли нажата левая кнопка мыши
        {
            // Вызываем событие при нажатии на кнопку
            onClick.Invoke();

            if (tableManager != null)
            {
                // Вызываем метод для применения урона к флагману
                tableManager.UseUltimate();
            }
            else
            {
                Debug.LogError("TableManager is not assigned!");
            }
        }
    }
}
