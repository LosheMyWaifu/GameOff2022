using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamareControl : MonoBehaviour
{
    public Camera pCamera;
    public Transform pCameraFollowPoint;

    //相机Z轴
    [Header("相机Z轴")]
    public float cameralayer = -10f;

    //死区
    [Header("相机跟随时间")]
    public float cameraSpeed = 0.5f;

    Vector3 currentVelocity;
    Vector3 tempVec;
    // Start is called before the first frame update
    void Start()
    {
        this.pCamera = Camera.main;
        pCameraFollowPoint = GameObject.Find("followPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        tempVec = new Vector3(pCameraFollowPoint.position.x, pCameraFollowPoint.position.y, cameralayer);
    }

    private void LateUpdate()
    {
        this.transform.position = Vector3.SmoothDamp(this.transform.position, tempVec, ref currentVelocity,cameraSpeed);
    }
}
