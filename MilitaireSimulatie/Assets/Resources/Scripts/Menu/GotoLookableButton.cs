using UnityEngine;
using System.Collections;
using System;

public class GotoLookableButton : LookableButton
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void Action()
    {
        Debug.Log("YES!");
    }
}
