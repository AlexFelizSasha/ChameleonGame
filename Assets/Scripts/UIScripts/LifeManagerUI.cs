using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeManagerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lifeText;
    private int _lifeAmount;

    private void Start()
    {
        _lifeAmount = ConstantsKeeper.LIFE_AMOUNT;
        Block.OnKillPlayer += Block_OnKillPlayer;
        _lifeText.text = "Life: " + _lifeAmount.ToString();
    }

    private void Block_OnKillPlayer(object sender, System.EventArgs e)
    {
        _lifeAmount --;
        _lifeText.text = "Life: " + _lifeAmount.ToString();
    }
}
