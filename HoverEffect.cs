using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    public int targetIndex = 0;

    private bool isHovering = false;
    private Vector3 targetPosition;
    [SerializeField] private float hoverHeight = 0.1f; // Высота подъема объекта при наведении курсора

    private void OnMouseEnter() // Метод, вызываемый при наведении курсора мыши на объект
    {
        isHovering = true;
    }

    private void OnMouseExit() // Метод, вызываемый при уходе курсора мыши с объекта
    {
        isHovering = false;
    }

    private void Update()
    {
        if (!GetComponent<CardMovement>().isPlaced)
        {
            if (isHovering && !GetComponent<Grab>().isReady) // Если курсор наведён и объект не перемещен на стол
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