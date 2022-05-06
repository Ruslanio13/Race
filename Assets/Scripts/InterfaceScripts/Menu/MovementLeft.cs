using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLeft : MonoBehaviour
{
    [SerializeField] private float _offsetDistance;
    [SerializeField] private RectTransform[] _interface;
     private List<float> _startInterfacePositions = new List<float>();

    public void StartMoveCameraLeft()
    {       
        StartCoroutine(MoveCameraLeft());
    }

    IEnumerator MoveCameraLeft()
    {
        FullStartInterface();

        while (_interface[0]?.transform.localPosition.x > -_offsetDistance + _startInterfacePositions[0])
        {
            foreach (RectTransform RT in _interface)
                RT.transform.localPosition += Vector3.left * _offsetDistance * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        for (int i = 0; i < _interface.Length; i++)
            _interface[i].localPosition = new Vector3(-_offsetDistance + _startInterfacePositions[i], _interface[i].localPosition.y);

        _startInterfacePositions.Clear();
    }
    private void FullStartInterface()
    {
        foreach (RectTransform RT in _interface)
        {
            _startInterfacePositions.Add(RT.localPosition.x);
        }
    }
}
