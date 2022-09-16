using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("�v���C���[�̃X�s�[�h")] float _speed;
    int _itemCount;//����̓���m�F�p
    Rigidbody _rb;

    void Start()
    {
       _rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // ���͂��ꂽ�������u�J��������Ƃ��� XZ ���ʏ�̃x�N�g���v�ɕϊ�����
        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        // �L�����N�^�[���u���͂��ꂽ�����v�Ɍ�����
        if (dir != Vector3.zero)
        {
            transform.forward = dir;
            dir = transform.forward;
        }

        // Y �������̑��x��ۂ��Ȃ���A���x�x�N�g�������߂ăZ�b�g����
        Vector3 velocity = dir.normalized * _speed;
        velocity.y = _rb.velocity.y;
        _rb.velocity = velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "Item")
        {
            Debug.Log($"�A�C�e���Q�b�g ");
            Destroy(collision.gameObject);
            _itemCount++;
        }
        if (_itemCount == 4)
        {
            Debug.Log($"�A�C�e�����ׂăQ�b�g");
        }
    }
}
