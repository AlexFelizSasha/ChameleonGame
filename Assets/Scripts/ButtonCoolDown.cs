using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCoolDown : MonoBehaviour
{
    public bool isActive = true;

    private GameConstantsSO _gameConstantsSO;
    private float _ñooldownTimerMax;
    private float _cooldownTimer;
    private Button _button;
    private Image _buttonImage;

    private void Awake()
    {
        _gameConstantsSO = DifficultyChoice.chosenDifficultySO;
        _ñooldownTimerMax = _gameConstantsSO.colorButtonCooldownTime;

        _button = GetComponent<Button>();
        _buttonImage = _button.GetComponent<Image>();
    }
    public void ColldownButton()
    {
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
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
