using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _coinsDisplayText;
    [SerializeField]
    private Text _livesDisplayText;

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
    }

    public void UpdateCoinsDisplay(int coins)
    {
        _coinsDisplayText.text = "Coins: " + coins;
    }

    public void UpdateLivesDisplay(int lives)
    {
        _livesDisplayText.text = "Lives: " + lives;
    }
}
