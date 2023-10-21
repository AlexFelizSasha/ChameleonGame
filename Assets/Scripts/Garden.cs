using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Garden : MonoBehaviour
{
    private float _changeStateTime;
    private List<GameObject> _gardenTreesList;

    private void Start()
    {
        _gardenTreesList = TreeCreator.instance.GetGardenTreeList();
        _changeStateTime = ConstantsKeeper.TREE_CHANGE_STATE_TIME;
        WateringStopPoint.OnWateringStopPoint += WateringStopPoint_OnWateringStopPoint;
    }
    private void WateringStopPoint_OnWateringStopPoint(object sender, System.EventArgs e)
    {
        StartCoroutine(ChangeTreeState());
    }
    private IEnumerator ChangeTreeState()
    {
        for (int i = 0; i < _gardenTreesList.Count; i++)
        {
            _gardenTreesList[i].GetComponent<TreeHandler>().livingTime -= _changeStateTime;
            Debug.Log("Tree " + i + " changed");
            if (i % 3 == 0)
                yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
