using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI buttonText;
    private float countdownTime = 10 * 60; 
    private bool isCountdownActive = false;
    private Coroutine countdownCoroutine;

    // Call this method when the button is pressed
    public void ToggleCountdown()
    {
        if (isCountdownActive)
        {
            StopCountdown();
        }
        else
        {
            StartCountdown();
        }
    }

    private void StartCountdown()
    {
        countdownCoroutine = StartCoroutine(CountdownCoroutine());
        buttonText.text = "Finish order";
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
