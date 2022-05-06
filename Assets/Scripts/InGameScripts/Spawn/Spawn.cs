using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : PlayerFollower
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
