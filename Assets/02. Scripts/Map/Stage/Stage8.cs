using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage8 : StageBase
{
    void Start()
    {
        StageIndex = 8;
    }

    public override void UseDrug()
    {
        Base.Manager.Map.StopTimeBacking();
        Base.Manager.Map.ModifyPlayerSpeed(1f);
    }
}