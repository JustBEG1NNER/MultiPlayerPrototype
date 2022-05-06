using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinSystem : MonoBehaviour
{
    public float coin=0;
    public Text coinsHave;

    private void Update()
    {
        coinsHave.text = coin.ToString();
    }

    public void addCoin(float coinCollected)
    {
        coin = coin + coinCollected;
    }

   
}
