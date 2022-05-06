using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCollisionAvoidance : MonoBehaviour
{
    [SerializeField] private CarBots _parentCar;
    [SerializeField] private float _brakingKoef;
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _speedAnim;
    [SerializeField] private float _offset;
    [SerializeField] private float _laneWidth;

    [SerializeField] private FreeLanes _freeLanes;
    private float _botSpeed;
    private float _speedOffset = 0.1f;
    private List<string> _permissibleTags = new List<string>();

    private bool _freeToTheLeft;
    private bool _freeToTheRight;
    private void Start()
    {
        _laneWidth = 0.5f;
        _freeLanes = FreeLanes.BOTH_LANES_FREE;
        _freeToTheLeft = true;
        _freeToTheRight = true;
        _permissibleTags.Add("Car");
        _permissibleTags.Add("Border");
    }

    private enum FreeLanes
    {
        NO_FREE_LANES = 0,
        LEFT_LANE_FREE = 1,
        RIGHT_LANE_FREE = 2,
        BOTH_LANES_FREE = 3

    }
    private enum TurnDirection
    {
        LEFT = -1,
        RETURNING = 0,
        RIGHT = 1,
    }


    public void SlowDown(float refIncomingTrafficSpeed, float incomingTrafficSpeed)
    {
        _botSpeed = _parentCar.GetSpeed();
        StartCoroutine(SmoothSlowDown(incomingTrafficSpeed));
    }
    private IEnumerator SmoothSlowDown(float incomingTrafficSpeed)
    {
        while (true)
        {
            _botSpeed = _parentCar.GetSpeed();
            if (_botSpeed > incomingTrafficSpeed + _speedOffset)
            {
                _parentCar.SetSpeed(_botSpeed - (_botSpeed - incomingTrafficSpeed) * Time.deltaTime * _brakingKoef);
            }
            else if (_botSpeed < incomingTrafficSpeed + _speedOffset)
            {
                _parentCar.SetSpeed(incomingTrafficSpeed);
            }
            yield return new WaitForEndOfFrame();
        }
    }


    public void SwitchLane(float thisBotSpeed, float incBotSpeed)
    {
        int rand = Random.Range(0, (int)_freeLanes + 1);
        switch (rand)
        {
            case 0:
                {
                    StartCoroutine(SmoothSlowDown(incBotSpeed));
                    break;
                }
            case 1:
                {
                    StartCoroutine(CommenceSwitching(Vector3.left, _parentCar.transform.position.x - _laneWidth));
                    break;
                }
            case 2:
                {
                    StartCoroutine(CommenceSwitching(Vector3.right, _parentCar.transform.position.x + _laneWidth));
                    break;
                }
            case 3:
                {
                    StartCoroutine(CommenceSwitching(Vector3.right, _parentCar.transform.position.x + _laneWidth));
                    break;
                }
        }


    }

    private IEnumerator CommenceSwitching(Vector3 turnDir, float targetLaneX)
    {
        switch (turnDir.x)
        {
            case -1:
                {
                    while (_parentCar.transform.position.x > targetLaneX)
                    {
                        _parentCar.transform.position += Vector3.left * Time.deltaTime;
                        yield return new WaitForEndOfFrame();
                    }
                }
                break;

            case 1:
                while (_parentCar.transform.position.x < targetLaneX)
                {
                    _parentCar.transform.position += Vector3.right * Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                break;
        }
        Debug.Log("Switch Finished "+ _parentCar.gameObject.name);
    }


    void OnTriggerEnter(Collider col)
    {
        if (_permissibleTags.Contains(col.tag))
            UpdateFreeLanes(col, false);
    }
    void OnTriggerExit(Collider col)
    {
        if (_permissibleTags.Contains(col.tag))
        {
            UpdateFreeLanes(col, true);
        }
    }

    private void UpdateFreeLanes(Collider col, bool operation)
    {
        switch (col.gameObject.tag)
        {
            case "Car":
                {
                    if (_parentCar.GetComponent<CarBots>().IsOnComing == col.gameObject.GetComponent<CarBots>().IsOnComing)
                    {
                        if (col.gameObject.transform.position.x < gameObject.transform.position.x)
                            _freeToTheLeft = operation;
                        else if (col.gameObject.transform.position.x > gameObject.transform.position.x)
                            _freeToTheRight = operation;
                    }
                    break;
                }
            case "Border":
                {
                    if (col.gameObject.transform.position.x < gameObject.transform.position.x)
                    {
                        _freeToTheLeft = operation;
                    }
                    else if (col.gameObject.transform.position.x > gameObject.transform.position.x)
                        _freeToTheRight = operation;
                    break;
                }
        }

        if (_freeToTheLeft && _freeToTheRight)
            _freeLanes = FreeLanes.BOTH_LANES_FREE;
        else if (!(_freeToTheLeft || _freeToTheRight))
            _freeLanes = FreeLanes.NO_FREE_LANES;
        else if (_freeToTheRight)
            _freeLanes = FreeLanes.RIGHT_LANE_FREE;
        else
            _freeLanes = FreeLanes.LEFT_LANE_FREE;

    }

    private void OnDestroy()
    {
        Destroy(_parentCar.gameObject);
    }
}
