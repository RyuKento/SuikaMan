using UnityEngine;
using UnityEngine.AI;

public class Enemy : EnemyMoveStop
{
    [SerializeField, Tooltip("�ǂ������鑊��")] GameObject _target;
    [SerializeField,Tooltip("�G�l�~�[�̃X�s�[�h")] float _speed = 0.1f;
    float distance;
    

    /// <summary>�p�j���Ăق����ꏊ</summary>
    [SerializeField] Transform[] _wanderingPoint;
    [SerializeField] int destPoint = 0;
    bool _enemy = false;
   
    void Start()
    {
        GotoNextPoint();
    }

    void Update()
    {
        if(stop == true)
        {
            return;
        }
            

        var playerPos = _target.transform.position;
        distance = Vector3.Distance(this.transform.position, playerPos);

        if (distance < 15)
        {
            //target�̕��ɏ������������ς��
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.transform.position - transform.position), 0.3f);

            //target�Ɍ������Đi��
            transform.position += transform.forward * _speed;
        }
        else
        {
            GotoNextPoint();
            //if(_enemy == false) { GotoNextPoint(); }
        }
    }

    void GotoNextPoint()
    {
        // �n�_���Ȃɂ��ݒ肳��Ă��Ȃ��Ƃ��ɕԂ��܂�
        if (_wanderingPoint.Length == 0)
            return;
;
        //target�̕��ɏ������������ς��
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_wanderingPoint[destPoint].position - transform.position), 0.3f);
         //target�Ɍ������Đi��
        transform.position += transform.forward * _speed;
        //_enemy = false;

        //// �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
        //// �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�
        //destPoint = (destPoint + 1) % _wanderingPoint.Length;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _target)
        {
            Debug.Log($"�X�C�J�j���߂܂���");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Item")
        {
            // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
            // �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�
            destPoint = (destPoint + 1) % _wanderingPoint.Length;
            //GotoNextPoint();

        }
    }
}
