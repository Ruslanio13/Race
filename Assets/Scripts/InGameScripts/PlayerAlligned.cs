using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerAlligned
{
    void FollowPlayer();
    float OffsetX { get; set; }
    float OffsetY { get; set; }
}
