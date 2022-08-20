using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
/// <summary>
/// �����x���������邽�߂�script
/// </summary>
public class DebugAcceleration : MonoBehaviour
{
    [SerializeField] private OVRInput.Controller Rcontroller;

    [SerializeField] private OVRInput.Controller Lcontroller;
    
    // Start is called before the first frame update
    void Start()
    {
        Lcontroller =OVRInput.Controller.LTouch;
        Rcontroller = OVRInput.Controller.RTouch;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 acr = OVRInput.GetLocalControllerAngularAcceleration(Rcontroller);
        Debug.Log($"R�����x:{acr}");

        Vector3 acl = OVRInput.GetLocalControllerAcceleration(Lcontroller);
        Debug.Log($"L�����x:{acl}");
    }
}
