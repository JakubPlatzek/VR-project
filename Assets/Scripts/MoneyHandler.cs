using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyHandler : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyText;
    public GameOverManager gameOverManager;

    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = "Money: $" + money.ToString();
    }

    public void SetMoney(int newMoney)
    {
        money += newMoney;
        moneyText.text = "Money: $" + money.ToString();
        if(money < 0)
        {
            gameOverManager.ShowGameOver();
        }
    }
}
