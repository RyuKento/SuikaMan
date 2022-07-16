using UnityEngine.AI;
using UnityEngine;
/// <summary>
/// �i�r���b�V�����g���Ƃ��p��Enemy
/// </summary>
public class SampleEnemy : MonoBehaviour
{
    [SerializeField, Tooltip("�p�j���Ăق����ꏊ")] Transform[] _wanderingPoint;
    [SerializeField, Tooltip("�p�j���Ăق����ꏊ")] int destPoint = 0;
    [SerializeField, Tooltip("�ǂ�������v���C���[")] GameObject _playerObj;
    [Tooltip("�i�r���b�V��")] NavMeshAgent m_agent;
    [Tooltip("�p�j����Bool")] bool _wanderingBool = true;

    private RaycastHit hit;

    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.autoBraking = false;

        GotoNextPoint();
    }

    void Update()
    {
        if (_wanderingBool == true)
        {
            if (!m_agent.pathPending && m_agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }
    }

    void GotoNextPoint()
    {

        Debug.Log(_wanderingBool);
        // �n�_���Ȃɂ��ݒ肳��Ă��Ȃ��Ƃ��ɕԂ��܂�
        if (_wanderingPoint.Length == 0)
            return;

        // �G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ肵�܂�
        m_agent.destination = _wanderingPoint[destPoint].position;

        // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
        // �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�
        destPoint = (destPoint + 1) % _wanderingPoint.Length;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == _playerObj)
        {
            var diff = _playerObj.transform.position - transform.position;
            var distance = diff.magnitude;
            var direction = diff.normalized;

            if (Physics.Raycast(transform.position, direction, out hit, distance))
            {
                Debug.Log("Ray����������");
                _wanderingBool = false;
                m_agent.isStopped = false;
                m_agent.destination = _playerObj.transform.position;
            }
            else
            {
                m_agent.isStopped = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _wanderingBool = true; ;
    }
}
