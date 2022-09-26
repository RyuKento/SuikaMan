using Oculus.Interaction;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField, Tooltip("�ǂ������鑊��")] GameObject _target;
    [SerializeField,Tooltip("�G�l�~�[�̃X�s�[�h")] float _speed = 0.1f;
    private StanEnemy _stanEnemy;
    float distance;
    /// <summary>�p�j���Ăق����ꏊ</summary>
    [SerializeField] Transform[] _wanderingPoint;
    [SerializeField] int destPoint = 0;
    public bool _enemy = true;
    public bool _isCover = false;

    [SerializeField] float _timeMiss = 5f;
    [SerializeField] float _timeDivide = 60f;
    private float _timeMissStore = 0;
    [SerializeField]private float _timeDivideStore = 0;

    [SerializeField]private float _speedMagni = 1.5f;
    [SerializeField] private float _findDistance = 25f;
    Rigidbody rb;
    [SerializeField]PlayerManager _playerManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GotoNextPoint();
        if (_wanderingPoint.Length - 1 < destPoint) 
        {
            Debug.Log($"�J�E���g���Z�b�g�F{this.gameObject.name}");
            destPoint = 0; }
        destPoint %= _wanderingPoint.Length;
        if (_wanderingPoint.Length == 0)
        {
            var points = GameObject.FindGameObjectsWithTag("Point");
            var temp = 0;
            foreach (var point in points)
            {
                _wanderingPoint[temp] = point.transform;
                temp++;
            }
        }
    }

    void Update()
    {
        //���M�F�}
        if (_stanEnemy && _stanEnemy.stop) { return; }
        if (_playerManager.GetComponent<PlayerManager>().IsGame)
        {

            var playerPos = _target.transform.position;
            distance = Vector3.Distance(this.transform.position, playerPos);
            //if (distance < 15)
            //{
            //    //target�̕��ɏ������������ς��
            //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.transform.position - transform.position), 0.3f);

            //    //target�Ɍ������Đi��
            //    transform.position += transform.forward * _speed;
            //}
            if (_enemy == true)
            {
                GotoNextPoint();
            }
            if (distance < _findDistance)
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
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - enemy), 1f);

                    //target�Ɍ������Đi��
                    //transform.position += transform.forward * _speed;
                    rb.velocity = transform.forward * _speed * _speedMagni;
                }
            }
            //���M�F�}
            if (distance >= _findDistance)
            {
                _enemy = true;
            }

            var animator = GetComponent<Animator>();
            if (_enemy)
            {
                animator.SetInteger("nowState", 1);
            }
            else if (!_enemy)
            {
                animator.SetInteger("nowState", 2);
            }
        }
        else
        {

            var animator = GetComponent<Animator>();
            animator.SetInteger("nowState", 0);

        }
    }

    private void FixedUpdate()
    {
        if (_playerManager.GetComponent<PlayerManager>().IsGame)
        {
            if (_isCover && !_enemy)
            {
                _timeMissStore += Time.deltaTime;
            }
            //Debug.Log(_tempTime);
            if (_timeMiss < _timeMissStore)
            {
                _enemy = true;
                _isCover = false;
                _timeMissStore = 0;
                Debug.Log("������߂�");
            }

            _timeDivideStore += Time.deltaTime;
            if (_timeDivideStore > _timeDivide)
            {
                DivideEnemy();
            }
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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - enemy), 1f);

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

    public void DivideEnemy()
    {
        _timeDivideStore = 0;
        Instantiate(gameObject).GetComponent<Enemy>().destPoint = Calc();
    }
    public int Calc()
    {
        var num = destPoint > _wanderingPoint.Length - 1 ? destPoint++ : destPoint = 0;
        return num;
    }

    public void Catch(PlayerManager pm)
    {
        pm.GameOver();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Point")
        {
            // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
            // �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�
            destPoint = (destPoint + 1) % _wanderingPoint.Length;
            GotoNextPoint();
            //_enemy = false;
        }
        if (collision.gameObject.tag == "Player")
        {
            Catch(_playerManager);
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

