using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrabObj : MonoBehaviour
{
    [SerializeField, Tooltip("����������")] float _sabun = 0.6f;
    [SerializeField, Tooltip("�_��Obj")] GameObject _targetObject;
    [Range(0F, 90F), Tooltip("���˂���p�x")] float ThrowingAngle = 45;
    [Tooltip("�R���g���[���[Pos")] Vector3 _prevPos = Vector3.zero;
    [Tooltip("�����pList")] List<Vector3> _log = new List<Vector3>();
    [Tooltip("OVRInput.Controller")] OVRInput.Controller rightCont, leftCont, controller;
    [Tooltip("OVRGrabber")] OVRGrabber grab;
    float sabunnkeisann;
    GameObject _throwObj;

    void Start()
    {
        grab = GetComponent<OVRGrabber>();
        rightCont = OVRInput.Controller.RTouch;
        leftCont = OVRInput.Controller.LTouch;
    }

    void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger) || OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            if(sabunnkeisann > _sabun)
            {
                ThrowingBall(_throwObj);
                sabunnkeisann = 0;
            }
        }

        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))//�E��
        {
            if (grab.grabbedObject == null){ return; }
            Debug.Log(grab.grabbedObject);
            keisan(rightCont);
            if(grab.grabbedObject.gameObject != null) { _throwObj = grab.grabbedObject.gameObject; }
            else { Debug.Log("null"); }
        }

        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger))//����
        {
            if (grab.grabbedObject == null) { return; }
            Debug.Log(grab.grabbedObject);
            keisan(leftCont);
            if (grab.grabbedObject.gameObject != null) { _throwObj = grab.grabbedObject.gameObject; }
            else { Debug.Log("null"); }
        }
    }
    void keisan(OVRInput.Controller controller)
    {
        Vector3 current = OVRInput.GetLocalControllerPosition(controller);
        if (_prevPos != Vector3.zero)
        {
            _log.Add(current - _prevPos);
            if (_log.Count > 10) _log.RemoveAt(0);
        }
        _prevPos = current;

        sabunnkeisann = _log.Sum(v => v.magnitude);
    }
    /// <summary>
    /// �{�[�����ˏo����
    /// </summary>
    private void ThrowingBall(GameObject obj)
    {
        if (obj != null)
        {
            // Ball�I�u�W�F�N�g�̐���
            GameObject ball = Instantiate(obj, this.transform.position, Quaternion.identity);
            // �W�I�̍��W
            Vector3 targetPosition = _targetObject.transform.position;

            // �ˏo�p�x
            float angle = ThrowingAngle;

            // �ˏo���x���Z�o
            Vector3 velocity = CalculateVelocity(this.transform.position, targetPosition, angle);

            // �ˏo
            Rigidbody rid = ball.GetComponent<Rigidbody>();
            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// �W�I�ɖ�������ˏo���x�̌v�Z
    /// </summary>
    /// <param name="pointA">�ˏo�J�n���W</param>
    /// <param name="pointB">�W�I�̍��W</param>
    /// <returns>�ˏo���x</returns>
    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {
        // �ˏo�p�����W�A���ɕϊ�
        float rad = angle * Mathf.PI / 180;

        // ���������̋���x
        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));

        // ���������̋���y
        float y = pointA.y - pointB.y;

        // �Ε����˂̌����������x�ɂ��ĉ���
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            // �����𖞂����������Z�o�ł��Ȃ����Vector3.zero��Ԃ�
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);
        }
        
    }
}
