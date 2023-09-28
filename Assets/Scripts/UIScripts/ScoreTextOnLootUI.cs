using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextOnLootUI : ScoreTextPopUpUI
{
    //[SerializeField] private TextMeshProUGUI _scoreText;
    //[SerializeField] private GameObject _player;

    //private float _livingTime;
    //private float _moveSpeed = 5f;
    //private int _positionToMoveUpY = 10;
    //private void Awake()
    //{
    //    Hide();
    //}
    //private void Start()
    //{
    //    Loot.OnLootScoreAdd += Loot_OnLootScoreAdd;
    //}
    //private void Update()
    //{
    //    if (_scoreText.gameObject.activeInHierarchy)
    //    {
    //        _livingTime += Time.deltaTime;
    //        if (_livingTime > 1f)
    //        {
    //            Hide();
    //        }
    //        MoveTextUp();
    //    }
    //}

    //private void MoveTextUp()
    //{
    //    Debug.Log(_livingTime);
    //    float _moveUpSpeed = _moveSpeed * Time.deltaTime;
    //    Vector3 _moveDirection = new Vector3(transform.position.x, _positionToMoveUpY, transform.position.z);
    //    transform.position = Vector3.MoveTowards(transform.position, _moveDirection, _moveUpSpeed);
    //}

    //private void Loot_OnLootScoreAdd(object sender, Loot.OnLootScoreAddEventArgs e)
    //{
    //    int _score = e.lootScore;
    //    if (_score > 0)
    //    {
    //        _scoreText.text = "+ " + _score.ToString();
    //        Show();

    //        gameObject.transform.position = _player.transform.position;
    //        Debug.Log("Show Score!");
    //    }
    //}
    //private void Show()
    //{
    //    _scoreText.gameObject.SetActive(true);
    //    _livingTime = 0;
    //}
    //private void Hide()
    //{
    //    _scoreText.gameObject.SetActive(false);
    //}

    [SerializeField] private Transform _player;

    private void Start()
    {
        Loot.OnLootScoreAdd += Loot_OnLootScoreAdd;
    }
    private void Loot_OnLootScoreAdd(object sender, Loot.OnLootScoreAddEventArgs e)
    {
        SetScoreText(e.lootScore);
        Show();
        SetScoreTextPosition(_player);
    }
}
