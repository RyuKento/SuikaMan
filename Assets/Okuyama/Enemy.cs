using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Tooltip("�ǂ������鑊��")] GameObject _target;
    [SerializeField,Tooltip("�G�l�~�[�̃X�s�[�h")] float _speed = 0.1f;

    void Start()
    {

    }

    void Update()
    {
        //target�̕��ɏ������������ς��
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.transform.position - transform.position), 0.3f);

        //target�Ɍ������Đi��
        transform.position += transform.forward * _speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _target)
        {
            Debug.Log($"�X�C�J�j���߂܂���");
        }
    }
}
