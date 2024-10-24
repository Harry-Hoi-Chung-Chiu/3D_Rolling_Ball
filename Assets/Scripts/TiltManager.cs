using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TiltManager : MonoBehaviour
{
    public Transform plane;
    public XRNode controllerNode = XRNode.RightHand;
    private Quaternion controllerRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        if (device.TryGetFeatureValue(CommonUsages.deviceRotation, out controllerRotation))
        {
            ApplyRotationToPlane();
        }
    }

    void ApplyRotationToPlane()
    {
        Vector3 eulerRotation = controllerRotation.eulerAngles;
        Quaternion targetRotation = Quaternion.Euler(eulerRotation.x, 0, eulerRotation.z);
        plane.rotation = targetRotation;
    }
}
