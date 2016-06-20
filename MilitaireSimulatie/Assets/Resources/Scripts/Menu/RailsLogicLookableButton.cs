using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class RailsLogicLookableButton : LookableButton
{
    [SerializeField]
    string methodName;
    [SerializeField]
    RailsLogic railsLogic;

    protected override void Start()
    {
        base.Start();

        //railsLogic = GetComponent<RailsLogic>();
    }

    protected override void Action()
    {
        Type railsLogicType = railsLogic.GetType();
        MethodInfo method = railsLogicType.GetMethod(methodName);
        method.Invoke(railsLogic, null);
        gameObject.SetActive(false);
    }
}
