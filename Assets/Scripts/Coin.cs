using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.tag == "Bullet")
            {
                gameObject.SetActive(true);
            }
            // �������� �Ծ��� ���� ���� �߰�
            // ��: ���� ����, ü�� ȸ�� ��
            // ���⼭�� ������ ������Ʈ�� ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}
