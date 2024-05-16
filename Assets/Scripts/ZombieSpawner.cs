using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // 생성할 좀비 프리팹
    public Transform[] spawnPoints; // 좀비가 생성될 위치 배열
    public Transform[] spawnTiles; // 좀비가 생성될 타일 배열

    public float zombieSpeed = 3f; // 좀비의 이동 속도

    private float spawnInterval = 5f; // 좀비 생성 간격
    private float nextSpawnTime = 0f; // 다음 생성 시간

    private void Update()
    {
        // 현재 시간이 다음 생성 시간보다 크거나 같으면 좀비 생성
        if (Time.time >= nextSpawnTime)
        {
            SpawnZombie();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    // 좀비 생성 메서드
    private void SpawnZombie()
    {
        // 랜덤한 타일 선택
        int randomTileIndex = Random.Range(0, spawnTiles.Length);
        // 오루뜨는데 아직 모르겠습니다 -> Spawn Points 와 Spawn Tile에 Element(index)값이 들어가지 않아서 오류가 뜬겁니다.
        /* 스폰 타일의 Transfrom 넣으니까 문제가 해결되네요.
         그리고 길이가 아니라 해당 이름의 오브젝트를찾아서 Ramdom으로 생성이 되게 해야 될 것 같아요.
         -> randomTileIndex를 통해서 랜덤으로 생성된 해당 오브젝트의 위치에 나타나게 해야 될 것 같아요.
         ex) randomTileIndex = 5 , zomMakeTile5의 이름을 가진 오브젝트를 찾아서 
             zomMake5의 Trasnfrom 위치에 좀비를 생성
        */
        Transform selectedTile = spawnTiles[randomTileIndex];

        // 랜덤한 spawnPoint 선택 -> 얘도 마찬가지로 Element을 넣어주세요.
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnIndex];

        // 좀비 생성 위치를 선택한 타일의 위치로 설정
        Vector3 spawnPosition = selectedTile.position;

        // 좀비 생성
        GameObject newZombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

        // 좀비가 왼쪽으로 이동하도록 Rigidbody2D에 속도 설정
        Rigidbody2D rb = newZombie.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(-zombieSpeed, 0f);
        }
        else
        {
            Debug.LogWarning("Rigidbody2D component not found on zombie prefab.");
        }
    }
}