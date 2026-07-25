using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
    private const int maxTime = 99;

    [Header("Component References")]
    [SerializeField] private TextMeshProUGUI digit1TextMesh;
    [SerializeField] private TextMeshProUGUI digit2TextMesh;
    [SerializeField] private PlayerShootController shootController;
    [SerializeField] private PlayerButtonsController buttonsController;

    [Header("Settings")]
    [SerializeField, Range(0, maxTime)] private int startingTime = 30;

    // Private Values
    private int timer;
    public Action<int, int> updatedDisplay;

    private void Awake()
    {
        if (shootController != null)
        {
            shootController.bulletShot += () => ReduceTimer(1);
        } else
        {
            Debug.LogWarning("ShootController not set");
        }
    }

    void Start()
    {
        timer = startingTime;
        StartCountdownCoroutine();
    }

    private void StartCountdownCoroutine()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        while (timer > 0)
        {
            ReduceTimer(1);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void UpdateNumberDisplay()
    {
        int digit2 = (timer % 10);
        int digit1 = (timer - digit2) / 10;

        digit1TextMesh.text = digit1.ToString();
        digit1TextMesh.color = Color.black;
        if (buttonsController.HasNumber(digit1))
        {
            digit1TextMesh.color = Color.red;
        }

        digit2TextMesh.text = digit2.ToString();
        digit2TextMesh.color = Color.black;
        if (buttonsController.HasNumber(digit2))
        {
            digit2TextMesh.color = Color.cyan;
        }

        updatedDisplay.Invoke(digit1, digit2);

        CheckZero();
    }

    public void ReduceTimer(int amountToReduce)
    {
        if (Mathf.Sign(amountToReduce) == -1) return;

        if (timer - amountToReduce > 0)
        {
            timer -= amountToReduce;
        }
        else
        {
            timer = 0;
        }
        UpdateNumberDisplay();
    }

    public void IncreaseTimer(int amountToIncrease)
    {
        if (Mathf.Sign(amountToIncrease) == -1) return;

        if (timer + amountToIncrease < maxTime)
        {
            timer += amountToIncrease;
        }
        else
        {
            timer = maxTime;
        }
        UpdateNumberDisplay();
    }

    private void CheckZero()
    {
        if (timer == 0)
        {
            return;
        }
    }
}
