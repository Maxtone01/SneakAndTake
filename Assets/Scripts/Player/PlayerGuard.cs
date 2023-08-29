using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuard : PlayerGeneral
{

    public void Update()
    {
        Inputs();
        SpeedSetup();
    }

    protected override void PlayerClassAction()
    {
        throw new System.NotImplementedException();
    }
}
