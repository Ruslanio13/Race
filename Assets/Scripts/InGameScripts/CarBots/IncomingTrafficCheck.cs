using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingTrafficCheck : MonoBehaviour
{
    [SerializeField] private TrafficCollisionAvoidance _trafficAvoidance;
    [SerializeField] private CarBots _carBots;
    void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "Car":
                {
                    Debug.Log("Time To Slow Down");

                    _trafficAvoidance.SwitchLane(col.GetComponent<CarBots>().GetRefSpeed(), col.GetComponent<CarBots>().GetSpeed());
                }
                break;



        }
    }

}
