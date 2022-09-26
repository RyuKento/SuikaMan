using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    [Tooltip("���ӃA�C�e���̃��X�g"), SerializeField] private List<GameObject> _surroundItems = new List<GameObject>();

    [Tooltip("�N���t�g�f�ނ̃��X�g"), SerializeField] private List<ItemType> _craftMaterials1 = new List<ItemType>();
    [Tooltip("�N���t�g��̃A�C�e��"), SerializeField] private GameObject _craftItem1;
    CraftMechanism _crRaft;

    //[Tooltip("�N���t�g�f�ނ̃��X�g"), SerializeField] private List<ItemType> _craftMaterials2 = new List<ItemType>();
    //[Tooltip("�N���t�g��̃A�C�e��"), SerializeField] private GameObject _craftItem2;
    //CraftMechanism _crKey;
    // Start is called before the first frame update
    void Start()
    {
        _crRaft = new CraftMechanism(_craftMaterials1,_craftItem1,0);
        //_crKey = new CraftMechanism(_craftMaterials2,_craftItem2,1);
    }

    // Update is called once per frame
    void Update()
    {
        //OVR�R���g���[���[��B�{�^���������ꂽ�Ƃ�
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            //���ӂ̃A�C�e�������N���t�g�̑f�ސ���葽���Ȃ�
            if (_craftMaterials1.Count <= _surroundItems.Count)
            {
                //���ӃA�C�e���̏Ƃ炵���킹���s��
                CheckList(_craftMaterials1,_craftItem1);
            }
            //���ӂ̃A�C�e�������N���t�g�̑f�ސ���葽���Ȃ�
            //if (_craftMaterials2.Count <= _surroundItems.Count)
            //{
            //    //���ӃA�C�e���̏Ƃ炵���킹���s��
            //    CheckList(_craftMaterials2,_craftItem2);
            //}
        }
        //�m�F�p
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            foreach (var item in _surroundItems)
            {
                Debug.Log(item);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�R���C�_�[�ɓ������I�u�W�F�N�g�̃R���|�[�l���g���擾
        var obj = other.gameObject.GetComponent<ItemMechanism>();
        
        //�����R���|�[�l���g���擾�ł��Ă���Ȃ�
        if (obj)
        {
            Debug.Log($"Add:{other.gameObject.name}:({obj})");
            //���X�g�ɉ�����
            _surroundItems.Add(other.gameObject);
        }
        Debug.Log($"_���ӂ̃A�C�e����:{_surroundItems.Count}");
    }
    private void OnTriggerExit(Collider other)
    {
        //�R���C�_�[�ɓ������I�u�W�F�N�g�̃R���|�[�l���g���擾
        var obj = other.gameObject.GetComponent<ItemMechanism>();
        //�����R���|�[�l���g���擾�ł��Ă���Ȃ�
        if (obj)
        {
            //���X�g����O��
            _surroundItems.Remove(other.gameObject);
        }
        Debug.Log($"_���ӂ̃A�C�e����:{_surroundItems.Count}");
    }
    /// <summary>
    /// �N���t�g��̃A�C�e���𐶐����鏈��
    /// </summary>
    public void CraftItem(GameObject item)
    {
        //�v���C���[�̑O���ɃN���t�g��̃A�C�e���𐶐�����
        Instantiate(item, transform.position += transform.forward
            , transform.rotation);
        //Instantiate(item, transform.position += transform.forward
        //  , transform.rotation);
    }
    //���ӂ̃A�C�e���̃��X�g���ƍ����鏈��
    public void CheckList(List<ItemType> array, GameObject craft)
    {
        //�N���t�g�f�ނ̎�ނ̍�Ɨp���X�g
        List<ItemType> useMaterial = new List<ItemType>();
        //���ӃA�C�e���̎�ނ𕡐�
        _surroundItems.ForEach(i => useMaterial.Add(i.GetComponent<ItemMechanism>().Type));
        //���ӃI�u�W�F�N�g�̍�Ɨp���X�g
        List<GameObject> useObject = new List<GameObject>();
        //���ӃI�u�W�F�N�g�𕡐�
        _surroundItems.ForEach(i => useObject.Add(i));
        //�N���t�g�\���ǂ��� 
        bool canCraft = true;
        //�N���t�g�f�ނ�S�Ċm�F����Ƃ�
        foreach(var item in array)
        {
            //���݂̃A�C�e�������݂��邩�ǂ���
            bool itemFind = false;
            //���ӃA�C�e���̐������m�F����Ƃ�
            for(int i = 0; i < useMaterial.Count; i++)
            {
                //���ӃA�C�e����i�Ԗڂƌ��݂̃A�C�e�����Ⴄ�Ȃ�
                if (useMaterial[i] != item)
                {
                    //i��i�߂�
                    continue;
                }
                //�����Ƃ���
                Debug.Log($"��v�F{useMaterial[i]}");
                //i�Ԗڂ̃A�C�e���̎�ނ𖳌��ɂ���
                useMaterial[i] = ItemType.Invalid;
                //���݂̃A�C�e�������݂��锻��
                itemFind = true;
                //���݂̃A�C�e����i�߂�
                break;
            }
            //�A�C�e�������݂��Ȃ��Ƃ�
            if (!itemFind)
            {
                //�N���t�g�s�\�Ȕ���
                canCraft = false;
                //�m�F���I����
                break;
            }
        }
        //�N���t�g���\�Ȏ�
        if (canCraft)
        {
            Debug.Log("Craft");
            //�N���t�g���̑f�ނ̏������s��
            MaterialProcess(useMaterial,useObject,craft);
        }

        ////��v���Ă���A�C�e���̗v�f�ԍ�
        //int[] checkNumber = new int[100];
        ////��v���Ă���A�C�e���̐�
        //int checkCount = 0;
        ////�N���t�g�f�ނ̗v�f���̉񐔌J��Ԃ�
        //for (int c = 0; c < _craftMaterials.Count; c++)
        //{
        //    //���ӂ̃A�C�e���̗v�f���̉񐔌J��Ԃ�
        //    for (int s = 0; s < _surroundingItems.Count; s++)
        //    {
        //        //���ӂ̃A�C�e���̎�ނ��擾
        //        var item = _surroundingItems[s].GetComponent<ItemMechanism>().Type;
        //        //�N���t�g�f�ނ̃��X�g�̎�ނƎ��ӃA�C�e���̃��X�g�̎�ނ������Ƃ�
        //        if (_craftMaterials[c] == item)
        //        {
        //            bool isExist = false;
        //            for(int i = 0; i < checkNumber.Length; i++)
        //            {
        //                if (checkNumber[i] == s)
        //                {
        //                    isExist = true;
        //                }
        //            }
        //            if (!isExist)
        //            {
        //                //���ӃA�C�e���̗v�f�����L��
        //                checkNumber[c] = s;
        //                checkCount++;
        //                Debug.Log($"s:{s}");
        //                break;
        //            }
        //        }

        //    }
        //}
        //Debug.Log($"count:{checkCount}");
        ////��v���ƃN���t�g�f�ނ̗v�f���������Ƃ�
        //if (_craftMaterials.Count == checkCount)
        //{
        //    //�A�C�e���𐶐�����
        //    CraftItem();
        //    //��v���Ă��鐔���̎��ӃA�C�e���������
        //    for (int i = 0; i < checkCount; i++)
        //    {
        //        _surroundingItems[checkNumber[i]].SetActive(false);
        //        _surroundingItems.RemoveAt(checkNumber[i]);
        //    }
        //}
    }
    /// <summary>
    /// �N���t�g���̑f�ނ̏���
    /// </summary>
    /// <param name="useMaterial">���ӃA�C�e���̎�ނ̃��X�g</param>
    /// <param name="useObject">���ӃA�C�e���̃I�u�W�F�N�g�̃��X�g</param>
    private void MaterialProcess(List<ItemType> useMaterial,List<GameObject> useObject, GameObject craft)
    {
        //�N���t�g�̐�������������
        CraftItem(craft);
        //���ӂ̃I�u�W�F�N�g�̃��X�g��S�Ċm�F����Ƃ�
        foreach(var item in useObject)
        {
            //�A�C�e�����\���ɂ���
            item.gameObject.SetActive(false);
        }
        //���ӂ̃A�C�e���̃��X�g����ɂ���
        _surroundItems.Clear();
        //���ӂ̃A�C�e���̎�ނ��m�F����Ƃ�
        for(int i = 0; i < useMaterial.Count; i++)
        {
            //���ӂ̃A�C�e���̎�ނ�i�Ԗڂ������Ȃ�Ai��i�߂�
            if (useMaterial[i] == ItemType.Invalid) continue;
            //�����łȂ��Ȃ���ӂ̃A�C�e����i�Ԗڂ����X�g�ɖ߂�
            _surroundItems.Add(useObject[i]);
            //i�Ԗڂ̃I�u�W�F�N�g��\������
            useObject[i].gameObject.SetActive(true);
        }
    }
}

public class CraftMechanism
{
    private List<ItemType> craftMaterials;
    private GameObject craftItem;
    private int priority;
    public CraftMechanism(List<ItemType> materials, GameObject item, int priority)
    {
        craftMaterials = materials;
        craftItem = item;
        this.priority = priority;
    }
}