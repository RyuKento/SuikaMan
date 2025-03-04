using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
/// <summary>
/// 加速度を可視化するためのscript
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
        Debug.Log($"R加速度:{acr}");

        Vector3 acl = OVRInput.GetLocalControllerAcceleration(Lcontroller);
        Debug.Log($"L加速度:{acl}");
    }
}
