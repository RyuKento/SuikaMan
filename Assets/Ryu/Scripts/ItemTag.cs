using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �A�C�e�����g�p����R���C�_�[�ɂ���R���|�[�l���g
/// </summary>
public class ItemTag : MonoBehaviour
{
    [Tooltip("�g�p�ł���A�C�e���̎��"),SerializeField] private ItemType _tagType;
    // Start is called before the first frame update
    /// <summary>
    /// �Ăяo���ꂽ�Ƃ��A�C�e���̎�ނ�ǂݎ���
    /// </summary>
    public ItemType TagType
    {
        get { return _tagType; }
    }
}
