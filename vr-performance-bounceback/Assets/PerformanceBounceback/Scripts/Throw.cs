using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
  private SteamVR_TrackedObject trackedObj;
  private SteamVR_Controller.Device device;
  public float throwForce = 2f;

  // Use this for initialization
  void Start()
  {
    trackedObj = GetComponent<SteamVR_TrackedObject>();
  }

  void OnTriggerStay(Collider col)
  {
    // CHANGES
    // 1. Device by index acquirement to OnTriggerStay method moved. The device gets only
    // acquired if the controller touches the collider of the ball 
    // 2. Update method removed completely
    // 3. Debug log removed

    if (col.gameObject.CompareTag("Throwable"))
    {
      device = SteamVR_Controller.Input((int)trackedObj.index);

      if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
      {
        //Multi Throwing
        col.transform.SetParent(null);
        Rigidbody rigidBody = col.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;

        rigidBody.velocity = device.velocity * throwForce;
        rigidBody.angularVelocity = device.angularVelocity;
      }
      else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
      {
        col.GetComponent<Rigidbody>().isKinematic = true;
        col.transform.SetParent(gameObject.transform);

        device.TriggerHapticPulse(2000);
      }
    }
  }
}
