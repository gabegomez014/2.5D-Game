using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _coinsDisplayText;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is null");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        _coinsDisplayText.text = "Coins: 0"; 
    }

    public void UpdateCoinsDisplay(int coins)
    {
        _coinsDisplayText.text = "Coins: " + coins;
    }
}
