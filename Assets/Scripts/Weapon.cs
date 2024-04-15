using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;

    public int count;
    public float speed;

    float initialX = 1f;
    float initialY = 0f;

    float savex;
    float savey;

    float timer;
    Player player;


    void Awake()
    {
        player = GetComponentInParent<Player>();

    }


    void Start()
    {
        Init();
        savex = initialX;
        savey = initialY;
    }

    void Update()
    {
        // 플레이어의 움직임을 측정합니다.
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // 플레이어가 움직이지 않을 때는 savex와 savey 값을 업데이트하지 않습니다.
        if (x != 0 || y != 0)
        {
            savex = x;
            savey = y;
        }

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            case 1:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Axe();
                }
                break;
            case 2:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Bow();
                }
                break;
            default:
                break;
        }
    }
    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                BatchShield();
                break;
            case 1:
                speed = 1f; //공격 속도 조정, 낮을 수록 빠름
                break;
            case 2:
                speed = 1f; //공격 속도 조정, 낮을 수록 빠름
                break;
            default:
                break;
        }
    }
    void BatchShield() //방패 배치하는 함수
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.parent = transform;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 2f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1은 계속 관통
        }
    }

    void BatchSword() //검 배치하는 함수
    {
        Transform sword = GameManager.instance.pool.Get(prefabId).transform;
        sword.parent = transform;
    }
    void Axe() //도끼 함수
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        StartCoroutine(RotateAndMove(bullet, dir));
        bullet.GetComponent<Bullet>().Init(damage, 0, dir);
    }
    IEnumerator RotateAndMove(Transform target, Vector3 dir) //도끼 회전하며 날아가는 코루틴
    {
        float rotationSpeed = 180f; // 1초에 180도씩 회전하도록 설정
        float angle = 0f;
        while (angle < 360f)
        {
            // 시간에 따라 회전 각도를 증가시킵니다.
            float rotationAmount = rotationSpeed * Time.deltaTime;
            target.Rotate(Vector3.forward, rotationAmount);
            target.Translate(dir * 2f * Time.deltaTime, Space.World);
            angle += Mathf.Abs(rotationAmount);
            yield return null;
        }
    }
    void Bow()
    {
        if (!player.scanner.nearestTarget)
            return;

            Vector3 targetPos = player.scanner.nearestTarget.position;
            Vector3 dir = targetPos - player.transform.position;
            dir = dir.normalized;

            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<Bullet>().Init(damage, 1, dir);
    }
}




    /* 보는 방향으로 오브젝트 발사하는 알고리즘 잠깐 보류
    void spear()
    {
        // 플레이어의 바라보는 방향을 얻어옵니다.
        Vector3 dir = new Vector3(savex, savey, 0);

        // 방향을 정규화합니다.
        dir = dir.normalized;

        // 총알 오브젝트를 풀에서 가져옵니다.
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;

        bullet.GetComponent<Bullet>().Init(damage, 0, dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    */