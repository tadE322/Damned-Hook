using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int minCoins = 2;
    private int maxCoins = 5;

    public void CoinDrop(Vector3 position)
    {
        int coinsToDrop = Random.Range(minCoins, maxCoins + 1);

        for (int i = 0; i < coinsToDrop; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0f);
            Instantiate(this, position + randomOffset, Quaternion.identity);
        }
    }
}