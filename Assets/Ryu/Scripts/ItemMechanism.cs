using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �v���C���[���g�p�ł���A�C�e���ɕt����R���|�[�l���g
/// </summary>
public class ItemMechanism : MonoBehaviour
{
    //OVR�̒͂܂��I�u�W�F�N�g�ɕt����R���|�[�l���g
    private OVRGrabbable _grabbable;

    [Tooltip("�A�C�e���̎��"),SerializeField] private ItemType _itemType;
    [Tooltip("�g�p�\����"),SerializeField] private bool _isUsable = false;

    /// <summary>
    /// �A�C�e���̎�ނ�ǂݎ��̂�
    /// </summary>
    public ItemType Type
    {
        get => _itemType;
    }

    void Start()
    {
        //OVRGrabbable���擾����
        _grabbable = GetComponent<OVRGrabbable>();

        //�A�C�e�����J�����Ȃ��
        if (Type == ItemType.Camera)
        {
            //�g�p�\�ɂ���
            _isUsable = true;
        }
    }

 
    void Update()
    {
        //��Ɏ����Ȃ���A�uOculus�R���g���[���[��A�{�^���v�܂��́u���N���b�N�v�������Ƃ�
        if (_grabbable.isGrabbed == true &&OVRInput.GetDown(OVRInput.Button.One)||Input.GetButtonDown("Fire1"))
        {
            //�g�p�\�Ȃ�
            if (_isUsable == true)
            {
                //�A�C�e�����g��
                UseItem();

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�A�C�e���g�p�G���A�̔��ʗp�N���X���擾����
        var type = other.GetComponent<ItemTag>();
        //���ʗp�N���X���擾�ł��Ă���ꍇ
        if (type != null)
        {
            Debug.Log(type.TagType);

            //�A�C�e���̎�ނƎg�p�G���A�̃^�C�v���Ή����Ă���Ƃ�
            if (_itemType == type.TagType)
            {
                //�A�C�e�����g�p�\�ɂ���
                _isUsable = true;
                Debug.Log($"�g�p�\:{Type}");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //�A�C�e���g�p�G���A�̔��ʗp�N���X���擾����
        var type = other.GetComponent<ItemTag>();
        //���ʗp�N���X���擾�ł��Ă���ꍇ
        if (type != null)
        {
            //���̃A�C�e���̎�ނƔ��ʗp�N���X�̎�ނ������Ȃ�
            if (_itemType == type.TagType)
            {
                //�A�C�e�����g�p�s�\�ɂ���
                _isUsable = false;
                Debug.Log($"�g�p�s�\:{Type}");
            }
        }
    }
    //�A�C�e�����g�p���鎞�̏���
    public void UseItem()
    {
        //�A�C�e���������
        Destroy(gameObject);
        Debug.Log("�A�C�e�����g����");
    }
}
