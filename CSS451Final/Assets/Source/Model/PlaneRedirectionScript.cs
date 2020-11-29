using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRedirectionScript : MonoBehaviour
{
    private GameObject ball;
    private PinballScript ballScript;
    private float mD;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Pinball");
        ballScript = ball.GetComponent<PinballScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballScript.CheckForPass(transform))
        {
            float d = Vector3.Distance(transform.position, ball.transform.position);
            Vector3 onBarrier = ball.transform.position - (-transform.forward) * d;
            if(Vector3.Distance(onBarrier, transform.position) < transform.localScale.x + (ball.transform.localScale.x / 2))
            {
                if (Vector3.Dot(ballScript.GetDir(), -transform.forward) < 0f)
                {
                    ballScript.ReflectDir(-transform.forward);
                }
            }
        }
    }
}
