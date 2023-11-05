using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    [SerializeField] private List<GameObject> _snowParts;
    private float _minimalSpeed = 2.0f;
    private float _maximalSpeed = 5.0f;

    private void OnEnable()
    {
        PutSnowPartsBack();
    }

    private void Update()
    {
        MoveSnow();
    }

    private void MoveSnow()
    {
        for (int i = 0;  i < _snowParts.Count; i++)
        {
            float _speed = Random.Range(_minimalSpeed, _maximalSpeed);
            float _snowPartSpeed = _speed * Time.deltaTime;

            GameObject _snowPart = _snowParts[i];

            float _movePointZposition = transform.position.z + Random.Range(-3, 3);
            float _movePointXposition = transform.position.x + Random.Range(-3, 3);
            Vector3 _movePoint = new Vector3(_movePointXposition, _snowPart.transform.position.y, _movePointZposition);

            _snowPart.transform.position = Vector3.MoveTowards(_snowPart.transform.position, _movePoint, _snowPartSpeed);
        }            
    }
    private void PutSnowPartsBack()
    {
        for (int i = 0; i < _snowParts.Count; i++)
            _snowParts[i].transform.position = transform.position;
    }
}
