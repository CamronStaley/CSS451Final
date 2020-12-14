using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleRedirectionScript : MonoBehaviour
{
    public NodePrimitive np;
    public SceneNode sn;
    public GameObject ball;
    private PinballScript ballScript;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        ballScript = ball.GetComponent<PinballScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale = np.GetScale();
        transform.position = np.GetLocation();
        transform.rotation = sn.GetRotation();
        if (ballScript.CheckForPassWithPaddles(transform.position, transform.up))
        {
            float d = Vector3.Distance(transform.position, ball.transform.position);
            Vector3 onBarrier = ball.transform.position - (transform.up) * d;
            if (Vector3.Distance(onBarrier, transform.position) < transform.localScale.x + ball.transform.localScale.x)
            {
                if (Vector3.Dot(ballScript.GetDir(), transform.up) < 0f)
                {
                    ballScript.ReflectDir(transform.up);
                }
            }
        }
    }
}
