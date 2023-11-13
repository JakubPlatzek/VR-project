using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public WristUI wristUI;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI buttonText;
    private float countdownTime = 10 * 60; 
    private bool isCountdownActive = false;
    private Coroutine countdownCoroutine;
    MoneyHandler moneyHandler;

    void Awake() 
    {
        moneyHandler = GameObject.Find("MoneyCounter").GetComponent<MoneyHandler>();
    }

    // Call this method when the button is pressed
    public void ToggleCountdown()
    {
        if (isCountdownActive)
        {
            if (wristUI.CheckOrder()) {
                StopCountdown();
                moneyHandler.SetMoney(Random.Range(100, 300));
                wristUI.ClearOrder();
            } else {
                errorText.text = "Order isn't complete!";
            }
        }
        else
        {
            StartCountdown();
            wristUI.GenerateOrder();
        }
    }

    private void StartCountdown()
    {
        countdownCoroutine = StartCoroutine(CountdownCoroutine());
        buttonText.text = "Check order";
    }

    private void StopCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
        isCountdownActive = false;
        countdownText.text = "10:00"; 
        buttonText.text = "Start order";
    }

    private IEnumerator CountdownCoroutine()
    {
        isCountdownActive = true;
        float currentTime = countdownTime;

        while (currentTime > 0)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);

            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1);

            currentTime -= 1;
        }

        countdownText.text = "Time is up";
        isCountdownActive = false;
    }
}
