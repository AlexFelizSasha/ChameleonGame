using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private GameObject _lootVisual;

    [SerializeField] private List<MaterialSO> _materialSOList;

    private float _fallingSpeed = 6.0f;
    private bool _isTouched = false;
    private bool _isPicked = false;
    private int _downPointY = -25;   //how low loot falls down

    private void Start()
    {
        _lootVisual.GetComponent<LootVisual>().OnLootTouched += Loot_OnLootTouched;
        _lootVisual.GetComponent<LootVisual>().OnLootPicked += Loot_OnLootPicked;
    }
    private void Update()
    {
        if (_isTouched)
        {
            DropDownLoot();
        }
        if (_isPicked)
        {
            DestroyLoot();
        }
    }
    private void Loot_OnLootPicked(object sender, System.EventArgs e)
    {
        _isPicked = true;
    }


    private void Loot_OnLootTouched(object sender, System.EventArgs e)
    {       
        _isTouched = true;

        Debug.Log("Loot Touched!");
    }
    private void DropDownLoot()
    {
        float _moveDistance = _fallingSpeed * Time.deltaTime;
        
        Vector3 _fallingDirection = new Vector3(transform.position.x, _downPointY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, _fallingDirection, _moveDistance);
    }
    private void DestroyLoot()
    {
        Destroy(gameObject);
    }
}
