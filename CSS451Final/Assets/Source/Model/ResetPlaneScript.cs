using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlaneScript : MonoBehaviour
{
    private GameObject ball;
    private PinballScript ballScript;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        ball = GameObject.Find("Pinball");
        ballScript = ball.GetComponent<PinballScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ballScript.CheckForPass(transform))
        {
            float d = Vector3.Distance(transform.position, ball.transform.position);
            Vector3 onBarrier = ball.transform.position - (-transform.forward) * d;
            if (Vector3.Distance(onBarrier, transform.position) < transform.localScale.x / 1.4)
            {
                if (Vector3.Dot(ballScript.GetDir(), -transform.forward) < 0f)
                {
                    ballScript.SetVelocity(0f);
                    ballScript.SetInit();
                }
            }
        }
    }
}
