using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagmanAttributes : MonoBehaviour
{
    // �������������� �������
    [SerializeField] private GameObject daun;
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _ultaText;
    public float maxHealth = 50f; // ������������ ��������
    public float currentHealth; // ������� ��������
    public float maxUltimateCharge = 100f; // ������������ ������� ������ �����
    public int currentUltimateCharge; // ������� ������� ������ �����

    void Start()
    {
        currentHealth = maxHealth; // ������������� ������������ ��������
        _healthText.text = currentHealth.ToString();
        currentUltimateCharge = 0; // ���������� ������� ������ ����� ����� ����
    }

    // ����� ��� ���������� �������� ��������
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        _healthText.text = currentHealth.ToString();
        if (currentHealth <= 0)
        {
            Die(); // ���� �������� ������ ��� ����� ����, �������.
        }
    }

    // ����� ��� ������� ����������
    public void ChargeUltimate(int percent)
    {
        currentUltimateCharge += percent;
        _ultaText.text = currentUltimateCharge.ToString();
        //Debug.Log($"{gameObject.name}: Ultimate charge = {currentUltimateCharge}");

        if (currentUltimateCharge > 100)
        {
            currentUltimateCharge = 100;
            Debug.Log("������ �� ������ :/");
        }
    }


    // ����� ��� ��������� ������ ��������
    void Die()
    {
        daun.SetActive(true);
        StartCoroutine(Fade());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10f);
            Debug.Log(currentHealth);
        }
    }

    IEnumerator Fade()
    {
        for (float i = 0f; i < 1; i += 0.002f)
        {
            daun.GetComponentInChildren<Text>().color = new Color(1, 0, 0, i);
            yield return new WaitForFixedUpdate();
        }
    }
}
