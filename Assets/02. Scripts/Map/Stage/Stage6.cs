using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6 : StageBase
{
    void Start()
    {
        StageIndex = 6;
    }

    public override void UseDrug()
    {
        Base.Manager.Sound.PlaySFX("SFX_Stage6_Item");

        Base.Manager.Map.SetInvincible(true);
        Base.Manager.PostProcessing.SetFlashBack();
    }
}
