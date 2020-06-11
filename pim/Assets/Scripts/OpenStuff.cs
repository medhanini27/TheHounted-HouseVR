using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

// Change the material when certain poses are made with the Myo armband.
// Vibrate the Myo armband when a fist pose is made.
public class OpenStuff : MonoBehaviour
{
    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;

    // Materials to change to when poses are made.
    public Material waveInMaterial;
    public Material waveOutMaterial;
    public Material doubleTapMaterial;
    public GameObject door;
    public GameObject hand;
    public GameObject Player;

    double dist = 3;
    int test;
    GameObject Object1; //what your picking up
    bool isHolding = false;
    GameObject[] l;



    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;
    void Start()
    {
        l = GameObject.FindGameObjectsWithTag("topick");
        door = GameObject.Find("dr R");
        hand = GameObject.FindGameObjectWithTag("hand");
        Player = GameObject.FindGameObjectWithTag("Player");

    }
    // Update is called once per frame.
    void Update ()
    {
        // Access the ThalmicMyo component attached to the Myo game object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

        // Check if the pose has changed since last update.
        // The ThalmicMyo component of a Myo game object has a pose property that is set to the
        // currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
        // detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
        // is not on a user's arm, pose will be set to Pose.Unknown.
        if (thalmicMyo.pose != _lastPose) {
            _lastPose = thalmicMyo.pose;

            // Vibrate the Myo armband when a fist is made.
            if (thalmicMyo.pose == Pose.Fist) {

                 
                for (int i = 0; i < l.Length; i++)
                {
                    if (Vector3.Distance(hand.transform.position, l[i].transform.position) < dist) //if distance is less than dist variable
                    {
                        isHolding = !isHolding; //changes isHolding var from false to true
                        Object1 = l[i];
                        Debug.Log("chosing  " + i + isHolding);
                        
                    }

                    if (test == 1)
                    { 
                    if (Vector3.Distance(hand.transform.position, door.transform.position) < dist)
                    {
                        if (Vector3.Distance(hand.transform.position, l[i].transform.position) < dist) //if distance is less than dist variable
                    {
                        isHolding = !isHolding; //changes isHolding var from false to true
                        Object1 = l[i];
                        Debug.Log("chosing  " + i);
                    }
                    }
                    }

                    
                }
                ExtendUnlockAndNotifyUserAction (thalmicMyo);

            // Change material when wave in, wave out or double tap poses are made.
            } else if (thalmicMyo.pose == Pose.WaveIn) {
                isHolding = false;
                hand.transform.DetachChildren();
                Object1.GetComponent<Rigidbody>().useGravity = true;


                //GetComponent<Renderer>().material = waveInMaterial;

                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            } else if (thalmicMyo.pose == Pose.WaveOut) {
                //GetComponent<Renderer>().material = waveOutMaterial;
                if (Vector3.Distance(hand.transform.position, door.transform.position) < dist) //if distance is less than dist variable
                {
                    Debug.Log("nearby");

                    //if (-0.2066299 > door.transform.rotation.y && door.transform.rotation.y > -0.8379524)
                    if (door.transform.rotation.eulerAngles.y < 1 && door.transform.rotation.eulerAngles.y > -1)

                    {
                        Debug.Log("open");
                        test = 1;


                    }
                    else
                        test = 2;

                }

                ExtendUnlockAndNotifyUserAction (thalmicMyo);
            } else if (thalmicMyo.pose == Pose.DoubleTap) {
                //GetComponent<Renderer>().material = doubleTapMaterial;

                ExtendUnlockAndNotifyUserAction (thalmicMyo);
            }
        }

        if (test == 1 && door.transform.rotation.eulerAngles.y != 270)
        {
            door.transform.Rotate(0, -1, 0);
            Debug.Log(door.transform.rotation.eulerAngles.y);

        }
        if (test == 2 && door.transform.rotation.eulerAngles.y > 1)
        {
            door.transform.Rotate(0, 1, 0);
            Debug.Log(door.transform.rotation.eulerAngles.y);

        }


        if (isHolding == true)
        {

            //Object1.GetComponent<Rigidbody>().useGravity = false; //sets gravity to not on so it doesn't just fall to the ground
            Object1.transform.parent = hand.transform;
            Debug.Log("dkhal");

            Object1.GetComponent<Rigidbody>().useGravity = false;
            //parents the object
            //  Object1.transform.position = SpawnTo.transform.position; //sets position (makes it go crazy)
            //  Object1.transform.rotation = SpawnTo.transform.rotation; //sets rotation  (makes it go crazy)
        }

    }

    // Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
    // recognized.
    void ExtendUnlockAndNotifyUserAction (ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard) {
            myo.Unlock (UnlockType.Timed);
        }

        myo.NotifyUserAction ();
    }
}
