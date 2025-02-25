using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCone : BasicShooter
{
    new void Start()
    {
        base.Start();
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/IceCone_Bullet");
        shootOrigin = this.gameObject.transform;
    }

    new void Update()
    {
        base.Update();
    }
}
