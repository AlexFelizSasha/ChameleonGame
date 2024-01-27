using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCoolDown : MonoBehaviour
{
    public bool isActive = true;

    [SerializeField] private GameConstantsSO _gameConstants;
    private float _ñooldownTimerMax;
    private float _cooldownTimer;
    private Button _button;
    private Image _buttonImage;

    private void Awake()
    {
        _ñooldownTimerMax = _gameConstants.colorButtonCooldownTime;

        _button = GetComponent<Button>();
        _buttonImage = _button.GetComponent<Image>();
    }
    public void ColldownButton()
    {
        Debug.Log("cooldown");
        _buttonImage.fillAmount = 0;
        isActive = false;
        _cooldownTimer = 0;
        StartCoroutine(FillButtonImage());
    }
    private IEnumerator FillButtonImage()
    {
        while (!isActive)
        {
            _cooldownTimer += Time.deltaTime;
            _buttonImage.fillAmount =_cooldownTimer/ _ñooldownTimerMax;
            isActive = _buttonImage.fillAmount >= 1;
            Debug.Log("filling button" + _buttonImage.fillAmount);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
