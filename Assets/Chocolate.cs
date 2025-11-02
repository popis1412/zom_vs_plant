using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chocolate : Sweet
{
    new void Start()
    {
        Scurhp = 80;
        base.Start();
    }

    private void Update()
    {
        animator.SetInteger("hurtHP", HP);
    }

}
