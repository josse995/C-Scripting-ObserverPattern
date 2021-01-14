using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBroker
{
    public static event Action ProjectileOutOfBounds;

    public static void CallProjectileOutOfBounds()
    {
        if (ProjectileOutOfBounds != null)
            ProjectileOutOfBounds();
    }
}
