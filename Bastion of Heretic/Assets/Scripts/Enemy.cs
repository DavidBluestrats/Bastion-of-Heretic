using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;
    CoinsBag coinManager;
     void Start()
     {
        coinManager = FindObjectOfType<CoinsBag>();
     }
    public void RewardGold()
    {
        coinManager.updateBalance(goldReward);
    }
    public void TakeGold()
    {
        coinManager.updateBalance(-goldPenalty);
    }
}
