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
        // �÷��̾��� �������� �����մϴ�.
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // �÷��̾ �������� ���� ���� savex�� savey ���� ������Ʈ���� �ʽ��ϴ�.
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
                speed = 1f; //���� �ӵ� ����, ���� ���� ����
                break;
            case 2:
                speed = 1f; //���� �ӵ� ����, ���� ���� ����
                break;
            default:
                break;
        }
    }
    void BatchShield() //���� ��ġ�ϴ� �Լ�
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.parent = transform;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 2f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1�� ��� ����
        }
    }

    void BatchSword() //�� ��ġ�ϴ� �Լ�
    {
        Transform sword = GameManager.instance.pool.Get(prefabId).transform;
        sword.parent = transform;
    }
    void Axe() //���� �Լ�
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
    IEnumerator RotateAndMove(Transform target, Vector3 dir) //���� ȸ���ϸ� ���ư��� �ڷ�ƾ
    {
        float rotationSpeed = 180f; // 1�ʿ� 180���� ȸ���ϵ��� ����
        float angle = 0f;
        while (angle < 360f)
        {
            // �ð��� ���� ȸ�� ������ ������ŵ�ϴ�.
            float rotationAmount = rotationSpeed * Time.deltaTime;
            target.Rotate(Vector3.forward, rotationAmount);
            target.Translate(dir * 2f * Time.deltaTime, Space.World);
            angle += Mathf.Abs(rotationAmount);
            yield return null;
        }
    }
    void Bow()
    {
        // �÷��̾��� �ٶ󺸴� ������ ���ɴϴ�.
        Vector3 dir = new Vector3(savex, savey, 0);

        // ������ ����ȭ�մϴ�.
        dir = dir.normalized;

        // �Ѿ� ������Ʈ�� Ǯ���� �����ɴϴ�.
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;

        bullet.GetComponent<Bullet>().Init(damage, 0, dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}