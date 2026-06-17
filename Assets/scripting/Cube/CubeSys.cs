using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CubeSys : MonoBehaviour
{
    public bool Pickedup;
    public bool Released;
    public bool Detached;
    public bool Caught;
    public Camera CameraManager;
    public CinemachineCamera CubeCamera;
    public CinemachineCamera OtherCamera;
    private Transform CubeChild;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(Transform child in transform)
        {
           if (child.GetComponent<Rigidbody>() != null)
            {
                CubeChild = child;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Released && !Caught)
        {

            CubeChild.GetComponent<Rigidbody>().isKinematic = true;
            Caught = true;
            foreach (Transform child in transform.parent)
            {
                if (child == transform)
                    continue;

                if (child.GetComponent<CinemachineCamera>() != null)
                {
                    OtherCamera = child.GetComponent<CinemachineCamera>();
                    break;
                }
            }
            CubeCamera.Priority = 0;
            OtherCamera.Priority = 100;
            //   PlayerCamera.Target.TrackingTarget = Catapilt.transform;
            //    PlayerCamera.GetComponent<CinemachineFollow>().FollowOffset = new Vector3(-15f, 8f, 0f);
        }
        if (Released && !Detached)
            {
            CubeCamera.Priority = 100;
            if (OtherCamera != null)
            {
                OtherCamera.Priority = 0;
            }
            this.gameObject.transform.SetParent(null);
                Detached = true;
            CubeChild.GetComponent<Rigidbody>().isKinematic = false;
            Caught = false;
            }
     //   if (Pickedup)
     //       pickup();
    }
   // void public Pickup()
   // { 
   //             this.gameObject.transform.SetParent();
   //             break;
   //         }
   //     }
   public void ReceiveClickInput()
    {
        if (this.transform.parent != null)
        {
            Released = true;
        //    PlayerCamera.Target.TrackingTarget = this.transform.GetChild(0);
        //    PlayerCamera.GetComponent<CinemachineFollow>().FollowOffset = new Vector3(-5f, 1f, 0f);
        }
    }
}
