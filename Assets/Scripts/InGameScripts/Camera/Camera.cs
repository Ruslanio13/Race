using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : PlayerFollower
{
    private void Start()
    {
        SetOffsets();
    }

    private void Update()
    {
        FollowPlayer();
    }
}
