using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _moveDistance;
    [SerializeField] private RectTransform[] _interface;
    [SerializeField] private Vector3 _moveDir;
    private List<float> _startInterfacePosition = new List<float>();

    public void StartMoveCamera()
    {
        StartCoroutine(MoveCamera());
    }

    IEnumerator MoveCamera()
    {
        FullStartInterface(_interface, _startInterfacePosition, _moveDir);

        while (ComparePosition(_interface[0], _startInterfacePosition[0], _moveDir))
        {
            foreach (RectTransform RT in _interface)
                RT.transform.localPosition += _moveDir * Math.Abs(_moveDistance * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Arrange(_interface, _startInterfacePosition, _moveDir);
        _startInterfacePosition.Clear();
    }

    private bool ComparePosition(RectTransform interf, float startInterfPos, Vector3 direction)
    {
        if (direction.x != 0)
        {
            if (direction.x > 0)
            {
                return (interf.transform.localPosition.x < _moveDistance + startInterfPos);
            }
            else
            {
                return (interf.transform.localPosition.x > _moveDistance + startInterfPos);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                return (interf.transform.localPosition.y < _moveDistance + startInterfPos);
            }
            else
            {
                return (interf.transform.localPosition.y > _moveDistance + startInterfPos);
            }
        }
    }

    private void Arrange(RectTransform[] interf, List<float> startInterfPos, Vector3 direction)
    {
        if (_moveDir.x != 0)
        {
            for (int i = 0; i < startInterfPos.Count; i++)
                interf[i].localPosition = new Vector3(_moveDistance + startInterfPos[i], interf[i].localPosition.y);
        }
        else
        {
            for (int i = 0; i < startInterfPos.Count; i++)
                interf[i].localPosition = new Vector3(interf[i].localPosition.x, _moveDistance + startInterfPos[i]);
        }
    }

    private void FullStartInterface(RectTransform[] interf, List<float> startInterfPos, Vector3 direction)
    {
        if (direction.x != 0)
            foreach (RectTransform RT in interf)
                startInterfPos.Add(RT.transform.localPosition.x);
        else
            foreach (RectTransform RT in interf)
                startInterfPos.Add(RT.transform.localPosition.y);
    }
}
