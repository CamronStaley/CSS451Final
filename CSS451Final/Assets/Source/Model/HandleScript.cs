using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleScript : MonoBehaviour
{
    private bool selected = false;
    public float moveSpeed;
    public GameObject end;
    private Vector3 initPos;
    private float maxLoc;
    private float minLoc;
    // Start is called before the first frame update
    void Start()
    {
        maxLoc = transform.localPosition.x;
        minLoc = end.transform.localPosition.x;
        initPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(selected)
        {
            float x = Input.GetAxis("Mouse Y");
            x *= moveSpeed;
            float newPos = transform.localPosition.x + x;
            if (newPos < maxLoc && newPos > minLoc)
            {
                Vector3 newVecPos = transform.localPosition;
                newVecPos.x = newPos;
                transform.localPosition = newVecPos;
            }
        } else
        {
            if (transform.localPosition.x != maxLoc) //calculate the travel to velocity and apply to the ball
            {

            }
            transform.localPosition = initPos;
        }
    }

    public void Select(bool flag)
    {
        selected = flag;
    }
}
