using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �A�C�e�����g�p����R���C�_�[�ɂ���R���|�[�l���g
/// </summary>
public class ItemTag : MonoBehaviour
{
    [Tooltip("�g�p�ł���A�C�e���̎��"),SerializeField] private ItemType _tagType;
    [Tooltip("���̃t�F�[�Y�Ŏg���A�C�e��"),SerializeField] private GameObject _gameObject;
    /// <summary>
    /// �Ăяo���ꂽ�Ƃ��A�C�e���̎�ނ�ǂݎ���
    /// </summary>
    public ItemType TagType
    {
        get { return _tagType; }
    }

    public void AdvancePhase()
    {
        if (_gameObject != null)
        {
            Instantiate(_gameObject,new Vector3(transform.position.x, transform.position.y+3, transform.position.z),transform.rotation);
            Destroy(this.gameObject);
        }
        if(TagType == ItemType.Key)
        {
            GameClear();
        }
    }
    public void GameClear()
    {
        Debug.Log("Game Clear");
    }
}
