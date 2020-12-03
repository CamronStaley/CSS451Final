using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballScript : MonoBehaviour
{

    public float boundFactor; // larger the factor smaller the objects bounds
    public GameObject platform;
    public Vector3 dir;
    private float velocity;
    private Vector3 initPos;
    private Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        velocity = 0f;
        initPos = transform.position;
        dir = Vector3.up;
    }

    void FixedUpdate()
    {
        CheckBounds();
        if (velocity == 0) //if ball is at rest then set it to the start
        {
            SetInit();
        }
        else
        {
            transform.position += velocity * Time.deltaTime * dir;
        }
    }

    public void CheckBounds()
    {
        if(Mathf.Abs(transform.localPosition.x) > 35f || Mathf.Abs(transform.localPosition.y) > 50f)
        {
            SetInit();
        }
    }

    public void SetInit()
    {
        initPos.y = platform.transform.position.y + (transform.localScale.y / 2);
        transform.position = initPos;
    }

    public bool CheckForPass(Transform p)
    {
        nextPos = transform.position + velocity * Time.deltaTime * dir;
        var currentDir = p.position - transform.position;
        var nextDir = nextPos - p.position;
        var p_norm = -p.forward;
        return (Vector3.Dot(currentDir, p_norm) > 0f && Vector3.Dot(nextDir, p_norm) < 0f);
    }

    public bool CheckForPassWithPaddles(Vector3 pos, Vector3 norm)
    {
        nextPos = transform.position + velocity * Time.deltaTime * dir;
        var currentDir = pos - transform.position;
        var nextDir = nextPos - pos;
        var p_norm = norm;
        return (Vector3.Dot(currentDir, p_norm) > 0f && Vector3.Dot(nextDir, p_norm) < 0f);
    }

    public void ReflectDir(Vector3 n)
    {  // WATCH OUT!! mDir is point towards n (instead of away!)
        float vDotn = Vector3.Dot(-dir, n);
        SetDir(2 * vDotn * n + dir);
    }

    // takes in a normalized direction 
    public void SetDir(Vector3 newDir)
    {
        dir = newDir;
    }

    //returns the normalized direction of the ball
    public Vector3 GetDir()
    {
        return dir;
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
