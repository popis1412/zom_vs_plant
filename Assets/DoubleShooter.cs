using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DoubleShooter : BasicShooter
{
    new void Start()
    {
        base.Start();
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/DoubleCone_Bullet");
        shootOrigin = this.gameObject.transform;
    }

    new void Update()
    {
        base.Update();
    }
}
