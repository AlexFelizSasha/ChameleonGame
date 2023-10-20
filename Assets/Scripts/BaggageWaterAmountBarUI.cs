using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaggageWaterAmountBarUI : MonoBehaviour
{
    [SerializeField] private Transform _baggageTransform;
    [SerializeField] private Image _waterAmountBar;
    private PlayerBaggage _baggage;

    private void Start()
    {
        _baggage = _baggageTransform.GetComponent<PlayerBaggage>();
    }
    private void Update()
    {
        transform.position = _baggageTransform.position;
        CountWaterAmount();
    }
    private void CountWaterAmount()
    {
        _waterAmountBar.fillAmount = _baggage.GetWaterAmount();
    }
}
