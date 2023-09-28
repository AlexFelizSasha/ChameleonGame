using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextPopUpUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _scoreText;

    protected float _livingTime;
    protected float _moveSpeed = 5f;
    protected int _positionToMoveUpY = 10;
    protected void Awake()
    {
        Hide();
    }
    protected void Update()
    {
        PopUpScoreText();
    }

    protected void MoveTextUp()
    {
        float _moveUpSpeed = _moveSpeed * Time.deltaTime;
        Vector3 _moveDirection = new Vector3(transform.position.x, _positionToMoveUpY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, _moveDirection, _moveUpSpeed);
    }
    protected void SetScoreText(int score)
    {
        _scoreText.text = "+ " + score.ToString();
    }
    protected void SetScoreTextPosition(Transform scoreObjectTransform)
    {
        gameObject.transform.position = scoreObjectTransform.position;
    }
    protected void PopUpScoreText()
    {
        if (_scoreText.gameObject.activeInHierarchy)
        {
            _livingTime += Time.deltaTime;
            if (_livingTime > 1f)
            {
                Hide();
            }
            MoveTextUp();
        }
    }
    protected void Show()
    {
        _scoreText.gameObject.SetActive(true);
        _livingTime = 0;
    }
    protected void Hide()
    {
        _scoreText.gameObject.SetActive(false);
    }
}
