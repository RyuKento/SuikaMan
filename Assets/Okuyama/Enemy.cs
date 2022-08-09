using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField, Tooltip("�ǂ������鑊��")] GameObject _target;
    [SerializeField,Tooltip("�G�l�~�[�̃X�s�[�h")] float _speed = 0.1f;
    [SerializeField] StanEnemy _stanEnemy;
    float distance;
    /// <summary>�p�j���Ăق����ꏊ</summary>
    [SerializeField] Transform[] _wanderingPoint;
    [SerializeField] int destPoint = 0;
    //���M�F�}
    [SerializeField] private int _nowPoint = 0; 
    public bool _enemy = true;
   
    void Start()
    {
        GotoNextPoint();
    }

    void Update()
    {
        if (_stanEnemy.stop) { return; }
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
        if(distance < 15)
        {
            _enemy = false;
            //target�̕��ɏ������������ς��
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.transform.position - transform.position), 0.3f);

            //target�Ɍ������Đi��
            transform.position += transform.forward * _speed;
        }
        //���M�F�}
        if(_enemy == false&&distance >= 15)
        {
            //�p�j���Ăق������W�ɏ������������ς��
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_wanderingPoint[_nowPoint].transform.position - transform.position), 0.3f);
            //�p�j���Ăق������W�Ɍ������Đi��
            transform.position += transform.forward * _speed;
            //���W�Ƃ̋����̑傫����3�ȓ��ɂȂ�����
            if((_wanderingPoint[_nowPoint].transform.position - transform.position).magnitude <= 3)
            {
                //���̍��W�ɕς���
                _nowPoint++;
            }
            //�Ō�̍��W�ɓ��B������n�߂̍��W�Ɍ�����
            if (_nowPoint > _wanderingPoint.Length-1 )
            {
                _nowPoint = 0;
            }
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

        //// �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
        //// �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�
        //destPoint = (destPoint + 1) % _wanderingPoint.Length;
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "aa")
        {
            // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
            // �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�
            destPoint = (destPoint + 1) % _wanderingPoint.Length;
            GotoNextPoint();
            _enemy = false;
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
}
