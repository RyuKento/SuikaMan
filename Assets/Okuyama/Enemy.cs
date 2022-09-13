using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField, Tooltip("�ǂ������鑊��")] GameObject _target;
    [SerializeField,Tooltip("�G�l�~�[�̃X�s�[�h")] float _speed = 0.1f;
    [SerializeField] StanEnemy _stanEnemy;
    float distance;
    /// <summary>�p�j���Ăق����ꏊ</summary>
    [SerializeField] Transform[] _wanderingPoint;
    [SerializeField] int destPoint = 0;
    public bool _enemy = true;
    public bool _isCover = false;

    public float _missTime = 5f;
    private float _tempTime = 0;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GotoNextPoint();
    }

    void Update()
    {
        //���M�F�}
        if (_stanEnemy && _stanEnemy.stop) { return; }
        var playerPos = _target.transform.position;
        distance = Vector3.Distance(this.transform.position, playerPos);
        //if (distance < 15)
        //{
        //    //target�̕��ɏ������������ς��
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.transform.position - transform.position), 0.3f);

        //    //target�Ɍ������Đi��
        //    transform.position += transform.forward * _speed;
        //}
        if(_enemy == true)
        {
            GotoNextPoint();
        }
        if (distance < 15)
        {
            Ray();
            //���M
            Vector3 target = _target.transform.position;
            target.y = 0;
            Vector3 enemy = transform.position;
            enemy.y = 0;
            if (!_enemy)
            {
                //target�̕��ɏ������������ς��
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - enemy), 0.7f);

                //target�Ɍ������Đi��
                //transform.position += transform.forward * _speed;
                rb.velocity = transform.forward * _speed;
            }
        }
        //���M�F�}
        if (distance >= 15)
        {
            _enemy = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isCover&&!_enemy)
        {
            _tempTime += Time.deltaTime;
        }
        //Debug.Log(_tempTime);
        if (_missTime < _tempTime)
        {
            _enemy = true;
            _isCover = false;
            _tempTime = 0;
            Debug.Log("������߂�");
        }
    }
    void GotoNextPoint()
    {
        // �n�_���Ȃɂ��ݒ肳��Ă��Ȃ��Ƃ��ɕԂ��܂�
        if (_wanderingPoint.Length == 0)
            return;
        
        //���M
        Vector3 target = _wanderingPoint[destPoint].position;
        target.y = 0;
        Vector3 enemy = transform.position;
        enemy.y = 0;

        //target�̕��ɏ������������ς��
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - enemy), 0.7f);

        //target�Ɍ������Đi��
        //transform.position += transform.forward * _speed;
        rb.velocity = transform.forward * _speed;
        //���M
        float distance = Vector3.Distance(target, enemy);
        if (distance < 0.5f)
        {
            //// �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
            //// �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�
            destPoint = (destPoint + 1) % _wanderingPoint.Length;
        }
    }

    public void Ray()
    {
        var player = _target.transform.position;
         var distance = Vector3.Distance(this.transform.position, player);
        var array = Physics.RaycastAll(transform.position, player- this.transform.position);
        _isCover = false;
        foreach (var item in array)
        {
            if(item.transform.gameObject.tag == "Obstacle")
            {
                if(item.distance < distance)
                {
                    _isCover = true;
                    //Debug.Log("cover");
                }
            }
            if (_isCover == false && item.transform.gameObject.tag == "Player")
            {
                _enemy = false;
                //Debug.Log("discovery");
            }
        }
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "aa")
        {
            // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
            // �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�
            destPoint = (destPoint + 1) % _wanderingPoint.Length;
            GotoNextPoint();
            //_enemy = false;
        }
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log($"�X�C�J�j���߂܂���");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "aa") { _enemy = true; }
    }

    private void OnTriggerEnter(Collider other)
    {
        _stanEnemy = other.GetComponent<StanEnemy>();
    }
    private void OnTriggerExit(Collider other)
    {
        _stanEnemy = null;
    }
}

