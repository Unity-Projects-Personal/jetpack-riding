using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
  private int coins;
  public void AddCoins(int coin_amount)
  {
    int coins = PlayerPrefs.HasKey("coins") ? PlayerPrefs.GetInt("coins") : 0;
    PlayerPrefs.SetInt("coins", (coins + coin_amount));
  }
}
