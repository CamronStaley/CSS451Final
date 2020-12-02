using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraScript : MonoBehaviour
{
    public GameObject ball;
    private PinballScript bScript;
    // Start is called before the first frame update
    void Start()
    {
        bScript = ball.GetComponent<PinballScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ball.transform.position;   
        transform.forward = bScript.GetDir();
        transform.up = Vector3.forward;
    }
}
