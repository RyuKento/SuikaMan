using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStan : Enemy
{
    //カメラが見つかったら後々入れる
     
    public bool isStop;

     void Start()
    {
        isStop = false;
    }

     void Update()
    {
        if(!isStop)
        {
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isStop = true;
    }
}
