using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Turn { Player, Bot }; // ������������ ��� ������������ ������� ����

    public Turn currentTurn; // ���������� ��� ������������ �������� ����

    public Bot bot; // ������ �� ������ ����

    public GameObject attackButton; // ������ �� ������ ������ �����

    private bool isClickable = true; // ���������� ��� ������������ �������������� ������



    void Start()
    {
        // ������� ������ AttackButton � �������� ������ �� ��� ��������� AttackButton
        AttackButton attackButton = FindObjectOfType<AttackButton>();

        currentTurn = Turn.Player; // �������� � ���� ������

        // ������������� �� ������� onClick
        attackButton.onClick.AddListener(OnAttackButtonClicked);
    }

    public void EndTurn()
    {
        if (currentTurn == Turn.Player)
        {
            currentTurn = Turn.Bot; // ���� ����� �����, ��������� � ���� ����
            StartCoroutine(BotTurn()); // ��������� ��� ����
        }
        else
        {
            currentTurn = Turn.Player; // ���� ����� ���, ��������� � ���� ������

            AttackButton attackButton = FindObjectOfType<AttackButton>();// ������� ������ AttackButton
            attackButton.SetClickable(true); ; // ������������ �������������� � ������� �����
        }
    }

    // �����, ���������� ��� ������� �� ������ �����
    private void OnAttackButtonClicked()
    {
        // ������� ������ AttackButton
        AttackButton attackButton = FindObjectOfType<AttackButton>();
        // ��������� �������������� � ������� �����
        attackButton.SetClickable(false);
    }

    IEnumerator BotTurn()
    {

        bot.MakeMove(); // �������� ������� ���� ����

        yield return new WaitForSeconds(2f); // �������� ��� ������� ��������
        
        EndTurn(); // ����������� ��� ����� ���� ����
    }
}
