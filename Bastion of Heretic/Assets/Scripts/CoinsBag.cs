using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsBag : MonoBehaviour
{
    [SerializeField] int startingBalance = 100;
    [SerializeField] int currentBalance;
    [SerializeField] TMP_Text currentCoinsUI;
    public int CurrentBalance {get{return currentBalance;}}
    public void updateBalance(int quantity)
    {
        currentBalance += quantity;
        if (currentBalance < 0)
        {
            currentBalance = 0;
        }
        currentCoinsUI.text = "Coins: " + currentBalance.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentBalance = startingBalance;
        currentCoinsUI.text = "Coins: " + currentBalance.ToString();
    }

    // Update is called once per frame
}
