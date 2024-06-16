using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagmanAttributes : MonoBehaviour
{
    // Характеристики корабля
    [SerializeField] private GameObject daun;
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _ultaText;
    public float maxHealth = 50f; // Максимальное здоровье
    public float currentHealth; // Текущее здоровье
    public float maxUltimateCharge = 100f; // Максимальный уровень заряда ульты
    public int currentUltimateCharge; // Текущий уровень заряда ульты

    void Start()
    {
        currentHealth = maxHealth; // Устанавливаем максимальное здоровье
        _healthText.text = currentHealth.ToString();
        currentUltimateCharge = 0; // Изначально уровень заряда ульты равен нулю
    }

    // Метод для уменьшения здоровья флагмана
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        _healthText.text = currentHealth.ToString();
        if (currentHealth <= 0)
        {
            Die(); // Если здоровье меньше или равно нулю, умираем.
        }
    }

    // Метод для зарядки ультимейта
    public void ChargeUltimate(int percent)
    {
        currentUltimateCharge += percent;
        _ultaText.text = currentUltimateCharge.ToString();
        //Debug.Log($"{gameObject.name}: Ultimate charge = {currentUltimateCharge}");

        if (currentUltimateCharge > 100)
        {
            currentUltimateCharge = 100;
            Debug.Log("Больше не влазит :/");
        }
    }


    // Метод для обработки смерти флагмана
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
