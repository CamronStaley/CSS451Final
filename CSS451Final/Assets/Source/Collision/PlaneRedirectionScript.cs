using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRedirectionScript : MonoBehaviour
{
    private GameObject ball;
    private PinballScript ballScript;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Pinball");
        ballScript = ball.GetComponent<PinballScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ballScript.CheckForPass(transform))
        {
            float d = Vector3.Distance(transform.position, ball.transform.position);
            Vector3 onBarrier = ball.transform.position - (transform.up) * d;
            if (Vector3.Distance(onBarrier, transform.position) < transform.localScale.x * 1.1f)
            {
                if (Vector3.Dot(ballScript.GetDir(), -transform.forward) < 0f)
                {
                    ballScript.ReflectDir(-transform.forward);
                }
            }
        }
    }
}
