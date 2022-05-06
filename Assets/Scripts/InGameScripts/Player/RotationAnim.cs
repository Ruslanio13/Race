using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnim : MonoBehaviour
{

    [SerializeField] private float _speedAnim;
    [SerializeField] private float _maxAngle;
    [SerializeField] private float offset;
    public enum RotateDir
    {
        LEFT = -1,
        RIGHT = 1,
        RETURNING = 0
    }

    private RotateDir _rotateDir = RotateDir.RETURNING;


    void Update()
    {
        Turn();
    }

    void Turn()
    {
        switch (_rotateDir)
        {
            case RotateDir.LEFT:
                if (transform.rotation.z < _maxAngle)
                    transform.Rotate(new Vector3(0, 0, _speedAnim * Time.deltaTime));
                break;

            case RotateDir.RIGHT:
                if (transform.rotation.z > -_maxAngle)
                    transform.Rotate(new Vector3(0, 0, -_speedAnim * Time.deltaTime));
                break;

            case RotateDir.RETURNING:
                if (Mathf.Abs(transform.rotation.z) - offset > 0)
                {
                    if (transform.rotation.z > 0)
                        transform.Rotate(new Vector3(0, 0, -_speedAnim * Time.deltaTime));
                    else
                        transform.Rotate(new Vector3(0, 0, _speedAnim * Time.deltaTime));
                }
                else
                {
                    transform.rotation = Quaternion.identity;
                }
                break;

        }
    }

    public void SetRotationDir(RotateDir rot) => _rotateDir = rot;
}
