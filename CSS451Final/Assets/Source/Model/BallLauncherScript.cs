using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallLauncherScript : MonoBehaviour
{
    public Transform handle; // what the user grabs
    public Transform platform; // what the ball rests on top of
    public Transform bottom; // the bottom of the launching mechanism
    public GameObject pinball; // the ball
    public float velocityFactor = 1; // platform travel distance / velocityFactor = new ball velocity
    public float handleSpeed = 1; // how fast the handle moves when interacted with by the user

    private PinballScript ballScript;
    private float handleMinX; 
    private float handleMaxX;
    private bool selected = false;
    private float velocity = 0; // velocity of the launcher
    private Vector3 handleInit; // initial position of the handle
    // Start is called before the first frame update
    void Start()
    {
        handleInit = handle.localPosition;
        handleMaxX = handleInit.x;
        handleMinX = bottom.localPosition.x;
        ballScript = pinball.GetComponent<PinballScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected) //if the user is currently using the handle
        {
            float x = Input.GetAxis("Mouse Y");
            x *= handleSpeed; 
            float newPos = handle.localPosition.x + x;
            if (newPos < handleMaxX && newPos > handleMinX) //make sure it doesn't go past the start and end
            {
                Vector3 newVecPos = handle.localPosition;
                newVecPos.x = newPos;
                handle.localPosition = newVecPos;
            }
        } else // if the user isn't using the handle (potentially just released it)
        {
            velocity = (handleInit.x - handle.localPosition.x) * velocityFactor;
            if(ballScript.GetVelocity() == 0) // if the ball is currently at rest
            {
                ballScript.SetDir(platform.forward); // set it's direction to the same as the platform
                ballScript.SetVelocity(velocity); //give it the velocity that the platform traveled 
            }
            handle.localPosition = handleInit;
        }
    }

    public void Select(bool flag)
    {
        selected = flag;
    }
}
