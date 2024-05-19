using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpawner : MonoBehaviour
{
    public GameObject sugarObject;

    private void Start()
    {
        SpawnSugar();
    }

    void SpawnSugar()
    {
        Instantiate(sugarObject);
        Invoke("SpawnSugar", 7f);   // 생성 쿨타임
    }
}
