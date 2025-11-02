using UnityEngine;

public class Sweet : CharacterController
{
    protected int Scurhp;   // ½Ä¹° HP
    protected new void Start()
    {
        gameObject.layer = 12;
        HP = Scurhp;
        base.Start();
    }
}


