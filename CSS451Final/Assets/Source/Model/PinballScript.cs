using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballScript : MonoBehaviour
{
    public GameObject platform;
    public Vector3 dir;
    private float velocity;
    private Vector3 initPos;
    // Start is called before the first frame update
    void Start()
    {
        dir = transform.up;
        dir = dir.normalized;
        velocity = 0f;
        initPos = transform.position;
    }

    void Update()
    {
        if (velocity == 0) //if ball is at rest then set it to the start
        {
            Vector3 pos = transform.position;
            pos.y = platform.transform.position.y + (transform.localScale.y / 2);
            transform.position = pos;
        }
        else
        {
            float move = velocity * Time.deltaTime;
            transform.Translate(dir * move);
        }
    }

    // takes in a normalized direction 
    public void SetDir(Vector3 newDir)
    {
        dir = newDir;
    }

    public float GetVelocity()
    {
        return velocity;
    }

    public void SetVelocity(float v)
    {
        velocity = v;
    }

}
